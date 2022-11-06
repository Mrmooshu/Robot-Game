using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Debuff", fileName = "Debuff")]
public class DebuffData : ScriptableObject
{
    [Header("Effect Properties")]
    public readonly EffectType type = EffectType.Debuff;
    public StatType statType;
    public float duration;
    public string debuffName = "debuff";
    public Sprite sprite;
    [Header("Modifier Properties")]
    public float modValue;
    public StatModType modType;
}