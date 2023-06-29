using UnityEngine;

public abstract class AutoGun : Gun
{
    void Start(){
        SetInputManager(new InputManagerAuto());
    }
}
