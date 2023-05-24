using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class UniversalPlayerData : ISerializationCallbackReceiver
{
    [System.Serializable]
    protected struct Upgrade
    {
        public string name;
        public int level;

        public Upgrade(string name, int level)
        {
            this.name = name;
            this.level = level;
        }
    }

    [SerializeField] private List<Upgrade> upgradesList;
    public static Dictionary<string, int> upgrades;

    [NonSerialized] static private List<Passive> passives;

    [NonSerialized] static private List<Passive> minionPassives;

    public float ConnectionPowerCapacity { get { return
                100 +
                upgrades["ConnectionPowerCapacityUpgrade"] * 10
                ; } }






    public UniversalPlayerData()
    {
        CreateUpgrades();
    }









    protected void CreateUpgrades()
    {
        upgrades = new Dictionary<string, int>()
        {
            { "ConnectionPowerCapacityUpgrade", 0},
            { "health passive", 5}
        };
    }

    public static void InitializePassives()
    {
        minionPassives = new List<Passive>();
        minionPassives.Add(new PassiveStat("health passive", new List<Entity> { }, StatModType.Base, EntityStatType.Health, 1));
    }

    public static void AddMinionPassives(MinionEntity minion)
    {
        foreach (Passive passive in minionPassives)
        {
            passive.AddEntity(minion);
        }
    }

    public static void RemoveMinionPassives(MinionEntity minion)
    {
        foreach (Passive passive in minionPassives)
        {
            passive.RemoveEntity(minion);
        }
    }

    public void OnAfterDeserialize()
    {
        if (upgradesList != null)
        {
            upgrades = upgradesList.ToDictionary(x => x.name, x => x.level);
        }
    }

    public void OnBeforeSerialize()
    {
        try
        {
            if (upgrades != null)
            {
                upgradesList = upgrades.Select(x => new Upgrade(x.Key, x.Value)).ToList();
            }
        }
        catch (Exception)
        {

        }
    }
}
