using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive
{
    protected Entity entity;

    public string skillName;

    public abstract void Refresh();
}
