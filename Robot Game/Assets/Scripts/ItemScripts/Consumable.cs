using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable", fileName = "Consumable Item")]
public class Consumable : Item
{
    public enum EffectType
    {
        heal, increaseStat
    }
    [Header("Consumable Properties")]
    public EffectType effectType;
}
