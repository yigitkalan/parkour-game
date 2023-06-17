using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Gun",menuName="Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public string name;

    [Header("Shooting")]
    public float damage;
    public bool isAutomatic;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    [HideInInspector]
    public bool isReloading;

}
