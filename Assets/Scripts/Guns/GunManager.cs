using DG.Tweening;
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
            currentGun.transform.DOShakeRotation(0.1f, 0.8f, 10, 90, false);
        }
        else{
            currentGun.transform.DOLocalRotate(Vector3.zero, 0.1f);
        }
        
    }
}
