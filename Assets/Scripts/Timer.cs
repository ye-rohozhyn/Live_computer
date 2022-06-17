using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public GameObject textDisplay;
    public float secondsLeft = 60;
    private float StartTime = 60;
    public bool takingAway = false;
    public Image ProgressBar;
    

    private void Start()
    {
        StartTime = secondsLeft;
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    private void Update()
    {
        if(takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
    }

    IEnumerator TimerTake()
    {
        
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        ProgressBar.fillAmount = secondsLeft/StartTime;
        double minutes, seconds;

        minutes = Math.Floor(secondsLeft / 60);
        seconds = secondsLeft - 60 * Math.Floor(secondsLeft / 60);

        if (minutes < 10) textDisplay.GetComponent<Text>().text = "0" + minutes + ":";
        else textDisplay.GetComponent<Text>().text = minutes +  ":";
        if (seconds < 10) textDisplay.GetComponent<Text>().text += "0" + seconds;
        else textDisplay.GetComponent<Text>().text += seconds;
        takingAway = false;
    }

}
