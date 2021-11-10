using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon", fileName = "Weapon Item")]
public class Weapon : ItemData
{
    public enum Type
    {
        pickaxe, hatchet, fishingpole
    }
    [Header("Weapon Properties")]
    public Type weaponType;
    public RuntimeAnimatorController animController;
}
