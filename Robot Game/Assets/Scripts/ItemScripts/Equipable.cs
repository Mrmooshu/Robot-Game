using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipable : ItemData
{
    [Header("Equipable Properties")]
    [SerializeField] public List<ModData> itemModifiers;
    private List<StatMod> mods = new List<StatMod>();

    public void Equip(ref Dictionary<StatType, Stat> stats)
    {
        //create mods if they havent bene created yet
        if (mods.Count == 0)
        {
            foreach (ModData modData in itemModifiers)
            {
                mods.Add(new StatMod(modData.value, modData.bonusType, modData.statType, this));
            }
        }
        //add mods to stats
        foreach (StatMod mod in mods)
        {
            stats[mod.statType].AddModifier(mod);
        }
    }
    public void Unequip()
    {
        //remove mods from stats
        foreach (StatMod mod in mods)
        {
            mod.stat.RemoveModifier(mod);
        }
    }
}
