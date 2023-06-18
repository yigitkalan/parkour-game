using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float reloadTime;
    [HideInInspector]
    public bool isReloading;

}
