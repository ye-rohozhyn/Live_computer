using UnityEngine;

public class SocketFire : MonoBehaviour
{
    [SerializeField] private SuperPC superPC;
    [SerializeField] private float damage;
    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private float fireHealth = 5;
    [SerializeField] private float leftTime = 15;
    [SerializeField] private float range = 5;
    [SerializeField] private MessageBox messageBox;

    private float startTime = 15, startHealth = 5;
    private Vector3 fireScale;
    private bool showMessage = false;

    private void Start()
    {
        startTime = leftTime;
        startHealth = fireHealth;
        fireScale = fireEffect.transform.localScale;
    }

    private void Update()
    {
        if (leftTime > 0)
        {
            if (showMessage) showMessage = false;
            leftTime -= Time.deltaTime;
        }
        else if (!showMessage)
        {
            messageBox.ShowWarningMessage("Ignition in the socket", "Needs to be extinguished urgently");
            showMessage = true;
        }
        else
        {
            if (fireEffect.isStopped)
            {
                fireEffect.transform.localScale = fireScale;
                fireEffect.Play();
            }

            superPC.DealingDamage(damage);
        }
    }

    public void Damage(float damage){
        if (leftTime > 0) return;

        fireHealth -= damage;
        float scale = fireHealth / startHealth;
        fireEffect.transform.localScale = new Vector3(scale, scale, scale);

        if (fireHealth <= 0)
        {
            fireEffect.Stop();
            leftTime = startTime;
            fireHealth = startHealth;
        }
    }
}
