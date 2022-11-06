using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public enum StatType
{
    Health,Mana,MoveSpeed,JumpForce,Gravity
}

[Serializable]
public class Stat
{
    public float BaseValue;
    protected bool isDirty = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;

    protected readonly List<StatMod> statModifiers;
    public readonly ReadOnlyCollection<StatMod> StatModifiers;

    public event Action statUpdated;

    public Stat()
    {
        statModifiers = new List<StatMod>();
        StatModifiers = statModifiers.AsReadOnly();
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
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    public void AddModifier(StatMod mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    protected int CompareModifierOrder(StatMod a, StatMod b)
    {
        if (a.Type < b.Type)
            return -1;
        else if (a.Type > b.Type)
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
            if (statModifiers[i].Source == source)
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

            if (mod.Type == StatModType.Base || mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.Additive)
            {
                sumAdditive += mod.Value;

                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.Additive)
                {
                    finalValue *= 1 + sumAdditive;
                    sumAdditive = 0;
                }
            }
            else if (mod.Type == StatModType.Multiplicative)
            {
                finalValue *= 1 + mod.Value;
            }
        }
        statUpdated?.Invoke();
        return (float)Math.Round(finalValue, 4);
    }
}
