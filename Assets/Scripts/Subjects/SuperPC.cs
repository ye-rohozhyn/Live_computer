using UnityEngine;
using UnityEngine.UI;

public class SuperPC : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float healthPC;

    private float startHealthPC;

    private void Start()
    {
        startHealthPC = healthPC;
    }

    public void DealingDamage(float damage)
    {
        healthPC -= damage;

        if(healthPC < 0)
        {
            healthPC = 0;
        }

        healthBar.fillAmount = healthPC / startHealthPC;
    }
}
