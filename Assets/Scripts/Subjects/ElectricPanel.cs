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

    private float startTime = 60;
    private bool isTurnOn = false;

    private void Start()
    {
        startTime = leftTime;
        Click_Action();
    }

    private void Update()
    {
        if (leftTime > 0)
        {
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
        else if (!isTurnOn)
        {
            superPC.DealingDamage(damage);
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
    }
}
