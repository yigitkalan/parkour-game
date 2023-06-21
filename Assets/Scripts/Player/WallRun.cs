using UnityEngine;

public enum Wall
{
    LeftWall,
    RightWall,
    None,
}

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerLook _playerLook;


    [Header("WallRuning")]
    [SerializeField] float minWallRunHeight = 1.5f;
    [SerializeField] float wallCheckDistance = 0.7f;
    [SerializeField] float wallRunGravityForce = 10;
    [SerializeField] float wallRunJumpForce = 10;
    [SerializeField] LayerMask wallLayer;
    bool leftWall;
    bool rightWall;
    RaycastHit leftHit;
    RaycastHit rightHit;
    Wall lastWall = Wall.None;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _playerLook = GetComponent<PlayerLook>();
    }

    void Update()
    {
        CheckWall();
        ResetLastWall();
        ManageWallJump();
    }

    void ManageWallJump()
    {
        if (CanWallRun())
        {
            if (leftWall)
            {
                StartWallRun();
            }
            else if (rightWall)
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    void CheckWall()
    {
        leftWall = Physics.Raycast(transform.position, -orientation.right, out leftHit, wallCheckDistance, wallLayer);
        rightWall = Physics.Raycast(transform.position, orientation.right, out rightHit, wallCheckDistance, wallLayer);
    }

    bool CanWallRun()
    {
        return (leftWall || rightWall) && !Physics.Raycast(transform.position, Vector3.down, minWallRunHeight);
    }

    void StartWallRun()
    {
        rb.useGravity = false;

        _playerLook.ChangeToFastFov();

        Vector3 wallRunGravityDir = Vector3.down * wallRunGravityForce * Time.deltaTime;
        rb.AddForce(wallRunGravityDir, ForceMode.Force);
        if (leftWall)
        {
            _playerLook.TiltCam(Wall.LeftWall);
        }
        if (rightWall)
        {
            _playerLook.TiltCam(Wall.RightWall);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            WallJump();
        }
    }

    void WallJump()
    {
        if (leftWall && lastWall != Wall.LeftWall)
        {
            lastWall = Wall.LeftWall;
            Vector3 wallRunJumpDirection = transform.up * 1.5f + leftHit.normal;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Impulse);
        }
        if (rightWall && lastWall != Wall.RightWall)
        {
            lastWall = Wall.RightWall;
            Vector3 wallRunJumpDirection = transform.up * 1.5f + rightHit.normal;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Impulse);
        }
    }

    // this method is to keep player from spamming jump on the same wall
    void ResetLastWall()
    {
        if (!(leftWall || rightWall))
        {
            lastWall = Wall.None;
        }
    }


    void StopWallRun()
    {
        rb.useGravity = true;
        _playerLook.ResetCameraTilt();
        _playerLook.ResetFov();
    }
}
