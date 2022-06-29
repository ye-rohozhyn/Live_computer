using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 6;
    [SerializeField] private float pressDistance = 2;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject hint;
    [SerializeField] private MessageBox messageBox;
    [Header("Sounds")]
    [SerializeField] private AudioSource stepSource;
    [SerializeField] private AudioSource handSource;
    [SerializeField] private AudioClip stepSound;
    [SerializeField] private float stepRate = 1.5f;
    [SerializeField] private AudioClip takeSound;
    [SerializeField] private AudioClip giveSound;
    [SerializeField] private AudioClip pressSound;
    [Header("Menu")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject progressBarCanvas;
    [SerializeField] private GameObject messageCanvas;
    [SerializeField] private GameObject cursorCanvas;

    private CharacterController _controller;
    private Transform _playerBody;
    private Vector3 _move;
    private DrawOutline _previousInteraction;
    private float gravityValue = -9.81f;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private float _timer;
    private bool menuOpen = false;

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

        _timer += Time.deltaTime;
        if (_timer >= 1 / stepRate & groundedPlayer & _move != Vector3.zero)
        {
            _timer = 0;
            stepSource.PlayOneShot(stepSound);
        }
        
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
                                handSource.PlayOneShot(takeSound);
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
                                {
                                    hand.GetChild(0).gameObject.GetComponent<FireExtinguisher>().Give();
                                    handSource.PlayOneShot(giveSound);
                                }
                                else messageBox.ShowErrorMessage("it's not from here");
                            }
                            break;
                        case "Fuse":
                            if (hand.childCount == 0)
                            {
                                hit.collider.transform.gameObject.GetComponent<Fuse>().Take();
                                handSource.PlayOneShot(takeSound);
                            }
                            else
                            {
                                messageBox.ShowErrorMessage("I can't take it");
                            }
                            break;
                        case "Fuse holder":
                            if (hand.childCount == 1)
                            {
                                if (hand.GetChild(0).gameObject.tag == "Fuse")
                                {
                                    hand.GetChild(0).gameObject.GetComponent<Fuse>().Give(hit.transform.gameObject);
                                    handSource.PlayOneShot(giveSound);
                                }
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = menuOpen ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !menuOpen;
            progressBarCanvas.SetActive(menuOpen);
            messageCanvas.SetActive(menuOpen);
            cursorCanvas.SetActive(menuOpen);
            Time.timeScale = menuOpen ? 1 : 0;
            menuPanel.SetActive(!menuOpen);
            menuOpen = !menuOpen;
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

    public void ResumeClick()
    {
        Cursor.lockState =CursorLockMode.Locked;
        Cursor.visible = false;
        progressBarCanvas.SetActive(true);
        messageCanvas.SetActive(true);
        cursorCanvas.SetActive(true);
        Time.timeScale = 1;
        menuPanel.SetActive(false);
        menuOpen = false;
    }

    public void SettingsClick()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void ExitClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void BackClick()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void PressSound()
    {
        handSource.PlayOneShot(pressSound);
    }
}
