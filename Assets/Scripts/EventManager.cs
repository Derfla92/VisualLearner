using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public TimeManager timeManager;

    public GameObject eventsContainer;
    public GameObject eventPrefab;
    public List<UiEvent> uiEvents = new List<UiEvent>();

    public int eventTime;
    public bool eventReccuring;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Only meant to be called when game isnt running
    public void NewEvent()
    {
        UiEvent newEvent = Instantiate(eventPrefab, eventsContainer.transform).GetComponent<UiEvent>();
        uiEvents.Add(newEvent);

        newEvent.recurringEvent = eventReccuring;
        newEvent.eventTime = eventTime;
        newEvent.rootEvent = newEvent;


        newEvent.PlaceEventOnTimeline();


        newEvent.initialized = true;

        eventTime = 0;
        eventReccuring = false;
    }

    public UiEvent NewRecurringEvent(float time)
    {
        UiEvent newEvent = Instantiate(eventPrefab, eventsContainer.transform).GetComponent<UiEvent>();

            uiEvents.Add(newEvent);

            newEvent.recurringEvent = true;
            newEvent.eventTime = time;
            newEvent.PlaceEventOnTimeline();

        return newEvent;
    }
}
