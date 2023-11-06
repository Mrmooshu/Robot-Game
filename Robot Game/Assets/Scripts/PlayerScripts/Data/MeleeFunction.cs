using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFunction : ClassFunction
{
    public MeleeFunction(MinionData host) : base(host)
    {
        itemType = typeof(MeleeWeapon);
        name = "Melee";
    }
}
