using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayGolemData : MinionData
{
    [SerializeField] private int _chargeLevel;
    public int ChargeLevel { get { return _chargeLevel; } set { _chargeLevel = value; ChargeLevelUpdated?.Invoke(); } }

    public event Action ChargeLevelUpdated;

    protected override void Create()
    {
        base.Create();
        functions[0] = new MiningFunction(this);
        variantName = "Clay Golem";
        ChargeLevel = 0;
    }

    protected override void CreateSkills()
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
        passives.Add(new OnHitPassive("sandblast", host, delegate(Entity target) { return new DamageScript.damageData( 2 * host.data.skills["sandblast"] + .1f * host.stats[EntityStatType.MagicDamage].Value, DamageScript.damageType.magic); }));
        // (2 per level + 10% ap) magic damage
    }

    new public ClayGolemEntity GetEntity()
    {
        return (ClayGolemEntity)base.GetEntity();
    }
}
