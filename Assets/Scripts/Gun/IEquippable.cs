using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquippable
{
    public void Equip(GameObject player);

    public void Drop(GameObject player);
}
