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
        public SkillTreeUpgrade reqiredSkill;
        public int reqiredLevel;
    }

    [Header("Skill info")]
    public int maxLevel;
    public requiredSkill[] requiredSkills;
    public Passive passive;

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