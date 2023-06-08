using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] float minWallRunHeight = 1.5f;
    [SerializeField] float wallCheckDistance = 0.7f;
    [SerializeField] float wallRunGravityForce = 10;
    [SerializeField] float wallRunJumpForce = 10;
    [SerializeField] Rigidbody rb;

    bool leftWall;
    bool rightWall;

    RaycastHit leftHit;
    RaycastHit rightHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWall();
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
        leftWall = Physics.Raycast(transform.position, -orientation.right, out leftHit, wallCheckDistance);
        rightWall = Physics.Raycast(transform.position, orientation.right, out rightHit, wallCheckDistance);
    }

    bool CanWallRun()
    {
        Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.red);
        return (leftWall || rightWall) && !Physics.Raycast(transform.position, Vector3.down, minWallRunHeight);
    }

    void StartWallRun()
    {
        rb.useGravity = false;

        Vector3 wallRunGravityDir = Vector3.down * wallRunGravityForce * Time.deltaTime;
        rb.AddForce(wallRunGravityDir, ForceMode.Force);

        if(Input.GetKeyDown(KeyCode.Space)){
            if(leftWall){
                Vector3 wallRunJumpDirection = transform.up*1.5f + leftHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Impulse);
            }
            if(rightWall){
                Vector3 wallRunJumpDirection = transform.up*1.5f + rightHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Impulse);
            }
        }
    }
    void StopWallRun() {
        rb.useGravity = true;
    }
}
