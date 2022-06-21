using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private HingeJoint HingeJoint;
    [SerializeField] private float turnOnAngle = 90.0f;
    [SerializeField] private float turnOffAngle = 0.0f;
    [SerializeField] private bool isTurnOn = false;
    [SerializeField] private MeshRenderer indicator;
    [SerializeField] private Material turnOnMaterialIndicator;
    [SerializeField] private Material turnOffMaterialIndicator;
    [SerializeField] private int indexMaterial;

    public void Click_Action()
    {
        isTurnOn = !isTurnOn;

        JointSpring jointSpring = HingeJoint.spring;
        jointSpring.targetPosition = isTurnOn ? turnOffAngle : turnOnAngle;
        HingeJoint.spring = jointSpring;

        Material[] materials = indicator.materials;
        materials[indexMaterial] = isTurnOn ? turnOnMaterialIndicator : turnOffMaterialIndicator;
        indicator.materials = materials;
    }
}
