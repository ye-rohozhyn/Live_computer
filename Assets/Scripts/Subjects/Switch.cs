using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private SuperPC superPC;
    [SerializeField] private float damage;
    [SerializeField] private HingeJoint HingeJoint;
    [SerializeField] private float turnOnAngle = 90.0f;
    [SerializeField] private float turnOffAngle = 0.0f;
    [SerializeField] private bool isTurnOn = true;
    [SerializeField] private MeshRenderer indicator;
    [SerializeField] private Material turnOnMaterialIndicator;
    [SerializeField] private Material turnOffMaterialIndicator;
    [SerializeField] private int indexMaterial;
    [SerializeField] private MiniSwitchesPanel panel;
    [SerializeField] private MessageBox messageBox;

    private void Update()
    {
        if (!isTurnOn) superPC.DealingDamage(damage);
    }

    public void Click_Action()
    {
        if (isTurnOn) OffSwitch();
        else OnSwitch();
    }

    public void OffSwitch()
    {
        isTurnOn = false;

        JointSpring jointSpring = HingeJoint.spring;
        jointSpring.targetPosition = turnOffAngle;
        HingeJoint.spring = jointSpring;

        Material[] materials = indicator.materials;
        materials[indexMaterial] = turnOffMaterialIndicator;
        indicator.materials = materials;

        List<Switch> switches = panel.GetWaitingList();

        int index = switches.IndexOf(this);
        switches.RemoveAt(index);

        panel.SetWaitingList(switches);

        messageBox.ShowWarningMessage("Switch off", "Re-enable required");
    }

    public void OnSwitch()
    {
        isTurnOn = true;

        JointSpring jointSpring = HingeJoint.spring;
        jointSpring.targetPosition = turnOnAngle;
        HingeJoint.spring = jointSpring;

        Material[] materials = indicator.materials;
        materials[indexMaterial] = turnOnMaterialIndicator;
        indicator.materials = materials;

        List<Switch> switches = panel.GetWaitingList();
        switches.Add(this);
        panel.SetWaitingList(switches);
    }
}
