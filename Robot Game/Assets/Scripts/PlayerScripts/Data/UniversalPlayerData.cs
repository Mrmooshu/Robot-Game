using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class UniversalPlayerData : ISerializationCallbackReceiver
{

    [System.Serializable]
    protected struct SavedData<T>
    {
        public string name;
        public T state;

        public SavedData(string name, T state)
        {
            this.name = name;
            this.state = state;
        }
    }

    [SerializeField] private List<SavedData<int>> upgradesList;
    public Dictionary<string, int> upgrades { get; private set; }

    [SerializeField] private List<SavedData<bool>> abilityList;
    public Dictionary<string, bool> abilities { get; private set; }

    [NonSerialized] static private List<Passive> passives;

    [NonSerialized] static private List<Passive> minionPassives;

    //dynamic data
    public float ConnectionPowerCapacity { get { return
                100 +
                upgrades["ConnectionPowerCapacityUpgrade"] * 10
                ; }
    }

    // saved data
    public List<string> unlockedSafepoints = new List<string>();






    public UniversalPlayerData()
    {
        Initialize();
    }

    public void Initialize()
    {
        CreateUpgrades();
        CreateAbilities();
    }







    protected void CreateUpgrades()
    {
        // inital creating of upgrades dictionary
        if (upgrades == null)
        {
            upgrades = new Dictionary<string, int>();
            foreach (string name in upgradeNames)
            {
                upgrades.Add(name, 0);
            }
        }
        // adds new upgrades to existing dictionary
        else
        {
            foreach (string name in upgradeNames)
            {
                if (!upgrades.ContainsKey(name))
                {
                    upgrades.Add(name, 0);
                }
            }
        }
    }

    protected void CreateAbilities()
    {
        // inital creating of abilities dictionary
        if (abilities == null)
        {
            abilities = new Dictionary<string, bool>();
            foreach (string name in abilityNames)
            {
                abilities.Add(name, false);
            }
        }
        // adds new abilities to existing dictionary
        else
        {
            foreach (string name in abilityNames)
            {
                if (!abilities.ContainsKey(name))
                {
                    abilities.Add(name, false);
                }
            }
        }
    }

    public void InitializePassives()
    {
        minionPassives = new List<Passive>();
        minionPassives.Add(new PassiveStat("HealthPassive", new List<Entity> { }, StatModType.Base, EntityStatType.Health, 1));
    }

    public void AddMinionPassives(MinionEntity minion)
    {
        foreach (Passive passive in minionPassives)
        {
            passive.AddEntity(minion);
        }
    }

    public void RemoveMinionPassives(MinionEntity minion)
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
            upgrades = upgradesList.ToDictionary(x => x.name, x => x.state);
        }
        if (abilityList != null)
        {
            abilities = abilityList.ToDictionary(x => x.name, x => x.state);
        }
    }

    public void OnBeforeSerialize()
    {
        try
        {
            if (upgrades != null)
            {
                upgradesList = upgrades.Select(x => new SavedData<int>(x.Key, x.Value)).ToList();
            }
        }
        catch (Exception)
        {

        }
        try
        {
            if (abilities != null)
            {
                abilityList = abilities.Select(x => new SavedData<bool>(x.Key, x.Value)).ToList();
            }
        }
        catch (Exception)
        {

        }
    }



    private List<string> upgradeNames = new List<string>()
    {
        "ConnectionPowerCapacityUpgrade",
        "HealthPassive"



    };

    private List<string> abilityNames = new List<string>()
    {
        //General Abilities
        "RoamingCancel",
        // Golem Abilites
        "Tornado"
        // more go here

    };
}
