using UnityEngine;

public class InputManagerNonAuto : InputManager
{
    KeyCode shootingKey = KeyCode.Mouse0;
    KeyCode reloadKey = KeyCode.R;
    public override bool IsShooting()
    {
        return Input.GetKeyDown(shootingKey);
    }

    public override bool IsReloading()
    {
        return Input.GetKeyDown(reloadKey);
    }
}
