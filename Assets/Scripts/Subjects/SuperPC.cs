using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuperPC : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float healthPC;

    [Header("End game settings")]
    [SerializeField] private GameObject endGameScript;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI restartButtonText;

    private float _startHealthPC;
    private EndGame _endGame;

    private void Start()
    {
        _endGame = endGameScript.GetComponent<EndGame>();
        _startHealthPC = healthPC;
    }

    public void DealingDamage(float damage)
    {
        healthPC -= damage;

        if(healthPC < 0)
        {
            healthPC = 0;
            Time.timeScale = 0;
            _endGame.SetEndState(EndGame.EndState.Lose);
            title.text = "You lose!";
            restartButtonText.text = "Restart";
            endPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        healthBar.fillAmount = healthPC / _startHealthPC;
    }
}
