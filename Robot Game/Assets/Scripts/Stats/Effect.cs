using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EffectType
{
    Buff,Debuff
}
public class Effect
{
    public float duration;
    public Stat stat;
    public StatMod mod;
    public EffectType type;

    public Effect(BuffData buff, Entity target)
    {
        stat = target.stats[buff.statType];
        mod = new StatMod(buff.modValue, buff.modType, buff);
        duration = buff.duration;
        stat.AddModifier(mod);
        target.StartCoroutine(BuffCoroutine());
    }

    public Effect(DebuffData debuff, Entity target)
    {
        stat = target.stats[debuff.statType];
        mod = new StatMod(debuff.modValue, debuff.modType, debuff);
        duration = debuff.duration;
        stat.AddModifier(mod);
        target.StartCoroutine(BuffCoroutine());
    }

    public IEnumerator BuffCoroutine()
    {
        yield return new WaitForSeconds(duration);
        stat.RemoveModifier(mod);
    }
}
