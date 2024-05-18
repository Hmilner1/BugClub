using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.05f;
    [SerializeField] private float speed;
    [SerializeField] private float groundDistance;

    private CharacterController characterController;
    private float currentVelocity;
    private Vector2 input;
    private Vector3 direction;
    private bool canMove;

    public Animator m_Animator;
    public InputSystem_Actions playerControls;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        characterController = GetComponent<CharacterController>();
        playerControls = new InputSystem_Actions();
        canMove = true;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        EventManager.instance.OnStopMovement.AddListener(DisableMovement);
        EventManager.instance.OnStartMovement.AddListener(EnableMovement);

    }

    private void OnDisable()
    {
        playerControls.Disable();
        EventManager.instance.OnStopMovement.RemoveListener(DisableMovement);
        EventManager.instance.OnStartMovement.AddListener(EnableMovement);

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

    void Movement()
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
}