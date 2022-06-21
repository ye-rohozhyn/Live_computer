using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField] private Transform playerHand;
    [SerializeField] private Transform holder;
    [SerializeField] private ParticleSystem muzzleFlash;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Take()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true;

        transform.parent = playerHand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = playerHand.localRotation;
    }

    public void Give()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true;

        transform.parent = holder;
        transform.localPosition = Vector3.zero;
        transform.localRotation = holder.localRotation;
    }

    public void Drop()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;

        transform.parent = null;
    }

    public void StartShoot()
    {
        if(muzzleFlash.isStopped) muzzleFlash.Play();
    }

    public void StopShoot()
    {
        if (muzzleFlash.isPlaying) muzzleFlash.Stop();
    }
}
