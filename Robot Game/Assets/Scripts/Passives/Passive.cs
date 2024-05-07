using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive
{
    protected MinionEntity host;

    public string passiveName;

    public virtual void ChangeEntity(MinionEntity entity)
    {
        if (entity != null)
        {
            host = entity;
        }
    }

    public virtual void RemoveEntity()
    {
        host.passives.Remove(this);
        host = null;
    }
}
