using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayGolemData : MinionData
{
    public ClayGolemData(string variantName) : base(variantName)
    {
    }

    protected override void CreateSkills(string prefabName)
    {
        // create fresh skill tree values from variants skill tree
        skills = new Dictionary<string, int>()
        {
            { "health passive", 0},
            { "mana passive", 0},
            { "on hit passive", 0}








        };
    }

    public override void InitializePassives()
    {
        var host = GetEntity();
        passives = new List<Passive>();
        passives.Add(new PassiveStat("health passive", host, StatModType.Base, EntityStatType.Health));
        passives.Add(new PassiveStat("mana passive", host, StatModType.Base, EntityStatType.Mana));
        passives.Add(new OnHitPassive("on hit passive", host));
    }
}
