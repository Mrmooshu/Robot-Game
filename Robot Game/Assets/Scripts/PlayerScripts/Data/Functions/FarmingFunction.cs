using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingFunction : ClassFunction
{
    public override Type itemType { get { return typeof(Trowl); } }

    protected override List<(EntityStatType, int)> uniquestats { get { return new List<(EntityStatType, int)> { }; } }

    public FarmingFunction(MinionData host) : base(host, "Farming")
    {
    }
}
