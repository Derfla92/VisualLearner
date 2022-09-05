using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoResize : MonoBehaviour
{

    private Slider slider;
    private TimeManager timeManager;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (timeManager.run)
        {
            ReSize();
            updateSlider();
        }
        */
    }


    private void ReSize()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.offsetMax += new Vector2(1*Time.deltaTime, 0);
        rectTransform = slider.GetComponent<RectTransform>();
        rectTransform.offsetMax += new Vector2(1*Time.deltaTime, 0);
        slider.maxValue += Time.deltaTime;
        
    }

    private void updateSlider()
    {
        slider.value = timeManager.currentTime;
    }


}
