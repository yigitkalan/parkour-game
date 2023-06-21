using UnityEngine;

public abstract class Gun : MonoBehaviour, ICanBeEquipped
{
    public GunData _gunData;

    protected IInputManager _inputManager;

    public void SetInputManager(IInputManager inputManager)
    {
        _inputManager = inputManager;
    }
    public abstract void Shoot();

    public abstract void Reload();
}
