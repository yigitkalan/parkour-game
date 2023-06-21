using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementForce = 10f;
    private Vector3 movementDirection;
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float moveMultiplier = 1000f;
    [SerializeField] private Transform orientation;

    [Header("Speed Check")]
    [SerializeField] private float walkLimit = 8f;
    [SerializeField] private float sprintLimit = 16f;
    [SerializeField] private float airLimit = 25;
    private float currentSpeedLimit;


    [Header("Jumping")]
    [SerializeField] private float jumpForce = 100f;
    private bool grounded;
    [SerializeField] private float gravityMultiplier = 40f;
    bool jumpCancelled;

    [Header("SlopeCheck")]
    private bool isOnSlope;
    private RaycastHit slopeHit;
    private Vector3 slopeMoveDir;

    [Header("Sliding")]
    [SerializeField] float slideForce;
    [SerializeField] float slideTime = 2;
    bool crouching = false;
    bool limitSpeed = true;
    bool canSlide = true;


    [Header("GroundCheck")]
    [SerializeField] private float playerHeight;
    [SerializeField] private float checkOffset = 0.2f;
    [SerializeField] private LayerMask groundLayer;


    private PlayerInput playerInput;
    private Rigidbody rb;
    PlayerLook _playerLook;

    private void Awake()
    {
        _playerLook = GetComponent<PlayerLook>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.onJump += Jump;
        playerInput.onJumpRelease += CutJump;
        playerInput.onSlide += Slide;
        playerInput.onSlideCancel += SlideReset;
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

    }
    private void FixedUpdate()
    {
        if (limitSpeed)
            SetSpeedLimit();
        CheckGrounded();
        Move();
        if (rb.useGravity && !grounded)
            IncreaseGravity();
    }

    void CheckSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + checkOffset))
        {
            if (slopeHit.normal != Vector3.up)
            {
                isOnSlope = true;
                slopeMoveDir = Vector3.ProjectOnPlane(movementDirection, slopeHit.normal);
            }
            else
            {
                isOnSlope = false;
            }
        }
        else
        {
            isOnSlope = false;
        }
    }

    void Move()
    {
        movementDirection = playerInput._horizontalInput * orientation.right + playerInput._verticalInput * orientation.forward;
        movementDirection = movementDirection.normalized;
        if (isOnSlope)
        {
            rb.AddForce(slopeMoveDir * Time.deltaTime * movementForce * moveMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(movementDirection * Time.deltaTime * movementForce * moveMultiplier, ForceMode.Acceleration);
        }

    }
    void SetSpeedLimit()
    {
        if (!grounded) currentSpeedLimit = airLimit;
        else currentSpeedLimit = playerInput.isSprinting ? sprintLimit : walkLimit;
        Vector3 horizontalSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalSpeed.magnitude > currentSpeedLimit)
        {
            horizontalSpeed = horizontalSpeed.normalized * currentSpeedLimit;
            rb.velocity = new Vector3(horizontalSpeed.x, rb.velocity.y, horizontalSpeed.z);
        }
    }
    void Slide()
    {
        if (canSlide && grounded)
        {
            if (rb.velocity.magnitude >= sprintLimit - 1 && transform.localScale.y == 1)
                StartCoroutine(SlideC());
            //this is for crouching part
            if (!crouching)
            {
                playerHeight = playerHeight * 0.5f;
                transform.localScale = new Vector3(1, 0.5f, 1);
                crouching = true;
                rb.AddForce(-transform.up * jumpForce * 0.5f, ForceMode.Impulse);
            }
        }
    }
    IEnumerator SlideC()
    {
        limitSpeed = false;
        canSlide = false;
        if (isOnSlope)
        {
            rb.velocity = rb.velocity + slopeMoveDir * slideForce;
        }
        else
        {
            rb.velocity = rb.velocity + movementDirection * slideForce;
        }
        yield return new WaitForSeconds(slideTime);
        limitSpeed = true;
        canSlide = true;
    }
    void SlideReset()
    {
        transform.localScale = Vector3.one;
        crouching = false;
        playerHeight = playerHeight * 2;
    }

    void Jump()
    {
        if (grounded)
        {
            jumpCancelled = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    void CutJump()
    {
        if (!grounded && !jumpCancelled)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.3f, rb.velocity.z);
            jumpCancelled = true;
        }
    }
    void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + checkOffset, groundLayer);
    }

    void IncreaseGravity()
    {
        if (rb.velocity.y < 1)
        {
            rb.AddForce(Vector3.down * gravityMultiplier * Time.deltaTime, ForceMode.Force);
        }
    }

}

