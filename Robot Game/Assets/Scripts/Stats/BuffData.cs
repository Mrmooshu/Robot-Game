using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Buff", fileName = "Buff")]
public class BuffData : ScriptableObject
{
    [Header("Effect Properties")]
    public readonly EffectType type = EffectType.Buff;
    public StatType statType;
    public float duration;
    public string buffName = "buff";
    public Sprite sprite;
    [Header("Modifier Properties")]
    public float modValue;
    public StatModType modType;
}