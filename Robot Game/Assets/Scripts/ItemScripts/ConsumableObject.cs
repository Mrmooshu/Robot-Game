using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]

public class ConsumableObject : ItemObject
{
    [SerializeField] private EffectType effect1 = EffectType.Nothing;
    [SerializeField] private EffectType effect2 = EffectType.Nothing;
    [SerializeField] private EffectType effect3 = EffectType.Nothing;
    [SerializeField] private EffectType effect4 = EffectType.Nothing;
    [SerializeField] private EffectType effect5 = EffectType.Nothing;
    public void Awake()
    {
        type = ItemType.Consumable;
    }
}

public enum EffectType
{
    Heal, BoostStat, Nothing
}
