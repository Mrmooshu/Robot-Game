using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public abstract class ClassFunction
{
    // number of skills each function has
    const int FUNCTIONSSKILLCOUNT = 10;

    public MinionData host;

    public string name;

    protected abstract List<(EntityStatType, int)> uniquestats { get; }

    public LevelData level;

    [SerializeReference] public Item equipItem;

    public abstract Type itemType { get; }

    public ClassFunction(MinionData host, string name)
    {
        this.name = name;
        equipItem = null;
        level = new LevelData(host);
        this.host = host;
        Initialize();
    }

    public void ChangeHost(MinionData newHost)
    {
        Deinitialize();
        host = newHost;
        level.data = host;
        Initialize();
    }

    //creates skills in hosts skill dictionary
    private void Initialize()
    {
        for (int i = 1; i <= FUNCTIONSSKILLCOUNT; i++)
        {
            var key = $"{name.ToLower()}skill {i}";
            if (!host.skills.ContainsKey(key))
            {
                host.skills.Add(key, 0);
            }
        }
    }

    //removes skills in hosts skill dictionary
    private void Deinitialize()
    {
        if (host == null)
        {
            return;
        }
        if (host.GetEntity() == null)
        {
            return;
        }
        foreach ((EntityStatType, int) stat in uniquestats)
        {
            host.GetEntity().stats.Remove(stat.Item1);
        }

        for (int i = 1; i <= FUNCTIONSSKILLCOUNT; i++)
        {
            host.skills.Remove($"{name.ToLower()}skill {i}");
        }
    }

    public void AddStats()
    {
        if (uniquestats == null)
        {
            return;
        }

        foreach ((EntityStatType, int) stat in uniquestats)
        {
            host.GetEntity().stats.Add(stat.Item1, new Stat(stat.Item2));
        }
    }

    public virtual void InitializePassives()
    {

    }
}
