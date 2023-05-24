using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive
{
    protected enum passiveType
    {
        single, multiple
    }

    protected Entity entity;
    protected List<Entity> entities;

    protected passiveType type;

    public string skillName;


    public abstract void Refresh();

    public abstract void AddEntity(Entity entity);

    public abstract void RemoveEntity(Entity entity);
}
