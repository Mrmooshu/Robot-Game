using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public abstract class MinionData : ISerializationCallbackReceiver
{
    private LevelData level;
    public LevelData Level { get => level; private set { level = value; } }

    public enum Activity
    {
        Storage, Idle, Mining, Woodcutting, Fishing
    }

    public enum CombatStyle
    {
        None,Melee,Range,Magic
    }

    [System.Serializable]
    protected struct skill
    {
        public string name;
        public int level;

        public skill(string name, int level)
        {
            this.name = name;
            this.level = level;
        }
    }

    [System.Serializable]
    public struct active
    {
        public string name;
        public float cooldown;
        public int stage;

        public active(string name, float cooldown)
        {
            this.name = name;
            this.cooldown = cooldown;
            stage = 0;
        }
    }

    //Saved variables
    public ClassFunction[] functions;
    public string lastSafePoint;
    public string variantName;
    [SerializeReference] public Item[] artifacts;
    public ItemInventory inventory;
    public active[] ActiveAbilities;
    public Vector3 savedPosition;
    public Activity activity = Activity.Storage;
    public CombatStyle style = CombatStyle.None;
    // used to save skills as a list in json
    [SerializeField] private List<skill> skillsList;
    [SerializeField] private int _skillPoints;
    public int SkillPoints { get { return _skillPoints; } set { _skillPoints = value; skillPointsUpdated?.Invoke(); } }


    //Unsaved variables
    [NonSerialized]public List<Passive> passives;
    public Dictionary<string, int> skills;


    public static event Action skillPointsUpdated;

    public MinionData()
    {
        Create();
        CreateSkills();
    }


    public virtual MinionEntity GetEntity()
    {
        foreach (MinionEntity minion in PlayerManager.instance.minionEntities)
        {
            if (minion.data == this)
            {
                return minion;
            }
        }
        return null;
    }

    public bool HasFunction<T>()
    {
        return functions.Any(x => x is T);
    }

    public ClassFunction GetFunction<T>()
    {
        return functions.First(x => x is T);
    }

    public Passive GetPassiveByName(string skillName)
    {
        return passives.FirstOrDefault(x => x.skillName == skillName);
    }

    protected virtual void Create()
    {
        Level = new LevelData();
        functions = new ClassFunction[5];
        artifacts = new Item[6];
        inventory = new ItemInventory(30, 10);
        ActiveAbilities = new active[6] { new active("", 0), new active("", 0), new active("", 0), new active("", 0), new active("", 0), new active("", 0) };
    }

    protected virtual void CreateSkills()
    {
    }

    public virtual void InitializePassives()
    {

    }


    //Serialiszation Functions
    public virtual void OnAfterDeserialize()
    {
        if (skillsList != null)
        {
            skills = skillsList.ToDictionary(x => x.name, x => x.level);
        }
        // reset active stages
        ActiveAbilities.ToList().ForEach(i => i.stage = 0);
    }

    public void OnBeforeSerialize()
    {
        try
        {
            if (activity != Activity.Storage)
            {
                savedPosition = GetEntity().transform.position;
            }
            if (skills != null)
            {
                skillsList = skills.Select(x => new skill(x.Key, x.Value)).ToList();
            }
        }
        catch (Exception)
        {

        }
    }
}
