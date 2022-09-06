using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class EventLine : MonoBehaviour
{
    public TimeManager timeManager;
    public GameObject timeLine;
    public GameObject timeMarker;


    // Start is called before the first frame update
    void Start()
    {

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
        rectTransform.offsetMax += new Vector2(Time.deltaTime, 0);

        rectTransform = timeMarker.GetComponent<RectTransform>();
        rectTransform.offsetMax = new Vector2(timeManager.currentTime, rectTransform.offsetMax.y);
    }



}