using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeDisplay : MonoBehaviour
{
    public SkillTreeUpgrade skill;

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
