using UnityEngine;

public class GunManager : MonoBehaviour
{
    Gun currentGun;


    // Start is called before the first frame update
    void Start()
    {
        currentGun = transform.GetComponentInChildren<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGun._inputManager.IsShooting()){
            currentGun.Shoot();
        }
        
    }
}
