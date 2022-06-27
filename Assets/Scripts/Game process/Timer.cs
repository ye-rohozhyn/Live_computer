using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private float secondsLeft = 60;
    [SerializeField] private bool takingAway = false;

    [Header("End game settings")]
    [SerializeField] private GameObject endGameScript;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI restartButtonText;

    private EndGame _endGame;

    private void Start()
    {
        Time.timeScale = 1;
        _endGame = endGameScript.GetComponent<EndGame>();
    }

    private void Update()
    {
        if (takingAway == false & secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
        else if (takingAway == false & secondsLeft <= 0)
        {
            takingAway = true;
            Time.timeScale = 0;
            _endGame.SetEndState(EndGame.EndState.Win);
            title.text = "Complete level!";

            if (_endGame.GetCurrentLevel() == _endGame.GetMaxLevel())
            {
                restartButtonText.text = "Restart";
            }
            else
            {
                restartButtonText.text = "Next";
            }
            
            endPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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