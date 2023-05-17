using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MinionData : ISerializationCallbackReceiver
{
    public enum Activity
    {
        Idle, Mining, Woodcutting, Fishing
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

    //Saved variables
    public string lastSafePoint;
    public string variantName;
    [SerializeReference] public Item tool;
    [SerializeReference] public Item weapon;
    public int level = 1;
    public ItemInventory inventory;
    public Vector3 savedPosition;
    public Activity activity = Activity.Idle;
    // used to save skills as a list in json
    [SerializeField] private List<skill> skillsList;
    [SerializeField] private int _skillPoints;
    public int SkillPoints { get { return _skillPoints; } set { _skillPoints = value; skillPointsUpdated?.Invoke(); } }


    //Unsaved variables
    [NonSerialized]public List<Passive> passives;
    public Dictionary<string, int> skills;


    public static event Action skillPointsUpdated;

    public MinionData(string variantName)
    {
        this.variantName = variantName;
        CreateSkills(variantName);
        inventory = new ItemInventory(20, 10);
    }


    public MinionEntity GetEntity()
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

    public Passive GetPassiveByName(string skillName)
    {
        return passives.FirstOrDefault(x => x.skillName == skillName);
    }

    protected virtual void CreateSkills(string prefabName)
    {
    }

    public virtual void InitializePassives()
    {

    }


    public virtual void OnAfterDeserialize()
    {
        if (skillsList != null)
        {
            skills = skillsList.ToDictionary(x => x.name, x => x.level);
        }
    }

    public void OnBeforeSerialize()
    {
        try
        {
            savedPosition = GetEntity().transform.position;
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
