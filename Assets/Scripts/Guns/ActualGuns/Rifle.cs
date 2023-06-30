using UnityEngine;

public class Rifle : AutoGun
{
    RaycastHit hitInfo;
    [SerializeField]
    ParticleSystem muzzleFlash;

    public override void Reload()
    {
    }

    public override void Shoot()
    {
        //this takes the firerate check part from parent
        if(!CanShoot()){
            return;
        }

        muzzleFlash.Play();
        Debug.DrawRay(camHolderLocation.position, camHolderLocation.forward * _gunData.range, Color.red, 3);
        if (GetHitInfo())
        {
            // print(hitInfo.transform.gameObject.name);
        }
    }
    bool GetHitInfo()
    {
        return Physics.Raycast(camHolderLocation.position, camHolderLocation.forward, out hitInfo, _gunData.range);

    }
}
