using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Effect
{
    public Entity target;
    public EffectData effectData;
    public int currentStacks = 1;
    public float currentDuration = 0;
    public List<StatMod> mods;

    public static void AddEffect(EffectData effectData, Entity target)
    {
        Effect effect = new Effect();
        effect.target = target;
        effect.effectData = effectData;
        effect.mods = new List<StatMod>();
        foreach (ModData mod in effectData.mods)
        {
            effect.mods.Add(new StatMod(mod.value, mod.bonusType, mod.statType, effect));
        }


        if (target.effects.ContainsKey(effectData.effectID))
        {
            effect = target.effects[effectData.effectID];
            if (effectData.stackable && (effect.currentStacks < effectData.maxStacks || effectData.maxStacks <= 0))
            {
                effect.currentStacks++;
            }
            effect.RefreshDuration();
        }
        else
        {
            effect.ActivateEffect();
        }
    }

    public static void AddEffect(int effectDataID, Entity target)
    {
        AddEffect(Database.GetEffect(effectDataID), target);
    }

    public static void AddEffect(string effectDataName, Entity target)
    {
        AddEffect(Database.GetEffectID(effectDataName), target);
    }

    private void RefreshDuration()
    {
        currentDuration = effectData.duration;
        ApplyStacksToMods();
        foreach (StatMod mod in mods)
        {
            target.stats[mod.statType].Recalculate();
        }
    }

    protected void ActivateEffect()
    {
        UpdateCaller.OnUpdate += Update;
        currentDuration = effectData.duration;
        ApplyStacksToMods();
        if (!target.effects.ContainsKey(effectData.effectID))
        {
            target.effects.Add(effectData.effectID, this);
            foreach (StatMod mod in mods)
            {
                target.stats[mod.statType].AddModifier(mod);
            }
        }
        if (target == PlayerManager.instance.activeMinion.GetEntity())
        {
            ((MinionEntity)target).InvokeEffectUpdate();
        }
    }

    protected void DeactivateEffect()
    {
        UpdateCaller.OnUpdate -= Update;
        target.effects.Remove(effectData.effectID);
        foreach (StatMod mod in mods)
        {
            target.stats[mod.statType].RemoveModifier(mod);
        }
        if (target == PlayerManager.instance.activeMinion.GetEntity())
        {
            ((MinionEntity)target).InvokeEffectUpdate();
        }
    }

    private void ApplyStacksToMods()
    {
        for (int i = 0; i < effectData.mods.Length; i++)
        {
            mods.ElementAt(i).Value = effectData.mods[i].value * currentStacks;
        }
    }

    private void Update()
    {
        currentDuration -= Time.deltaTime;
        if (currentDuration <= 0)
        {
            if (effectData.fallOff && currentStacks >= 2)
            {
                currentStacks--;
                RefreshDuration();
            }
            else
            {
                DeactivateEffect();
            }
        }
    }
}
