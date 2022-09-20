using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class EventLine : MonoBehaviour
{
    public TimeManager timeManager;
    public GameObject timeLine;
    public GameObject timeMarker;
    public UnitHandler unitHandler;
    public GameObject events;
    public GameObject eventPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Boss boss = (Boss)unitHandler.enemies.Find(x => x is Boss);
        Spell[] bossSpells = boss.GetComponents<Spell>();
        foreach (Spell spell in bossSpells)
        {
            GameObject newEvent = Instantiate(eventPrefab);
            newEvent.transform.SetParent(events.transform);
            newEvent.GetComponent<UiEvent>().eventTime = (spell.cooldown + spell.castTime) * 2;
            newEvent.transform.GetComponent<RectTransform>().localPosition = new Vector2((spell.cooldown + spell.castTime) * 2, 43);
            newEvent.GetComponent<UiEvent>().recurringEvent = true;
            newEvent.GetComponent<UiEvent>().rootEvent = newEvent.GetComponent<UiEvent>();
            newEvent.GetComponent<UiEvent>().initialized = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeManager.run)
        {
            AdvanceTime();
        }
    }

    private void AdvanceTime()
    {
        RectTransform rectTransform = timeLine.GetComponent<RectTransform>();
        rectTransform.offsetMax += new Vector2(Time.deltaTime * 2, 0);

        rectTransform = timeMarker.GetComponent<RectTransform>();
        rectTransform.offsetMax = new Vector2(timeManager.currentTime * 2, rectTransform.offsetMax.y);
    }



}
