using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiEvent : MonoBehaviour
{

    public TimeManager timeManager;

    public Image icon;
    public bool initialized = false;

    public float eventTime = 0;
    public bool recurringEvent = false;

    public GameObject glow;
    public GameObject pointyThing;
    public GameObject background;
    public GameObject actionsContainer;

    public UiEvent rootEvent = null;

    public UiEvent previousEvent = null;
    public UiEvent nextEvent = null;

    public List<EventAction> eventActions = new List<EventAction>();



    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (initialized)
        {
            if (recurringEvent)
            {
                if (nextEvent == null)
                {
                    RectTransform timeline = GameObject.Find("Timeline").GetComponent<RectTransform>();
                    if (rootEvent == this)
                    {
                        if (timeline.rect.width > rootEvent.eventTime)
                        {
                            UiEvent newEvent = GameObject.Find("GameManager").GetComponent<EventManager>().NewRecurringEvent(eventTime * 2);
                            newEvent.rootEvent = rootEvent;
                            newEvent.previousEvent = this;
                            newEvent.icon.sprite = rootEvent.icon.sprite;
                            nextEvent = newEvent;
                            PlaceEventOnTimeline();
                            newEvent.initialized = true;

                        }
                    }
                    else
                    {
                        if (timeline.rect.width > eventTime + rootEvent.eventTime)
                        {
                            UiEvent newEvent = GameObject.Find("GameManager").GetComponent<EventManager>().NewRecurringEvent(eventTime + rootEvent.eventTime);
                            newEvent.rootEvent = rootEvent;
                            newEvent.previousEvent = this;
                            newEvent.icon.sprite = rootEvent.icon.sprite;
                            nextEvent = newEvent;
                            PlaceEventOnTimeline();
                            newEvent.initialized = true;


                        }

                    }

                }
            }
        }
        */
        if (eventTime < timeManager.currentTime)
        {
            pointyThing.GetComponent<Image>().color = Color.red;
            background.GetComponent<Image>().color = Color.red;
        }

    }

    public void AddAction(EventAction action)
    {
        EventAction newAction = Instantiate(action, actionsContainer.transform);
        newAction.deleteButton.SetActive(true);
        newAction.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleLeft;
        newAction.id = action.id;
        eventActions.Add(newAction);
        actionsContainer.GetComponent<RectTransform>().offsetMax = new Vector2(30, 6 + 32 * eventActions.Count);
        actionsContainer.GetComponent<RectTransform>().offsetMin = new Vector2(-30, 6);
        RectTransform rectTransform = newAction.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0);
        rectTransform.anchorMax = new Vector2(0.5f, 0);
        rectTransform.offsetMax = new Vector2(30, 32 * eventActions.Count);
        rectTransform.offsetMin = new Vector2(-30, 32 * (eventActions.Count - 1));

        if (rootEvent == this && nextEvent != null)
        {
            PropagateActions(newAction);
        }

    }


    public void PropagateActions(EventAction action)
    {
        if (nextEvent != null)
        {

            nextEvent.AddAction(action);

            nextEvent.PropagateActions(action);
        }

    }

    public void RemoveAction(EventAction delAction)
    {
        if (rootEvent == this && nextEvent != null)
        {
            PropagateRemove(delAction);
        }
        List<EventAction> removeActions = eventActions.FindAll(x => x.id == delAction.id);
        eventActions.RemoveAll(x => x.id == delAction.id);
        foreach (EventAction action in removeActions)
        {
            Destroy(action.gameObject);
        }
        List<EventAction> newList = eventActions.ToList();
        eventActions.Clear();
        foreach (EventAction action in newList)
        {
            eventActions.Add(action);
            RectTransform rectTransform = action.GetComponent<RectTransform>();
            rectTransform.offsetMax = new Vector2(30, 32 * eventActions.Count);
            rectTransform.offsetMin = new Vector2(-30, 32 * (eventActions.Count - 1));
        }
        actionsContainer.GetComponent<RectTransform>().offsetMax = new Vector2(30, 6 + 32 * eventActions.Count);
        actionsContainer.GetComponent<RectTransform>().offsetMin = new Vector2(-30, 6);
    }

    public void PropagateRemove(EventAction delAction)
    {
        if (nextEvent != null)
        {

            nextEvent.RemoveAction(delAction);

            nextEvent.PropagateRemove(delAction);
        }
    }



}
