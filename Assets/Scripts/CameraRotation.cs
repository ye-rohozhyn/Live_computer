using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity = 100;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform cameraPosition;

    private Transform _cameraHolder;
    private float _xRotation = 0;

    void Start()
    {
        _cameraHolder = GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        _cameraHolder.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
