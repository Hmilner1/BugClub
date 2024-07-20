using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class PlayerControllerOnline : NetworkBehaviour
{
    [SerializeField]
    private float smoothTime = 0.05f;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float groundDistance;
    [SerializeField]
    private GameObject InteractCanvas;
    [SerializeField]
    private Transform playerSpawn;
    [SerializeField]
    private GameObject playerCam;

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


    public override void OnNetworkSpawn()
    {
        transform.parent.position = new Vector3(-32, 1, 36);
        if (!IsOwner) { playerCam.SetActive(false); }
        base.OnNetworkSpawn();

    }

    private void OnEnable()
    {
        EventManager.instance.OnStopMovement.AddListener(DisableMovement);
        EventManager.instance.OnStartMovement.AddListener(EnableMovement);
        EventManager.instance.OnPlayerInteractOverlap.AddListener(EnableInteract);
        EventManager.instance.OnplayerStopInteract.AddListener(DisableInteract);
        EventManager.instance.OnPlayerLost.AddListener(RespawnPlayer);
        playerControls.Player.Interact.started += OnInteraction;
        playerControls.Player.Enable();

    }

    private void OnDisable()
    {
        EventManager.instance.OnStopMovement.RemoveListener(DisableMovement);
        EventManager.instance.OnStartMovement.RemoveListener(EnableMovement);
        EventManager.instance.OnPlayerInteractOverlap.RemoveListener(EnableInteract);
        EventManager.instance.OnplayerStopInteract.RemoveListener(DisableInteract);
        EventManager.instance.OnPlayerLost.RemoveListener(RespawnPlayer);
        playerControls.Player.Interact.started -= OnInteraction;
        playerControls.Player.Disable();
    }

    void Update()
    {
        if (!IsOwner) { return; }
        Movement();
    }

    private void DisableMovement()
    {
        if (!IsOwner) { return; }
        canMove = false;
        m_Animator.SetBool("IsWalking", false);
        m_Animator.SetFloat("Velocity", 0);

    }

    private void EnableMovement()
    {
        if (!IsOwner) { return; }
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
        if (!IsOwner) { return; }
        Interaction();
    }

    public void Interaction()
    {
        if (!IsOwner) { return; }
        if (canInteract)
        {
            EventManager.instance.Interact();
        }
    }


    private void EnableInteract()
    {
        if (!IsOwner) { return; }
        InteractCanvas.SetActive(true);
        canInteract = true;
    }

    private void DisableInteract()
    {
        if (!IsOwner) { return; }
        InteractCanvas.SetActive(false);
        canInteract = false;
    }

    private void RespawnPlayer()
    {
        if (!IsOwner) { return; }
        transform.position = playerSpawn.position;
        BugBox.instance.HealAll();
    }

}
