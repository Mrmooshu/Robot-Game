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
        GameObject skillTree = Instantiate(PlayerManager.instance.activeMinion.GetEntity().skillTree);
        skillTree.transform.SetParent(view);
        skillTree.transform.position = view.transform.position;
        skillTree.transform.localScale = new Vector3(1,1,1);
        skillTree.gameObject.SetActive(true);
        GetComponent<ScrollRect>().content = skillTree.GetComponent<RectTransform>();
        RefreshInfoWindow();
    }

    public void RefreshInfoWindow()
    {
        var minion = PlayerManager.instance.activeMinion;
        pointsAvailableText.text = "Skill Points: " + minion.SkillPoints;
        if (selectedSkill != null)
        {
            skillNameText.enabled = true;
            skillDescriptionText.enabled = true;
            skillLevelText.enabled = true;
            skillUpgradeButton.enabled = true;
            skillNameText.text = selectedSkill.skillName;
            skillDescriptionText.text = selectedSkill.skillDescription;
            skillLevelText.text = "Level: " +  minion.skills[selectedSkill.skillName] + "/" + selectedSkill.maxLevel;
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
        var minion = PlayerManager.instance.activeMinion;
        if (selectedSkill != null && selectedSkill.requiredSkills.ToList().TrueForAll(x => minion.skills[x.reqiredSkillName] >= x.reqiredLevel))
        {
            if (minion.skills[selectedSkill.skillName] < selectedSkill.maxLevel && minion.SkillPoints >= 1)
            {
                minion.skills[selectedSkill.skillName]++;
                minion.SkillPoints--;
                minion.GetPassiveByName(selectedSkill.skillName).Refresh();
            }
        }
    }
}
