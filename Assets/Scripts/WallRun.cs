using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Wall{
    LeftWall,
    RightWall,
    None,
}

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] Rigidbody rb;


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


    [Header("Camera")]
    [SerializeField] Camera cam;
    [SerializeField] float normalFov;
    [SerializeField] float wallRunFov;
    [SerializeField] float fovTime;
    [SerializeField] float camTilt;
    [SerializeField] float camTiltTime;
    
    public float tilt {get; private set;}


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckWall();
        ManageWallJump();
    }

    void ManageWallJump(){
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
        leftWall = Physics.Raycast(transform.position, -orientation.right, out leftHit, wallCheckDistance,wallLayer);
        rightWall = Physics.Raycast(transform.position, orientation.right, out rightHit, wallCheckDistance,wallLayer);
    }

    bool CanWallRun()
    {
        Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.red);
        return (leftWall || rightWall) && !Physics.Raycast(transform.position, Vector3.down, minWallRunHeight);
    }

    void StartWallRun()
    {
        rb.useGravity = false;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunFov, fovTime*Time.deltaTime);

        Vector3 wallRunGravityDir = Vector3.down * wallRunGravityForce * Time.deltaTime;
        rb.AddForce(wallRunGravityDir, ForceMode.Force);
        if(leftWall){
           tilt = Mathf.Lerp(tilt,-camTilt,camTiltTime*Time.deltaTime);
        }
        if(rightWall){
           tilt = Mathf.Lerp(tilt,camTilt,camTiltTime*Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            if(leftWall && lastWall != Wall.LeftWall){
                lastWall = Wall.LeftWall;
                Vector3 wallRunJumpDirection = transform.up*1.5f+ leftHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Impulse);
                StartCoroutine(ResetLastWall());
            }
            if(rightWall && lastWall != Wall.RightWall){
                lastWall = Wall.RightWall;
                Vector3 wallRunJumpDirection = transform.up*1.5f + rightHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Impulse);
                StartCoroutine(ResetLastWall());
            }
        }
    }

    //this method is to keep player to stop spamming jump on the same wall
    IEnumerator ResetLastWall(){
        yield return new WaitForSeconds(2);
        lastWall = Wall.None;

    }

    void StopWallRun() {
        rb.useGravity = true;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFov, fovTime*Time.deltaTime);
        tilt = Mathf.Lerp(tilt,0,camTiltTime*Time.deltaTime);

    }
}
