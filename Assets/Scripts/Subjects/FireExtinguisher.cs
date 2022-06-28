using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField] private Transform playerHand;
    [SerializeField] private Transform holder;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource audioShoot;

    private Rigidbody _rb;

    private void Start()
    {
        muzzleFlash.transform.parent = transform;
        _rb = GetComponent<Rigidbody>();
    }

    public void Take()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true;
        _rb.detectCollisions = false;

        transform.parent = playerHand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = playerHand.localRotation;

        transform.gameObject.layer = playerHand.gameObject.layer;

        foreach (Transform child in transform)
        {
            child.gameObject.layer = playerHand.gameObject.layer;
        }
    }

    public void Give()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true;
        _rb.detectCollisions = true;

        transform.parent = holder;
        transform.localPosition = Vector3.zero;
        transform.localRotation = holder.localRotation;

        transform.gameObject.layer = holder.gameObject.layer;

        foreach (Transform child in transform)
        {
            child.gameObject.layer = holder.gameObject.layer;
        }
    }

    public void Drop()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;
        _rb.detectCollisions = true;

        transform.parent = null;

        transform.gameObject.layer = holder.gameObject.layer;

        foreach (Transform child in transform)
        {
            child.gameObject.layer = holder.gameObject.layer;
        }
    }

    public void StartShoot()
    {
        if (muzzleFlash.isStopped)
        {
            muzzleFlash.Play();
            audioShoot.Play();
        }
    }

    public void StopShoot()
    {
        if (muzzleFlash.isPlaying)
        {
            muzzleFlash.Stop();
            audioShoot.Stop();
        }
    }
}
