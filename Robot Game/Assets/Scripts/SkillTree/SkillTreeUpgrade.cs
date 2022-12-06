using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillTree/SkillUpgrade", fileName = "Skill")]
public class SkillTreeUpgrade : ScriptableObject
{
    [Serializable]
    public struct requiredSkill
    {
        public SkillTreeUpgrade reqiredSkill;
        public int reqiredLevel;
    }

    [Header("Skill info")]
    public string abilityName;
    public string abilityDescription;
    public requiredSkill[] requiredSkills;
    public int maxLevel;


}