using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gear Object", menuName = "Inventory System/Items/Gear")]

public class GearObject : ItemObject
{


    public void Awake()
    {
        type = ItemType.Gear;
    }

}
