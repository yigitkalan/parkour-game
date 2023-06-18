using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour, IEquippable
{
    public abstract void Shoot();

    public abstract void Reload();

    public abstract void Equip(GameObject player);

    public abstract void Drop(GameObject player);
}
