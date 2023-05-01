using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    Transform orientation;
    float minWallRunHeight = 1.5f;
    float wallCheckDistance = 0.5f;

    bool leftWall;
    bool rightWall;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckWall(){
        leftWall = Physics.Raycast(transform.position, -orientation.right, wallCheckDistance);
    }

    bool CanWallRun(){
        return (leftWall || rightWall) && !Physics.Raycast(transform.position, Vector3.down, minWallRunHeight);
    }

}
