using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public TimeManager timeManager;
    public EventLine eventLine;

    public GameObject eventsContainer;
    public GameObject eventPrefab;
    public List<UiEvent> uiEvents = new List<UiEvent>();
    public UiEvent nextEvent;

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
        //nextEvent = uiEvents.Find(x => x.eventTime > timeManager.currentTime);

        foreach(UiEvent e in uiEvents)
        {
            UiEvent current = e;
            while(current.nextEvent != null)
            {
                if(current.eventTime > timeManager.currentTime)
                {
                    break;
                }
                current = current.nextEvent;
            }
            if (nextEvent != null)
            {
                if (current.eventTime < nextEvent.eventTime)
                {
                    nextEvent = current;
                }
            }
            else
            {
                nextEvent = current;
            }
        }

        UpdateTimeLine();
    }

    public void GetBossEvents()
    {
        Boss boss = (Boss)unitHandler.enemies.Find(x => x is Boss);
        GameObject[] bossSpells = boss.GetComponent<SpellCaster>().spells.ToArray();
        foreach (GameObject spell in bossSpells)
        {
            NewRecurringEvent(spell.GetComponent<Spell>());
        }
    }

    public void UpdateTimeLine()
    {
        foreach (UiEvent rootEvent in uiEvents)
        {
            if (rootEvent.rootEvent = rootEvent)
            {
                int max = (int)Mathf.Floor(eventLine.timeLine.rect.width / (rootEvent.eventTime * 2));
                int current = GetLengthOfChain(rootEvent);
                if (current < max)
                {
                    NewRecurringEvent(rootEvent);
                }
            }
        }
    }

    public int GetLengthOfChain(UiEvent rootEvent)
    {
        int i = 1;
        UiEvent current = rootEvent;
        while(current.nextEvent != null)
        {
            current = current.nextEvent;
            i++;
        }
        return i;
    }

    public void NewRecurringEvent(Spell spell)
    {
        UiEvent newEvent = Instantiate(eventPrefab, eventsContainer.transform).GetComponent<UiEvent>();

        //Add to eventslist
        uiEvents.Add(newEvent);

        //Set event information according to spell
        newEvent.icon.sprite = spell.icon;
        newEvent.eventTime = (spell.castTime + spell.cooldown);
        newEvent.rootEvent = newEvent;
        newEvent.glow.SetActive(true);

        //Position Event on timeline
        RectTransform rectTransform = newEvent.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(newEvent.eventTime * 2,0);

    }

    public void NewRecurringEvent(UiEvent rootEvent)
    {
        UiEvent current = rootEvent;
        while (current.nextEvent != null)
        {
            current = current.nextEvent;
        }

        UiEvent newEvent = Instantiate(rootEvent, eventsContainer.transform).GetComponent<UiEvent>();
        current.nextEvent = newEvent;
        newEvent.rootEvent = rootEvent;
        newEvent.nextEvent = null;
        newEvent.eventTime = current.eventTime + rootEvent.eventTime;
        newEvent.glow.SetActive(true);
        RectTransform rectTransform = newEvent.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(newEvent.eventTime * 2, 0);


    }
}
