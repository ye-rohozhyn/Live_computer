using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 6;
    [SerializeField] private float pressDistance = 2;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject hint;
    [SerializeField] private MessageBox messageBox;

    private CharacterController _controller;
    private Transform _playerBody;
    private Vector3 _move;
    private DrawOutline _previousInteraction;
    private float gravityValue = -9.81f;
    private bool groundedPlayer;
    private Vector3 playerVelocity;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _playerBody = GetComponent<Transform>();
    }

    private void Update()
    {
        DrawOutline();
        CheckPressedKeys();
        Movement();
    }

    private void Movement()
    {
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        _move = _playerBody.right * x + _playerBody.forward * z;
        _controller.Move(_move * Time.deltaTime * playerSpeed);

        playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
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

                    Ray ray = new Ray(cameraObject.position, cameraObject.forward);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, pressDistance))
                    {
                        if (hit.collider.transform.tag == "Fire")
                        {
                            hit.collider.transform.GetComponent<SocketFire>().Damage(Time.deltaTime);
                        }
                    }
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
                            else
                            {
                                messageBox.ShowErrorMessage("I can't take it");
                            }
                            break;
                        case "Fire extinguisher holder":
                            if (hand.childCount == 1)
                            {
                                if (hand.GetChild(0).gameObject.tag == "Fire extinguisher")
                                    hand.GetChild(0).gameObject.GetComponent<FireExtinguisher>().Give();
                                else
                                    messageBox.ShowErrorMessage("it's not from here");
                            }
                            break;
                        case "Fuse":
                            if (hand.childCount == 0)
                            {
                                hit.collider.transform.gameObject.GetComponent<Fuse>().Take();
                            }
                            else
                            {
                                messageBox.ShowErrorMessage("I can't take it");
                            }
                            break;
                        case "Fuse holder":
                            if (hand.childCount == 1)
                            {
                                if(hand.GetChild(0).gameObject.tag == "Fuse")
                                    hand.GetChild(0).gameObject.GetComponent<Fuse>().Give(hit.transform.gameObject);
                                else
                                    messageBox.ShowErrorMessage("it's not from here");
                            }
                            break;
                        case "Box":
                            hit.transform.gameObject.GetComponent<Box>().Click_Action();
                            break;
                        case "Button":
                            hit.transform.gameObject.GetComponent<Button>().Click_Action();
                            break;
                        case "Electric panel":
                            hit.transform.gameObject.GetComponent<ElectricPanel>().Click_Action();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void DrawOutline()
    {
        Ray ray = new Ray(cameraObject.position, cameraObject.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pressDistance))
        {
            var interaction = hit.collider.GetComponent<DrawOutline>();

            if(interaction != null)
            {
                if(interaction != this && interaction != _previousInteraction)
                {
                    if (_previousInteraction != null) _previousInteraction.OnHoverExit();

                    interaction.OnHoverEnter();
                    _previousInteraction = interaction;

                    cursor.SetActive(false);
                    hint.SetActive(true);
                }
            }
            else if(_previousInteraction != null)
            {
                _previousInteraction.OnHoverExit();
                _previousInteraction = null;

                cursor.SetActive(true);
                hint.SetActive(false);
            }
        }
        else if (_previousInteraction != null)
        {
            _previousInteraction.OnHoverExit();
            _previousInteraction = null;

            cursor.SetActive(true);
            hint.SetActive(false);
        }
    }
}
