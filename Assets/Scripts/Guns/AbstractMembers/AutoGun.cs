public abstract class AutoGun : Gun
{
    void Start(){
        SetInputManager(new InputManagerNonAuto());
    }
}
