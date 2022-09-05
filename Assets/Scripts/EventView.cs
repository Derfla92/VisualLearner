using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventView : MonoBehaviour
{
    public TimeManager timeManager;
    public RectTransform content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeManager.run)
        {
            content.offsetMax += new Vector2(Time.deltaTime, 0);
        }
    }



}
