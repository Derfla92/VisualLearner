using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public TimeManager timeManager;

    public GameObject eventsContainer;
    public GameObject eventPrefab;
    public List<UiEvent> uiEvents = new List<UiEvent>();

    public UnitHandler unitHandler;

    public int eventTime;
    public bool eventReccuring;

    // Start is called before the first frame update
    void Start()
    {
        GetBossEvents();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetBossEvents()
    {
        Boss boss = (Boss)unitHandler.enemies.Find(x => x is Boss);
        GameObject[] bossSpells = boss.GetComponent<SpellCaster>().spells.ToArray();
        foreach (GameObject spell in bossSpells)
        {
            UiEvent newEvent = NewRecurringEvent(spell.GetComponent<Spell>());
            newEvent.initialized = true;
            //UiEvent newEvent = Instantiate(eventPrefab).GetComponent<UiEvent>();

            //newEvent.eventTime = (spell.cooldown + spell.castTime) * 2;
            //newEvent.GetComponent<RectTransform>().localPosition = new Vector2((spell.cooldown + spell.castTime) * 2, -6);
            //newEvent.recurringEvent = true;
            //newEvent.rootEvent = newEvent;
            //newEvent.initialized = true;
        }
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

    public UiEvent NewRecurringEvent(Spell spell)
    {
        UiEvent newEvent = Instantiate(eventPrefab, eventsContainer.transform).GetComponent<UiEvent>();

        uiEvents.Add(newEvent);
        newEvent.icon.sprite = spell.icon;
        newEvent.recurringEvent = true;
        newEvent.eventTime = (spell.castTime + spell.cooldown) * 2;
        newEvent.rootEvent = newEvent;
        newEvent.PlaceEventOnTimeline();

        return newEvent;
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
