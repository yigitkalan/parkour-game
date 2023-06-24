using UnityEngine;

public class Pistol : NonAutoGun
{
    public override void Reload()
    {
    }

    public override void Shoot()
    {
        Debug.DrawRay(camHolderLocation.position, camHolderLocation.forward, Color.red, 3);
    }
}
