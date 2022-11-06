using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/EffectDatabase", fileName = "EffectDatabase")]
public class EffectDatabase : ScriptableObject
{
    public List<BuffData> buffList;
    public List<DebuffData> debuffList;
}