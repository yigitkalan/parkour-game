using UnityEngine;

public abstract class Gun : MonoBehaviour, ICanBeEquipped
{
    [SerializeField]
    protected Transform camHolderLocation;

    public GunData _gunData;

    public InputManager _inputManager;
    public void SetInputManager(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    public abstract void Shoot();

    public abstract void Reload();

}
