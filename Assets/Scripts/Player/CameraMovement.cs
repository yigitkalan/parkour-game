using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;

    private void Start(){
     Application.targetFrameRate = 300;
    }

    private void LateUpdate()
    {
        transform.position = playerPos.position;

    }
}


