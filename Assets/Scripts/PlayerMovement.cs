using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    private Vector3 movementDirection;
    private float currentMoveSpeed;
    [SerializeField]
    private float walkSpeed = 10f;
    [SerializeField]
    private float sprintSpeed = 20f;
    [SerializeField]
    private float moveDrag = 3f;
    private float moveMultiplier = 1000f;



    [Header("Jumping")]
    [SerializeField]
    private float jumpForce = 100f;
    private bool grounded;

    [Header("GroundCheck")]
    [SerializeField]
    private float playerHeight;
    [SerializeField]
    private float checkOffset = 0.4f;
    [SerializeField]
    private LayerMask groundLayer;


    private PlayerInput playerInput;
    private Rigidbody rb;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.onJump += Jump;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = moveDrag;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        SetMoveSpeed();
        Move();
        CheckGrounded();
        //UPDATE GRAVITY FOR QUICKFALL
    }
    void Move()
    {
        print(transform.right);
        movementDirection = playerInput._horizontalInput * transform.right + playerInput._verticalInput * transform.forward;
        movementDirection = movementDirection.normalized;
        rb.AddForce(movementDirection * Time.deltaTime * currentMoveSpeed * moveMultiplier, ForceMode.Acceleration);
    }
    void SetMoveSpeed()
    {
        currentMoveSpeed = playerInput.isSprinting ? sprintSpeed : walkSpeed;
    }
    void Jump()
    {
        if (grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + checkOffset, groundLayer);
    }

}
