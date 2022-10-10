using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Tool", fileName = "Tool Item")]
public class Tool : ItemData
{
    public enum Type
    {
        pickaxe, hatchet, fishingpole
    }
    [Header("Tool Properties")]
    public Type toolType;
    public RuntimeAnimatorController animController;
}
