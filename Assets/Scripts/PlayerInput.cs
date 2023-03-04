using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector]
    public float _horizontalInput;
    [HideInInspector]
    public float _verticalInput;
    [HideInInspector]
    public bool isSprinting;

    public event Action onJump = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        isSprinting = false;

    }

    // Update is called once per frame
    void Update()
    {
        GetMoveInput();
        CheckSprint();
        CheckJump();
    }
    void GetMoveInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }
    void CheckSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
    }
    void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onJump();
        }
    }
}
