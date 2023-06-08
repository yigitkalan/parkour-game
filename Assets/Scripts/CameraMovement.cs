using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;

    private void LateUpdate()
    {
        transform.position = playerPos.position;

    }
}


