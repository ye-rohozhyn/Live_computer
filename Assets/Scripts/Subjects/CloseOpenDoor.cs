using UnityEngine;

public class CloseOpenDoor : MonoBehaviour
{
    [SerializeField] private HingeJoint HingeJoint;
    [SerializeField] private bool isBlocked = false;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private float closeAngle = 0;
    [SerializeField] private float openAngle = 90;

    public void Click_Action()
    {
        if (isBlocked) return;

        JointSpring jointSpring = HingeJoint.spring;
        jointSpring.targetPosition = isOpen ? closeAngle : openAngle;
        HingeJoint.spring = jointSpring;
        isOpen = !isOpen;
    }
}
