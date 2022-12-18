using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class PlayerBody : ISerializationCallbackReceiver
{
    [System.Serializable]
    private struct skill
    {
        public string name;
        public int level;

        public skill(string name, int level)
        {
            this.name = name;
            this.level = level;
        }
    }

    public string variantName;
    public int level = 1;
    public float health = 1;
    public Item weapon;
    public Item tool;
    public Dictionary<string,int> skills;
    [SerializeField] private int _skillPoints;
    public int SkillPoints { get { return _skillPoints; } set { _skillPoints = value; skillPointsUpdated?.Invoke(); } }
    public static event Action skillPointsUpdated;

    // used to save skills as a list in json
    [SerializeField] private List<skill> skillsList;

    public PlayerBody(string variantName)
    {
        this.variantName = variantName;
        // create fresh skill tree values from variants skill tree
        skills = new Dictionary<string, int>();
        SkillPoints = 10;
        foreach (SkillUpgradeDisplay skill in Database.GetVariant(variantName).skillTree.GetComponentsInChildren<SkillUpgradeDisplay>())
        {
            skills.Add(skill.skill.abilityName, 0);
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
        if (skills != null)
        {
            skillsList = skills.Select(x => new skill(x.Key,x.Value)).ToList();
        }
    }
}
