using UnityEngine;

public class InputManagerAuto : InputManager
{
    KeyCode shootingKey = KeyCode.Mouse0;
    KeyCode reloadKey = KeyCode.R;
    public override bool IsShooting()
    {
        return Input.GetKey(shootingKey);

    }

    public override bool IsReloading()
    {
        return Input.GetKeyDown(reloadKey);
    }
}
