using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingFunction : ClassFunction
{
    public FarmingFunction(MinionData host) : base(host, "Farming")
    {
        itemType = typeof(Trowl);
    }
}
