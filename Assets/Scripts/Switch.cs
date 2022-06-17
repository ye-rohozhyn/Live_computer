using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private Axis axis = Axis.Y;
    [SerializeField] private float smooth = 2.0f;
    [SerializeField] private float angle = 90.0f;
    [SerializeField] private bool isTurnOn = false;
    [SerializeField] private bool withIndicator = false;
    [SerializeField] private MeshRenderer indicator;
    [SerializeField] private Material turnOnMaterialIndicator;
    [SerializeField] private Material turnOffMaterialIndicator;
    [SerializeField] private int indexMaterial;

    private Vector3 defaultRot;
    private Vector3 openRot;

    private enum Axis { X, Y, Z }

    private void Start()
    {
        defaultRot = transform.eulerAngles;
        switch (axis)
        {
            case Axis.X:
                openRot = new Vector3(defaultRot.x + angle, defaultRot.y, defaultRot.z);
                break;
            case Axis.Y:
                openRot = new Vector3(defaultRot.x, defaultRot.y + angle, defaultRot.z);
                break;
            case Axis.Z:
                openRot = new Vector3(defaultRot.x, defaultRot.y, defaultRot.z + angle);
                break;
        }
        
    }

    public void Click_Action()
    {
        isTurnOn = !isTurnOn;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(isTurnOn ? openRot : defaultRot), Time.deltaTime * smooth);

        if (withIndicator)
        {
            Material[] materials = indicator.materials;
            materials[indexMaterial] = isTurnOn ? turnOnMaterialIndicator : turnOffMaterialIndicator;
            indicator.materials = materials;
        }
    }
}
