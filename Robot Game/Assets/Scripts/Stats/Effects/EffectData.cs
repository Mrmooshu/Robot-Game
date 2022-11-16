using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct modData
{
    public float value;
    public StatModType bonusType;
    public StatType statType;
}

public abstract class EffectData : ScriptableObject
{

    [Header("Effect Properties")]
    public int effectID;
    public float duration;
    public bool stackable = false;
    public int maxStacks = 1;
    public bool fallOff = true;
    public string effectName;
    public Sprite sprite;
    [Header("Modifier Properties")]
    public modData[] mods;
}