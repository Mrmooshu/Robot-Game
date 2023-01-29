using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility
{
    public float coolDown;
    public float cost;
    public float costType;
    public abstract void Action();
}
