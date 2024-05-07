using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithingFunction : ClassFunction
{
    public override Type itemType { get { return typeof(Hammer); } }

    protected override List<(EntityStatType, int)> uniquestats { get { return new List<(EntityStatType, int)> {  }; } }

    public SmithingFunction(MinionData host) : base(host,"Smithing") { }
}
