using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 6;
    [SerializeField] private float pressDistance = 2;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private Transform hand;

    private CharacterController _controller;
    private Transform _playerBody;
    private Vector3 _move;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _playerBody = GetComponent<Transform>();
    }

    private void Update()
    {
        CheckPressedKeys();
        Movement();
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        _move = _playerBody.right * x + _playerBody.forward * z;
        _controller.Move(_move * Time.deltaTime * playerSpeed);
    }

    private void CheckPressedKeys()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hand.childCount == 1)
            {
                GameObject handChild = hand.GetChild(0).gameObject;

                if (handChild.tag == "Fire extinguisher")
                {
                    handChild.GetComponent<FireExtinguisher>().Drop();
                }
                if(handChild.tag == "Fuse")
                {
                    handChild.GetComponent<Fuse>().Drop();
                }
            }
        }

        if (Input.GetButton("Fire1"))
        {
            if (hand.childCount == 1)
            {
                if (hand.GetChild(0).gameObject.tag == "Fire extinguisher")
                {
                    hand.GetChild(0).gameObject.GetComponent<FireExtinguisher>().StartShoot();
                }
            }
        }
        else if (hand.childCount == 1 && hand.GetChild(0).gameObject.tag == "Fire extinguisher")
        {
            hand.GetChild(0).gameObject.GetComponent<FireExtinguisher>().StopShoot();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(cameraObject.position, cameraObject.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pressDistance))
            {
                if (hit.collider.transform != null)
                {
                    switch (hit.collider.transform.tag)
                    {
                        case "Door":
                            hit.collider.transform.gameObject.GetComponent<CloseOpenDoor>().Click_Action();
                            break;
                        case "Switch":
                            hit.collider.transform.gameObject.GetComponent<Switch>().Click_Action();
                            break;
                        case "Fire extinguisher":
                            if (hand.childCount == 0)
                            {
                                hit.collider.transform.gameObject.GetComponent<FireExtinguisher>().Take();
                            }
                            break;
                        case "Fire extinguisher holder":
                            if (hand.childCount == 1)
                            {
                                hand.GetChild(0).gameObject.GetComponent<FireExtinguisher>().Give();
                            }
                            break;
                        case "Fuse":
                            if (hand.childCount == 0)
                            {
                                hit.collider.transform.gameObject.GetComponent<Fuse>().Take();
                            }
                            break;
                        case "Fuse holder":
                            if (hand.childCount == 1)
                            {
                                hand.GetChild(0).gameObject.GetComponent<Fuse>().Give(hit.transform.gameObject);
                            }
                            break;
                        case "Box":
                            hit.transform.gameObject.GetComponent<Box>().Click_Action();
                            break;
                        case "Button":
                            hit.transform.gameObject.GetComponent<Button>().Click_Action();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
