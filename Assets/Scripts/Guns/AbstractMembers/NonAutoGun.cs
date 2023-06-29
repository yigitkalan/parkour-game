using UnityEngine;

public abstract class NonAutoGun : Gun
{
    void Start(){
        SetInputManager(new InputManagerNonAuto());
    }

}
