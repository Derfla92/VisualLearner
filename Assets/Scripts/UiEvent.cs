using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiEvent : MonoBehaviour
{
    public Image icon;
    public bool initialized = false;

    public float eventTime = 0;
    public bool recurringEvent = false;

    UiEvent(int eventTime, bool recurringEvent)
    {
        this.eventTime = eventTime;
        this.recurringEvent = recurringEvent;
    }

    public UiEvent rootEvent = null;

    public UiEvent previousEvent = null;
    public UiEvent nextEvent = null;



    // Start is called before the first frame update
    void Start()
    {

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

}
