using UnityEngine;

public class Manometer : MonoBehaviour
{
    [SerializeField] private SuperPC superPC;
    [SerializeField] private float damage;
    [SerializeField] private HingeJoint HingeJoint;
    [SerializeField] private float leftTime = 50;

    private float startTime = 50;
    private float oneSecond = 1;

    private void Start()
    {
        startTime = leftTime;
    }

    private void Update()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            oneSecond -= Time.deltaTime;

            if (oneSecond <= 0) { 
                oneSecond = 1;
                JointSpring jointSpring = HingeJoint.spring;
                jointSpring.targetPosition += (180 / startTime);
                HingeJoint.spring = jointSpring;
            }
        }
        else
        {
            superPC.DealingDamage(damage);
        }
    }

    public void ResetTime()
    {
        leftTime = startTime;
        oneSecond = 1;
        JointSpring jointSpring = HingeJoint.spring;
        jointSpring.targetPosition = 0;
        HingeJoint.spring = jointSpring;
    }
}
