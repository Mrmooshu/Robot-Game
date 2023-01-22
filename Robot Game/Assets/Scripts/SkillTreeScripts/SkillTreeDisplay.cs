using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        PlayerManager.instance.playerChanged += Refresh;
        skillUpgradeButton.onClick.AddListener(UpgradeCurrentSkill);
        PlayerBody.skillPointsUpdated += RefreshInfoWindow;
        Refresh();
    }

    private void OnDestroy()
    {
        PlayerManager.instance.playerChanged -= Refresh;
        skillUpgradeButton.onClick.RemoveListener(UpgradeCurrentSkill);
        PlayerBody.skillPointsUpdated -= RefreshInfoWindow;
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
        GameObject skillTree = PlayerManager.instance.activeCore.GetPlayer().skillTree;
        skillTree.transform.SetParent(view);
        skillTree.transform.position = view.transform.position;
        skillTree.gameObject.SetActive(true);
        GetComponent<ScrollRect>().content = skillTree.GetComponent<RectTransform>();
        RefreshInfoWindow();
    }

    public void RefreshInfoWindow()
    {
        pointsAvailableText.text = "Skill Points: " + PlayerManager.instance.activeCore.currentBody.SkillPoints;
        if (selectedSkill != null)
        {
            skillNameText.enabled = true;
            skillDescriptionText.enabled = true;
            skillLevelText.enabled = true;
            skillUpgradeButton.enabled = true;
            skillNameText.text = selectedSkill.passive.abilityName;
            skillDescriptionText.text = selectedSkill.passive.abilityDescription;
            skillLevelText.text = "Level: " +  PlayerManager.instance.activeCore.currentBody.skills[selectedSkill.passive.abilityName] + "/" + selectedSkill.maxLevel;
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
        if (selectedSkill != null)
        {
            if (PlayerManager.instance.activeCore.currentBody.skills[selectedSkill.passive.abilityName] < selectedSkill.maxLevel && PlayerManager.instance.activeCore.currentBody.SkillPoints >= 1)
            {
                PlayerManager.instance.activeCore.currentBody.skills[selectedSkill.passive.abilityName]++;
                PlayerManager.instance.activeCore.currentBody.SkillPoints--;
                //selectedSkill.Refresh();
            }
        }
    }
}
