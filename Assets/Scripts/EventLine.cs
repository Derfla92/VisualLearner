using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using Unity.VisualScripting;

public class EventLine : MonoBehaviour
{
    public TimeManager timeManager;
    public RectTransform content;
    public RectTransform timeLine;
    public RectTransform timeMarker;



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
        timeLine.offsetMax += new Vector2(Time.deltaTime * 2, 0);

        timeMarker.offsetMax = new Vector2(timeManager.currentTime * 2, timeMarker.offsetMax.y);

        content.offsetMax += new Vector2(Time.deltaTime, 0);
    }



}
