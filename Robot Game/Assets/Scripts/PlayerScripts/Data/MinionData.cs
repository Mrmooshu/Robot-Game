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

    public string lastSafePoint;
    public string variantName;
    [SerializeReference] public Item tool;
    [SerializeReference] public Item weapon;

    public int level = 1;
    public ItemInventory inventory;
    public Vector3 savedPosition;
    public Activity activity = Activity.Idle;

    public MinionData(string variantName)
    {
        this.variantName = variantName;
        CreateSkillTree(variantName);
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

    public Dictionary<string, int> skills;
    [SerializeField] private int _skillPoints;
    public int SkillPoints { get { return _skillPoints; } set { _skillPoints = value; skillPointsUpdated?.Invoke(); } }
    public static event Action skillPointsUpdated;

    // used to save skills as a list in json
    [SerializeField] private List<skill> skillsList;

    protected void CreateSkillTree(string prefabName)
    {
        // create fresh skill tree values from variants skill tree
        skills = new Dictionary<string, int>();
        SkillPoints = 100;
        foreach (Passive skill in GeneralManager.instance.entityPrefabs.LoadAsset<GameObject>(prefabName).GetComponent<MinionEntity>().skillTree.GetComponentsInChildren<Passive>())
        {
            skills.Add(skill.abilityName, 0);
        }
    }


    public void OnAfterDeserialize()
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
