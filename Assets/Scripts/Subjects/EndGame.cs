using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public enum EndState
    {
        Win,
        Lose
    }

    private EndState _endState;
    private int currentLevelNumber = 0;
    private int maxLevelNumber = 1;

    [SerializeField] private int currentLevel = 1;

    private void Start()
    {
        currentLevelNumber = PlayerPrefs.GetInt("Current complete level", 0);
    }

    public void SetEndState(EndState state)
    {
        _endState = state;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetMaxLevel()
    {
        return maxLevelNumber;
    }

    public void ClickNextOrRestart()
    {
        if (_endState == EndState.Win) //Next
        {
            if(currentLevel == maxLevelNumber) //Final level
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else 
            {
                if(currentLevel == currentLevelNumber) //Next level
                {
                    currentLevelNumber += 1;
                    PlayerPrefs.SetInt("Current complete level", currentLevelNumber);
                    SceneManager.LoadScene("Level " + currentLevelNumber);
                }
                else //Current level less current complete level
                {
                    SceneManager.LoadScene("Level " + currentLevel + 1);
                }
            }
        }
        else if (_endState == EndState.Lose) //Restart
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
