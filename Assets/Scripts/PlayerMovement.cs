using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    private Vector3 movementDirection;
    [SerializeField]
    private float movementForce = 10f;
    [SerializeField]
    private float groundDrag = 6f;
    [SerializeField]
    private float moveMultiplier = 1000f;
    [SerializeField]
    private Transform orientation;



    [Header("Speed Check")]
    [SerializeField]
    private float walkLimit = 8f;
    [SerializeField]
    private float sprintLimit = 16f;
    private float currentSpeedLimit;


    [Header("Jumping")]
    [SerializeField]
    private float jumpForce = 100f;
    private bool grounded;
    [SerializeField]
    private float gravityMultiplier = 40f;

    [Header("SlopeCheck")]
    private bool isOnSlope;
    private RaycastHit slopeHit;
    private Vector3 slopeMoveDir;



    [Header("GroundCheck")]
    [SerializeField]
    private float playerHeight;
    [SerializeField]
    private float checkOffset = 0.2f;
    [SerializeField]
    private LayerMask groundLayer;
    private bool isInAir;


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
        rb.drag = groundDrag;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSlope();
        print(isOnSlope);

    }
    private void FixedUpdate()
    {
        SetSpeedLimit();
        CheckGrounded();
        Move();
        IncreaseGravity();
    }

    void CheckSlope(){
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight/2+checkOffset)){
            isInAir = false;
            if(slopeHit.normal != Vector3.up){
                isOnSlope = true;
                slopeMoveDir = Vector3.ProjectOnPlane(movementDirection, slopeHit.normal);
            }
            else{
                isOnSlope =  false;
            }
        }
        else{
            isOnSlope =  false;
            isInAir = true;
        }
    }

    void Move()
    {
        movementDirection = playerInput._horizontalInput * orientation.right + playerInput._verticalInput * orientation.forward;
        movementDirection = movementDirection.normalized;
        if(isOnSlope){
            rb.AddForce(slopeMoveDir * Time.deltaTime * movementForce * moveMultiplier, ForceMode.Acceleration);
        }
        else{
            rb.AddForce(movementDirection * Time.deltaTime * movementForce * moveMultiplier, ForceMode.Acceleration);
        }

    }
    void SetSpeedLimit()
    {
        currentSpeedLimit = playerInput.isSprinting ? sprintLimit : walkLimit;
        Vector3 horizontalSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalSpeed.magnitude > currentSpeedLimit)
        {
            horizontalSpeed = horizontalSpeed.normalized * currentSpeedLimit;
            rb.velocity = new Vector3(horizontalSpeed.x, rb.velocity.y, horizontalSpeed.z);
        }

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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + checkOffset, groundLayer);
    }

    void IncreaseGravity()
    {
        if(isInAir && rb.velocity.y < 1){
            rb.AddForce(Vector3.down * gravityMultiplier * Time.deltaTime, ForceMode.Force);
        }
    }

}
