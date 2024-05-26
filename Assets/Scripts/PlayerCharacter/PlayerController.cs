using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float smoothTime = 0.05f;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float groundDistance;
    [SerializeField]
    private GameObject InteractCanvas;

    private CharacterController characterController;
    
    private float currentVelocity;
    private Vector2 input;
    private Vector3 direction;
    private bool canMove;
    private bool canInteract;

    public Animator m_Animator;
    public InputSystem_Actions playerControls;

    private void Awake()
    {
        playerControls = new InputSystem_Actions();
        Application.targetFrameRate = 120;
        characterController = GetComponent<CharacterController>();
        canMove = true;
        canInteract = false;
    }

    private void OnEnable()
    {
        EventManager.instance.OnStopMovement.AddListener(DisableMovement);
        EventManager.instance.OnStartMovement.AddListener(EnableMovement);
        EventManager.instance.OnPlayerInteractOverlap.AddListener(EnableInteract);
        EventManager.instance.OnplayerStopInteract.AddListener(DisableInteract);
        playerControls.Player.Interact.started += OnInteraction;
        playerControls.Player.Enable();

    }

    private void OnDisable()
    {
        EventManager.instance.OnStopMovement.RemoveListener(DisableMovement);
        EventManager.instance.OnStartMovement.RemoveListener(EnableMovement);
        EventManager.instance.OnPlayerInteractOverlap.RemoveListener(EnableInteract);
        EventManager.instance.OnplayerStopInteract.RemoveListener(DisableInteract);
        playerControls.Player.Interact.started -= OnInteraction;
        playerControls.Player.Disable();
    }

    void Update()
    {
        Movement();
    }

    private void DisableMovement()
    {

        canMove = false;
        m_Animator.SetBool("IsWalking", false);
        m_Animator.SetFloat("Velocity", 0);

    }

    private void EnableMovement()
    {
        canMove = true;
    }

    private void Movement()
    {
        if (!canMove) { return; }

        input = playerControls.Player.Move.ReadValue<Vector2>();
        direction = new Vector3(input.x, -1, input.y);
        if (input.sqrMagnitude == 0)
        {
            m_Animator.SetBool("IsWalking", false);
            m_Animator.SetFloat("Velocity", input.sqrMagnitude);
            return;
        }

        m_Animator.SetBool("IsWalking", true);
        m_Animator.SetFloat("Velocity", input.sqrMagnitude);
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

        characterController.Move(direction * speed * Time.deltaTime);
    }

    private void OnInteraction(InputAction.CallbackContext context)
    {
        Interaction();
    }

    public void Interaction()
    {
        if (canInteract)
        {
            EventManager.instance.Interact();
        }
    }


    private void EnableInteract()
    { 
        InteractCanvas.SetActive(true);
        canInteract = true;
    }

    private void DisableInteract()
    { 
        InteractCanvas.SetActive(false);
        canInteract = false;
    }
}