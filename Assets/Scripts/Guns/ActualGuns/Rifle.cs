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
        if (!CanShoot())
        {
            return;
        }

        muzzleFlash.Play();
        if (GetHitInfo())
        {

            //if we hit something
            if (GetHitInfo())
            {
                if (hitInfo.transform.gameObject.GetComponent<Rigidbody>() != null)
                {
                    hitInfo.rigidbody.AddForce(-hitInfo.normal * _gunData.damage * _bulletHitMultiplier * Time.deltaTime, ForceMode.Impulse);
                }

                GameObject bulletHitEffect = Instantiate(_bulletHitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bulletHitEffect, 0.5f);
                // print(hitInfo.transform.gameObject.name);
            }
        }
        bool GetHitInfo()
        {
            return Physics.Raycast(camHolderLocation.position, camHolderLocation.forward, out hitInfo, _gunData.range);

        }
    }
}
