using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassFunction
{
    // number of skills each function has
    const int FUNCTIONSSKILLCOUNT = 10;

    public MinionData host;

    public string name;

    protected List<(EntityStatType, int)> uniquestats;

    public LevelData level { get; protected set; }

    public Item equipItem;

    public System.Type itemType;

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
        Initialize();
    }

    //creates skills in hosts skill dictionary
    private void Initialize()
    {
        for (int i = 1; i <= FUNCTIONSSKILLCOUNT; i++)
        {
            host.skills.Add($"{name.ToLower()}skill {i}", 0);
        }
    }

    //removes skills in hosts skill dictionary
    private void Deinitialize()
    {
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
