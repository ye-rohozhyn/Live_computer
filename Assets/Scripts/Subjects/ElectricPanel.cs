using UnityEngine;

public class ElectricPanel : MonoBehaviour
{
    [SerializeField] private SuperPC superPC;
    [SerializeField] private float damage;
    [SerializeField] private HingeJoint HingeJoint;
    [SerializeField] private float angle = 45;
    [SerializeField] private float leftTime = 60;
    [SerializeField] private float range = 10;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private int index = 0;
    [SerializeField] private Material turnOnMaterial;
    [SerializeField] private Material turnOffMaterial;
    [SerializeField] private ParticleSystem[] effects;
    [SerializeField] private MessageBox messageBox;
    [SerializeField] private AudioSource handSource;
    [SerializeField] private AudioSource electricPanelSource;
    [SerializeField] private AudioClip switchSound;
    [SerializeField] private AudioClip sparkSound;

    private float startTime = 60;
    private bool isTurnOn = false;
    private bool showMessage = false;

    private void Start()
    {
        startTime = leftTime;
        leftTime = Random.Range(startTime - range, startTime + range);

        JointSpring joinSpring = HingeJoint.spring;
        joinSpring.targetPosition = 0;
        HingeJoint.spring = joinSpring;

        Material[] materials = meshRenderer.materials;
        materials[index] = turnOnMaterial;
        meshRenderer.materials = materials;

        isTurnOn = true;

        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i].isPlaying) effects[i].Stop();
        }
    }

    private void Update()
    {
        if (leftTime > 0)
        {
            if (showMessage) showMessage = false;
            leftTime -= Time.deltaTime;
        }
        else if (isTurnOn)
        {
            JointSpring joinSpring = HingeJoint.spring;
            joinSpring.targetPosition = angle;
            HingeJoint.spring = joinSpring;

            Material[] materials = meshRenderer.materials;
            materials[index] = turnOffMaterial;
            meshRenderer.materials = materials;

            isTurnOn = false;

            for (int i = 0; i < effects.Length; i++)
            {
                if(effects[i].isStopped) effects[i].Play();
            }
        }
        else if (!showMessage)
        {
            messageBox.ShowWarningMessage("Short circuit", "Set up electricity in the electrical panel");
            showMessage = true;
        }
        else if (!isTurnOn)
        {
            superPC.DealingDamage(damage);
            if(!electricPanelSource.isPlaying) electricPanelSource.PlayOneShot(sparkSound);
        }
    }

    public void Click_Action()
    {
        if (isTurnOn) return;

        leftTime = Random.Range(startTime - range, startTime + range);

        JointSpring joinSpring = HingeJoint.spring;
        joinSpring.targetPosition = 0;
        HingeJoint.spring = joinSpring;

        Material[] materials = meshRenderer.materials;
        materials[index] = turnOnMaterial;
        meshRenderer.materials = materials;

        isTurnOn = true;

        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i].isPlaying) effects[i].Stop();
        }

        handSource.PlayOneShot(switchSound);
    }
}
