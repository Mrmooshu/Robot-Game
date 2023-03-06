using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public enum StatType
{
    Health,HealthRegen,Mana,ManaRegen,MoveSpeed, MoveAcceleration,JumpForce,Gravity,AttackDamage,MagicDamage,AttackDefense,MagicDefense,CritChance
}

[Serializable]
public class Stat
{
    public float BaseValue;
    protected bool isDirty = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;

    public readonly List<StatMod> statModifiers;

    public event Action statUpdated;

    public Stat()
    {
        statModifiers = new List<StatMod>();
    }

    public Stat(float baseValue) : this()
    {
        BaseValue = baseValue;
    }

    public float Value
    {
        get
        {
            if (isDirty || lastBaseValue != BaseValue)
            {
                lastBaseValue = BaseValue;
                isDirty = false;
                _value = CalculateFinalValue();
                statUpdated?.Invoke();
            }
            return _value;
        }
    }

    public void AddModifier(StatMod mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
        mod.stat = this;
    }

    protected int CompareModifierOrder(StatMod a, StatMod b)
    {
        if (a.bonusType < b.bonusType)
            return -1;
        else if (a.bonusType > b.bonusType)
            return 1;
        return 0;
    }

    public bool RemoveModifier(StatMod mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumAdditive = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatMod mod = statModifiers[i];
            float modValue = mod.Value;

            if (mod.bonusType == StatModType.Base || mod.bonusType == StatModType.Flat)
            {
                finalValue += modValue;
            }
            else if (mod.bonusType == StatModType.Additive)
            {
                sumAdditive += modValue;

                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].bonusType != StatModType.Additive)
                {
                    finalValue *= 1 + sumAdditive;
                    sumAdditive = 0;
                }
            }
            else if (mod.bonusType == StatModType.Multiplicative)
            {
                finalValue *= 1 + modValue;
            }
        }
        return (float)Math.Round(finalValue, 4);
    }

    public void Recalculate()
    {
        isDirty = true;
        float v = Value;
    }
}
