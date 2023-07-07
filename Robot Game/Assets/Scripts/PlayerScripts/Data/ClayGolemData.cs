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
            { "sandblast", 0}








        };
    }

    public override void InitializePassives()
    {
        var host = GetEntity();
        passives = new List<Passive>();
        passives.Add(new HealthRegenPassive(host));
        passives.Add(new PassiveStat("health passive", host, StatModType.Base, EntityStatType.Health, 5));
        passives.Add(new PassiveStat("mana passive", host, StatModType.Base, EntityStatType.Mana, 5));
        passives.Add(new OnHitPassive("sandblast", host, delegate(Entity target) { return new DamageScript.damageData( (.1f + host.data.skills["sandblast"] * .01f) * host.stats[EntityStatType.MagicDamage].Value, DamageScript.damageType.magic); })); // (10% + (.1% per level) ap) magic damage
    }
}
