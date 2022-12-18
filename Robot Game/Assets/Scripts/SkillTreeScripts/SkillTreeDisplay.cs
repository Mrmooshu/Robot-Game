using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeDisplay : MonoBehaviour
{

    public Transform view;
    public TextMeshProUGUI skillLevelText;
    public TextMeshProUGUI skillDescriptionText;
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
            Destroy(child.gameObject);
        }
        GameObject skillTree = Instantiate(Database.GetVariant(PlayerManager.instance.activeCore.currentBody.variantName).skillTree, view);
        GetComponent<ScrollRect>().content = skillTree.GetComponent<RectTransform>();
        RefreshInfoWindow();
    }

    public void RefreshInfoWindow()
    {
        pointsAvailableText.text = "" + PlayerManager.instance.activeCore.currentBody.SkillPoints;
        if (selectedSkill != null)
        {
            skillLevelText.text = PlayerManager.instance.activeCore.currentBody.skills[selectedSkill.abilityName] + "/" + selectedSkill.maxLevel;
            skillDescriptionText.text = selectedSkill.abilityDescription;
            skillUpgradeButton.onClick.RemoveAllListeners();
            skillUpgradeButton.onClick.AddListener(UpgradeCurrentSkill);
        }
    }

    private void UpgradeCurrentSkill()
    {
        if (selectedSkill != null)
        {
            if (PlayerManager.instance.activeCore.currentBody.skills[selectedSkill.abilityName] < selectedSkill.maxLevel && PlayerManager.instance.activeCore.currentBody.SkillPoints >= 1)
            {
                PlayerManager.instance.activeCore.currentBody.skills[selectedSkill.abilityName]++;
                PlayerManager.instance.activeCore.currentBody.SkillPoints--;
            }
        }
    }
}
