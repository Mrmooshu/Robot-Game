using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUpgrade : MonoBehaviour
{
    [Serializable]
    public struct requiredSkill
    {
        public string reqiredSkillName;
        public int reqiredLevel;
    }

    [Header("Skill info")]
    public string skillName;
    public string skillDescription;
    public int maxLevel;
    public requiredSkill[] requiredSkills;

    private Button button { get => GetComponent<Button>(); }

    private void Start()
    {
        button.onClick.AddListener(SelectSkill);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(SelectSkill);
    }

    public void SelectSkill()
    {
        SkillTreeDisplay.instance.selectedSkill = this;
        SkillTreeDisplay.instance.RefreshInfoWindow();
    }
}