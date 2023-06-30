using UnityEngine;

public class Pistol : NonAutoGun
{
    RaycastHit hitInfo;
    [SerializeField]
    ParticleSystem muzzleFlash;
    public override void Reload()
    {
        // isReloading =  true;
    }

    public override void Shoot()
    {
        if(!CanShoot()){
            return;
        }

        muzzleFlash.Play();
        Debug.DrawRay(camHolderLocation.position, camHolderLocation.forward * _gunData.range, Color.red, 3);
        if (GetHitInfo())
        {
            print(hitInfo.transform.gameObject.name);
        }
    }
    bool GetHitInfo()
    {
        return Physics.Raycast(camHolderLocation.position, camHolderLocation.forward, out hitInfo, _gunData.range);

    }
}
