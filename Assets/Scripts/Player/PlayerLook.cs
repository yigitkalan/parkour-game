using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSens = 10f;
    private float xRotation;
    private PlayerInput _playerInput;
    private float yRotation;


    [Header("Camera")]
    [SerializeField] GameObject cameraHolder;
    [SerializeField] private Camera playerCam;
    [SerializeField] float camTilt;
    [SerializeField] float camTiltTime;
    [SerializeField] int normalFov;
    [SerializeField] int wallRunFov;
    [SerializeField] float fovTime;
    float currentTilt;


    [SerializeField]
    private Transform orientation;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
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
        //x input for y rotation since horizantal mouse movement will determine
        // the rotation amount arount y axis
        yRotation += _playerInput.mouseXInput * mouseSens * Time.deltaTime;
        //and vise verca
        xRotation -= _playerInput.mouseYInput * mouseSens * Time.deltaTime;
        //limit the updated x rotation
        xRotation = Mathf.Clamp(xRotation, -90, 90);
    }
    void SetRotationInput()
    {
        //player will rotate along the y axis to rotate itself and the camera
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        //camera will do the up down look by rotating arount the x axis
        cameraHolder.transform.rotation = Quaternion.Euler(xRotation, yRotation, currentTilt);
    }

    public void TiltCam(Wall whichWall){
        if(whichWall == Wall.LeftWall){
            currentTilt = Mathf.Lerp(currentTilt, -camTilt, camTiltTime * Time.deltaTime);
        }
        else if( whichWall == Wall.RightWall){
            currentTilt = Mathf.Lerp(currentTilt, camTilt, camTiltTime * Time.deltaTime);
        }
    }
    public void ResetCameraTilt(){
        currentTilt = Mathf.Lerp(currentTilt, 0, camTiltTime * Time.deltaTime);
    }

    public void ChangeToFastFov(){
        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, wallRunFov, fovTime * Time.deltaTime);
    }
    public void ResetFov(){
        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, normalFov, fovTime * Time.deltaTime);
    }
}
