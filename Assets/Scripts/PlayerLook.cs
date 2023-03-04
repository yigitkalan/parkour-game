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


    Camera playerCam;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetRotationInput();
    }
    void GetRotationInput()
    {
        mouseXInput = Input.GetAxisRaw("Mouse X");
        mouseYInput = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseYInput * mouseSens * Time.deltaTime;
        xRotation += mouseXInput * mouseSens * Time.deltaTime;
    }
    void SetRotationInput()
    {
        //player will rotate along the y axis to rotate itself and the camera
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        //camera will do the up down look by rotating arount the x axis
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
