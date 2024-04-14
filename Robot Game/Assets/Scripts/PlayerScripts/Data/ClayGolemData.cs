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
        functions[1] = new MeleeFunction(this);
        functions[2] = new WoodcuttingFunction(this);
        functions[3] = new FarmingFunction(this);
        variantName = "Clay Golem";
        ChargeLevel = 0;
    }

    protected override void CreateSkills()
    {
        skills.Add("health passive", 0);
        skills.Add("sandblast", 0);
    }

    public override void InitializePassives()
    {
        var host = GetEntity();
        passives = new List<Passive>();
        passives.Add(new HealthRegenPassive(host));
        passives.Add(new PassiveStat("health passive", host, StatModType.Base, EntityStatType.health, 5));
        passives.Add(new OnHitPassive("sandblast", host, delegate(Entity target) { return new DamageScript.damageData( 2 * host.data.skills["sandblast"] + .1f * host.stats[EntityStatType.damagepower].Value, DamageScript.damageType.range); }));
        // (2 per level + 10% damage) magic damage
    }

    new public ClayGolemEntity GetEntity()
    {
        return (ClayGolemEntity)base.GetEntity();
    }
}
