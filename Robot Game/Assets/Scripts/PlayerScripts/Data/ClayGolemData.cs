using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayGolemData : GolemData
{
    [SerializeField] private int _chargeLevel;
    public int ChargeLevel { get { return _chargeLevel; } set { _chargeLevel = value; ChargeLevelUpdated?.Invoke(); } }

    public override GameObject Blueprint { get { return GeneralManager.instance.entityPrefabs.LoadAsset<GameObject>("Clay Golem"); } }

    public event Action ChargeLevelUpdated;

    public ClayGolemData(MinionData data = null) : base(data)
    {

    }


    protected override void Create(MinionData data = null)
    {
        base.Create(data);
        ChargeLevel = 0;
    }

    protected override void CreateSkills()
    {
        skills.Add("health passive", 0);
        skills.Add("sandblast", 0);
    }

    new public ClayGolemEntity GetEntity()
    {
        return (ClayGolemEntity)base.GetEntity();
    }
}
