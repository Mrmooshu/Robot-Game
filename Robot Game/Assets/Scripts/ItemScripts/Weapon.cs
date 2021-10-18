using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon", fileName = "Weapon Item")]
public class Weapon : Item
{
    public enum Type
    {
        sword, staff, bow
    }
    [Header("Weapon Properties")]
    public Type weaponType;
}
