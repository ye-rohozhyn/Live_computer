using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject manometer;
    [SerializeField] private ParticleSystem[] effects;

    private Manometer _manometer;

    private void Start()
    {
        _manometer = manometer.GetComponent<Manometer>();
    }

    public void Click_Action()
    {
        animator.SetTrigger("isPressed");
        _manometer.ResetTime();

        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i].isStopped) effects[i].Play();
        }
    }
}
