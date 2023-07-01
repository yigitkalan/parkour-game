using UnityEngine;

public abstract class Gun : MonoBehaviour, ICanBeEquipped
{

    protected float nextTimeToFire;
    protected bool isReloading;

    [SerializeField]
    protected Transform camHolderLocation;

    public GunData _gunData;

    [SerializeField]
    protected float _bulletHitMultiplier = 20;

    [SerializeField]
    protected GameObject _bulletHitEffect;


    public InputManager _inputManager;
    public void SetInputManager(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    public abstract void Shoot();

    public abstract void Reload();

    public bool CanShoot()
    {
        if(Time.time < nextTimeToFire)

            return false;

        nextTimeToFire = Time.time + 1f / _gunData.fireRate;
        return true;
    }

}
