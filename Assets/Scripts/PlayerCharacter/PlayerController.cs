using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.05f;
    [SerializeField] private float speed;
    [SerializeField] private float groundDistance;

    private CharacterController characterController;
    private float currentVelocity;
    private Vector2 input;
    private Vector3 direction;

    public Animator m_Animator;
    public InputSystem_Actions playerControls;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
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