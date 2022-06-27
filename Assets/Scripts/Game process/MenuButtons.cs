using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel, playPanel, settingsPanel;
    [SerializeField] private UnityEngine.UI.Button[] levels;

    private int currentCompleteLevel = 1;
    private int maxLevelCount = 1;

    private void Start()
    {
        currentCompleteLevel = PlayerPrefs.GetInt("Current complete level", 0);

        for (int i = 0; i < levels.Length; i++)
        {
            if (i > currentCompleteLevel) levels[i].interactable = false;
        }
    }

    public void ClickPlayButton()
    {
        menuPanel.SetActive(false);
        playPanel.SetActive(true);
    }

    public void ClickSettingsButton()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void ClickExitButton()
    {
        Application.Quit();
    }

    public void ClickApplyButton()
    {

    }

    public void ClickSettingBackButton()
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void ClickPlayBackButton()
    {
        playPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void StartLevel(int levelNumber)
    {
        SceneManager.LoadScene("Level " + levelNumber);
    }
}
