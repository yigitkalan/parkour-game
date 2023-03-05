using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSens = 10f;

    private float mouseXInput;
    private float mouseYInput;
    private float xRotation;
    private float yRotation;

    [SerializeField]
    private Transform orientation;

    [SerializeField]
    private Transform playerCam;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetRotationInput();
        SetRotationInput();
    }
    void GetRotationInput()
    {
        mouseXInput = Input.GetAxisRaw("Mouse X");
        mouseYInput = Input.GetAxisRaw("Mouse Y");

        //x input for y rotation since horizantal mouse movement will determine
        // the rotation amount arount y axis
        yRotation += mouseXInput * mouseSens * Time.deltaTime;
        //and vise verca
        xRotation -= mouseYInput * mouseSens * Time.deltaTime;
        //limit the updated x rotation
        xRotation = Mathf.Clamp(xRotation, -90, 90);
    }
    void SetRotationInput()
    {
        //player will rotate along the y axis to rotate itself and the camera
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        //camera will do the up down look by rotating arount the x axis
        playerCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
