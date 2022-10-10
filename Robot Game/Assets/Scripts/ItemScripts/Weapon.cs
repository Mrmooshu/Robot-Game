using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon", fileName = "Weapon Item")]
public class Weapon : ItemData
{
    [Header("Weapon Properties")]
    public RuntimeAnimatorController animController;
}
