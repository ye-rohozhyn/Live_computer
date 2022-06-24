using System;
using System.Collections;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    [SerializeField] private Transform fuseWater;
    [SerializeField] private Transform playerHand;
    [SerializeField] private Rigidbody fuseRB;
    [SerializeField] private float lifeTime = 30;
    [SerializeField] private bool takingAway = false;

    private float startTime = 30;
    private float oneSecond = 1f;

    private void Start()
    {
        startTime = lifeTime;
    }

    private void Update()
    {
        if (takingAway == false & lifeTime > 0)
        {
            oneSecond -= Time.deltaTime;

            if (takingAway == false & oneSecond <= 0)
            {
                oneSecond = 1;
                lifeTime -= 1;
                fuseWater.localScale = new Vector3(1, 1, (lifeTime / startTime));

                if (transform.parent.gameObject.tag == "Fuse holder") takingAway = false;
            }
        }
        else
        {
            oneSecond = 1;
        }
    }

    public void Take()
    {
        takingAway = true;
        fuseRB.useGravity = false;
        fuseRB.isKinematic = true;
        fuseRB.detectCollisions = false;

        transform.parent = playerHand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = playerHand.localRotation;
    }

    public void Give(GameObject obj)
    {
        if (obj.tag == "Fuse holder") takingAway = false;

        fuseRB.useGravity = false;
        fuseRB.isKinematic = true;
        fuseRB.detectCollisions = false;

        transform.parent = obj.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        fuseRB.useGravity = true;
        fuseRB.isKinematic = false;
        fuseRB.detectCollisions = true;

        transform.parent = null;
    }
}
