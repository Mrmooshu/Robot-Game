using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveStat : Passive
{
    protected StatMod modifier;

    public float perLevel;

    private StatModType mathType;

    private EntityStatType statType;

    public PassiveStat(string name, Entity host, StatModType mathType, EntityStatType statType, float perLevel)
    {
        type = passiveType.single;
        skillName = name;
        entity = host;
        entities = new List<Entity> { };
        this.mathType = mathType;
        this.statType = statType;
        this.perLevel = perLevel;
        modifier = new StatMod(StatFormula(), mathType, statType);
        AddEntity(host);
    }

    public PassiveStat(string name, List<Entity> hosts, StatModType mathType, EntityStatType statType, float perLevel)
    {
        type = passiveType.multiple;
        skillName = name;
        entities = new List<Entity> { };
        this.mathType = mathType;
        this.statType = statType;
        this.perLevel = perLevel;
        modifier = new StatMod(StatFormula(), mathType, statType);
        foreach (Entity host in hosts)
        {
            AddEntity(host);
        }
    }

    public override void Refresh()
    {
        modifier.Value = StatFormula();
    }

    protected float StatFormula()
    {
        if (type is passiveType.single)
        {
            return ((MinionEntity)entity).data.skills[skillName] * perLevel;
        }
        else if(type is passiveType.multiple)
        {
            return PlayerManager.instance.universal.upgrades[skillName] * perLevel;
        }
        return 0;

    }

    public override void AddEntity(Entity entity)
    {
        entities.Add(entity);
        entity.stats[statType].AddModifier(modifier);
    }

    public override void RemoveEntity(Entity entity)
    {
        entities.Remove(entity);
        entity.stats[statType].RemoveModifier(modifier);
    }
}
