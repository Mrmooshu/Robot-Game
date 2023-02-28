using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class MinionEntity : UnitEntity
{
    [NonSerialized]public MinionData data;

    public virtual void Initialize(MinionData data)
    {
        this.data = data;
        base.Initialize();
    }

    public abstract void ToolAction();
}