using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassFunction
{
    public MinionData host;

    public string name;

    public LevelData level { get; protected set; }

    public Item equipItem;

    public System.Type itemType;

    public ClassFunction(MinionData host)
    {
        equipItem = null;
        level = new LevelData(host);
        this.host = host;
    }

    public void ChangeHost(MinionData newHost)
    {
        host = newHost;
    }
}
