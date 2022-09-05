using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timer;

    public GameObject BtnStart;
    public GameObject BtnStop;

    public bool run = false;
    public float currentTime = 0;
    public float timeMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            UpdateTime();
            UpdateTimer();
        }
    }

    private void UpdateTime()
    {
        currentTime += Time.deltaTime * timeMultiplier;
    }

    private void UpdateTimer()
    {
        timer.text = ((int)currentTime).ToString();
    }

    public void StartTime()
    {
        run = true;
        BtnStart.SetActive(false);
        BtnStop.SetActive(true);
    }

    public void StopTime()
    {
        run = false;
        BtnStart.SetActive(true);
        BtnStop.SetActive(false);
    }

    public void SpeedUp()
    {
        if (timeMultiplier < 5 && timeMultiplier >= 1)
        {
            timeMultiplier += 1;
        }
    }
    public void SpeedDown()
    {
        if (timeMultiplier <= 5 && timeMultiplier > 1)
        {
            timeMultiplier -= 1;
        }
    }
}
