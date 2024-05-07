using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class UniversalPlayerData : ISerializationCallbackReceiver
{
    public struct minionunlock
    {
        public Type minionType;
        public bool unlocked;
        public string name;

        public minionunlock(Type type, string name, bool unlocked = false)
        {
            minionType = type;
            this.unlocked = unlocked;
            this.name = name;
        }
    }

    [SerializeField] private List<UniversalHelperFunctions.SavedData<int>> upgradesList;
    public Dictionary<string, int> upgrades { get; private set; }

    [SerializeField] private List<UniversalHelperFunctions.SavedData<bool>> abilityList;
    public Dictionary<string, bool> abilities { get; private set; }

    [NonSerialized] static private List<Passive> passives;

    //dynamic data
    public float ConnectionPowerCapacity { get { return
                100 +
                upgrades["ConnectionPowerCapacityUpgrade"] * 10
                ; }
    }

    // saved data
    public List<string> unlockedSafepoints = new List<string>();

    public List<minionunlock> minionsUnlocked;

    public UniversalPlayerData()
    {
        Initialize();
    }

    public void Initialize()
    {
        CreateUpgrades();
        CreateAbilities();
        minionsUnlocked = new List<minionunlock>
        {
            new minionunlock(typeof(ClayGolemData), "Clay Golem"),
            new minionunlock(typeof(WoodSentinelData), "Wood Sentinel"),
            new minionunlock(typeof(MagmaTitanData), "Magma Titan")
        };
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

    public void AddMinionPassives(MinionEntity minion)
    {
        RebirthPassives(minion);
    }

    public void OnAfterDeserialize()
    {
        if (upgradesList != null)
        {
            upgrades = upgradesList.ToDictionary(x => x.name, x => x.data);
        }
        if (abilityList != null)
        {
            abilities = abilityList.ToDictionary(x => x.name, x => x.data);
        }
    }

    public void OnBeforeSerialize()
    {
        try
        {
            if (upgrades != null)
            {
                upgradesList = upgrades.Select(x => new UniversalHelperFunctions.SavedData<int>(x.Key, x.Value)).ToList();
            }
        }
        catch (Exception)
        {

        }
        try
        {
            if (abilities != null)
            {
                abilityList = abilities.Select(x => new UniversalHelperFunctions.SavedData<bool>(x.Key, x.Value)).ToList();
            }
        }
        catch (Exception)
        {

        }
    }



    private List<string> upgradeNames = new List<string>()
    {
        "ConnectionPowerCapacityUpgrade",
        "HealthPassive",
        // highest level rebirthed for class
        "Clay Golem RebirthLevel",
        "Wood Sentinel RebirthLevel",
        "Magma Titan RebirthLevel"



    };

    private List<string> abilityNames = new List<string>()
    {
        //General Abilities
        "RoamingCancel",
        // Golem Abilites
        "Tornado"
        // more go here

    };

    private void RebirthPassives(MinionEntity entity)
    {
        //Clay Golem
        AssignPassives(new List<(Passive, (Type, int))>
        {
        (new PassiveStat("ClayGolemRebirthLevel10", StatModType.Additive, EntityStatType.health, () => 5), (typeof(MinionData), ClayGolemRebirthInfo[0].unlock)),
        (new PassiveStat("ClayGolemRebirthLevel20", StatModType.Flat, EntityStatType.damagepower, () => entity.stats[EntityStatType.health].Value * .01f), (typeof(GolemData), ClayGolemRebirthInfo[1].unlock))
        }, "ClayGolemRebirthLevel");










        void AssignPassives(List<(Passive, (Type, int))> passives, string upgrade)
        {
            foreach ((Passive, (Type, int)) passive in passives)
            {
                if (entity.GetType().IsAssignableFrom(passive.Item2.Item1) && upgrades[upgrade] >= passive.Item2.Item2)
                {
                    passive.Item1.ChangeEntity(entity);
                }
            }
        }
    }

    private RebirthInfo[] ClayGolemRebirthInfo
    {
        get
        {
            return new RebirthInfo[]{
                new RebirthInfo("All units gain 5% incresed health", 10),
                new RebirthInfo("Golems gain 1% max health as damage", 20)
            };
        }
    }

    public struct RebirthInfo
    {
        public string description;
        public int unlock;

        public RebirthInfo(string d, int u)
        {
            description = d;
            unlock = u;
        }
    }

}
