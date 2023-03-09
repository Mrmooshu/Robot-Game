using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public ModData[] mods;
}