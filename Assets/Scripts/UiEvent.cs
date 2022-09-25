using System.Collections;
using System.Collections.Generic;
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
                            UiEvent newEvent = GameObject.Find("GameManager").GetComponent<EventManager>().NewRecurringEvent(eventTime*2);
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
            if(eventTime < timeManager.currentTime)
            {
                pointyThing.GetComponent<Image>().color = Color.red;
                background.GetComponent<Image>().color = Color.red;
            }
        }

    }


    public void PlaceEventOnTimeline()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (previousEvent != null)
        {
            rectTransform.anchoredPosition = new Vector2(previousEvent.eventTime + rootEvent.eventTime, rectTransform.anchoredPosition.y);
            eventTime = previousEvent.eventTime + rootEvent.eventTime;
        }
        else rectTransform.anchoredPosition = new Vector2(eventTime, rectTransform.anchoredPosition.y);
    }
    
    public void AddAction(EventAction newAction)
    {
        eventActions.Add(newAction);
        int numberOfActionsStored = eventActions.Count;
        actionsContainer.GetComponent<RectTransform>().offsetMax = new Vector2(30, 6+32 * numberOfActionsStored);
        actionsContainer.GetComponent<RectTransform>().offsetMin = new Vector2(-30, 6);
        newAction.transform.SetParent(actionsContainer.transform);
        RectTransform rectTransform = newAction.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0);
        rectTransform.anchorMax = new Vector2(0.5f, 0);
        rectTransform.offsetMax = new Vector2(30, 32 * numberOfActionsStored);
        rectTransform.offsetMin = new Vector2(-30, 0 + 32 *(numberOfActionsStored-1));
        
        if (rootEvent == this && nextEvent != null)
        {
                PropagateActions(newAction);
        }

    }

    public void PropagateActions(EventAction newAction)
    {
        if(nextEvent != null)
        {
            EventAction action = Instantiate(newAction);

            nextEvent.AddAction(action);

            nextEvent.PropagateActions(newAction);
        }
        
    }

    public void RemoveAction(EventAction delAction)
    {
        List<EventAction> delActions = eventActions.FindAll(x => x.id == delAction.id);
        eventActions.RemoveAll(x => x.id == delAction.id);
        List<EventAction> newActionList = new List<EventAction>();
        foreach (EventAction action in eventActions)
        {
            newActionList.Add(action);
        }
        actionsContainer.GetComponent<RectTransform>().offsetMax = new Vector2(30, 6);
        actionsContainer.GetComponent<RectTransform>().offsetMin = new Vector2(-30, 6);
        foreach (EventAction action in newActionList)
        {
            AddAction(action);
        }
        if (rootEvent == this && nextEvent != null)
        {
            PropagateRemove(delAction);
        }
        foreach (EventAction action in delActions)
        {
            Destroy(action.gameObject);
        }
    }

    public void PropagateRemove(EventAction delAction)
    {
        if(nextEvent != null)
        {

            nextEvent.RemoveAction(delAction);

            nextEvent.PropagateRemove(delAction);
        }
    }



}
