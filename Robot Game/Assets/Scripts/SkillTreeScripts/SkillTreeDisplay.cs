using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkillTreeDisplay : MonoBehaviour
{
    public Transform view;
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI skillDescriptionText;
    public TextMeshProUGUI skillLevelText;
    public TextMeshProUGUI pointsAvailableText;
    public Button skillUpgradeButton;

    public static SkillTreeDisplay instance;

    public SkillTreeUpgrade selectedSkill = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PlayerManager.instance.minionChanged += Refresh;
        skillUpgradeButton.onClick.AddListener(UpgradeCurrentSkill);
        MinionData.skillPointsUpdated += RefreshInfoWindow;
        Refresh();
    }

    private void OnDestroy()
    {
        PlayerManager.instance.minionChanged -= Refresh;
        skillUpgradeButton.onClick.RemoveListener(UpgradeCurrentSkill);
        MinionData.skillPointsUpdated -= RefreshInfoWindow;
    }

    private void OnEnable()
    {
        RefreshInfoWindow();
    }

    public void Refresh()
    {
        foreach (Transform child in view.transform)
        {
            child.SetParent(child.GetComponent<HoldParent>().parentToHold.transform);
            child.gameObject.SetActive(false);
        }
        GameObject skillTree = PlayerManager.instance.activeMinion.GetEntity().skillTree;
        skillTree.transform.SetParent(view);
        skillTree.transform.position = view.transform.position;
        skillTree.transform.localScale = new Vector3(1,1,1);
        skillTree.gameObject.SetActive(true);
        GetComponent<ScrollRect>().content = skillTree.GetComponent<RectTransform>();
        RefreshInfoWindow();
    }

    public void RefreshInfoWindow()
    {
        pointsAvailableText.text = "Skill Points: " + PlayerManager.instance.activeMinion.SkillPoints;
        if (selectedSkill != null)
        {
            skillNameText.enabled = true;
            skillDescriptionText.enabled = true;
            skillLevelText.enabled = true;
            skillUpgradeButton.enabled = true;
            skillNameText.text = selectedSkill.passive.abilityName;
            skillDescriptionText.text = selectedSkill.passive.abilityDescription;
            skillLevelText.text = "Level: " +  PlayerManager.instance.activeMinion.skills[selectedSkill.passive.abilityName] + "/" + selectedSkill.maxLevel;
            skillUpgradeButton.onClick.RemoveAllListeners();
            skillUpgradeButton.onClick.AddListener(UpgradeCurrentSkill);
        }
        else
        {
            skillNameText.enabled = false;
            skillDescriptionText.enabled = false;
            skillLevelText.enabled = false;
            skillUpgradeButton.enabled = false;
        }
    }

    private void UpgradeCurrentSkill()
    {
        if (selectedSkill != null && selectedSkill.requiredSkills.ToList().TrueForAll(x => PlayerManager.instance.activeMinion.skills[x.reqiredSkill.passive.abilityName] >= x.reqiredLevel))
        {
            if (PlayerManager.instance.activeMinion.skills[selectedSkill.passive.abilityName] < selectedSkill.maxLevel && PlayerManager.instance.activeMinion.SkillPoints >= 1)
            {
                PlayerManager.instance.activeMinion.skills[selectedSkill.passive.abilityName]++;
                PlayerManager.instance.activeMinion.SkillPoints--;
                selectedSkill.passive.Refresh();
            }
        }
    }
}
