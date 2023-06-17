using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] float smooth;
    [SerializeField] float swayMultiplier;
    [SerializeField] PlayerInput _playerInput;


    // Update is called once per frame
    void Update()
    {
        SwayWeapon();

    }

    void SwayWeapon(){
       Quaternion targetY = Quaternion.AngleAxis(-_playerInput.mouseXInput*swayMultiplier, Vector3.up);
       Quaternion targetX = Quaternion.AngleAxis(_playerInput.mouseYInput*swayMultiplier, Vector3.right);
        
       Quaternion targetRotation = targetX * targetY;

       transform.localRotation  = Quaternion.Slerp(transform.localRotation, targetRotation, smooth*Time.deltaTime);
    }
}
