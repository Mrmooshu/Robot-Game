using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/EffectDatabase", fileName = "EffectDatabase")]
public class EffectDatabase : ScriptableObject
{
    public List<EffectData> effectList;

    public List<BuffData> buffList;
    public List<DebuffData> debuffList;

    public void Initialize()
    {
        effectList.Clear();
        effectList.AddRange(buffList);
        effectList.AddRange(debuffList);
    }
}