using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private float secondsLeft = 60;
    [SerializeField] private bool takingAway = false;

    private void Update()
    {
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);

        secondsLeft -= 1;

        double minutes, seconds;
        minutes = Math.Floor(secondsLeft / 60);
        seconds = secondsLeft - 60 * Math.Floor(secondsLeft / 60);

        if (minutes < 10) textDisplay.text = "0" + minutes + ":";
        else textDisplay.text = minutes + ":";
        if (seconds < 10) textDisplay.text += "0" + seconds;
        else textDisplay.text += seconds;

        takingAway = false;
    }
}