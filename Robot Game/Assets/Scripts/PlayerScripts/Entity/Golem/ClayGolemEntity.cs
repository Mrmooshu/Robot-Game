using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayGolemEntity : GolemEntity
{
    public override void InitializePassives()
    {
        base.InitializePassives();
        passives.Add(new PassiveStat("health passive", StatModType.Base, EntityStatType.health, () => { return 5; }, host: this));
        passives.Add(new OnHitPassive("sandblast", delegate (Entity target) { return new DamageScript.damageData(2 * data.skills["sandblast"] + .1f * stats[EntityStatType.damagepower].Value, DamageScript.damageType.range); }, host: this));
        // (2 per level + 10% damage) magic damage
    }
}
