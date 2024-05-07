using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestListDisplay : SelectableMenu<Quest>
{
    public static QuestListDisplay instance;

    private Transform infoTransform;

    protected override void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        base.Start();
        infoTransform = infoObject.transform;
        Quest.questStepUpdated += RefreshInfo;
        ItemInventory.inventoryUpdated += RefreshItemQuanity;
        PlayerManager.instance.minionChanged += RefreshItemQuanity;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Quest.questStepUpdated -= RefreshInfo;
        ItemInventory.inventoryUpdated -= RefreshItemQuanity;
        PlayerManager.instance.minionChanged -= RefreshItemQuanity;
    }

    public override void RefreshList()
    {
        base.RefreshList();

        if (toggles[0].isOn || !Filtered)
        {
            foreach (Quest quest in QuestManager.instance.inactiveQuests)
            {
                CreateQuestOption(quest, new Color(255, 0, 0));
            }
        }
        if (toggles[1].isOn || !Filtered)
        {
            foreach (Quest quest in QuestManager.instance.activeQuests)
            {
                CreateQuestOption(quest, new Color(255, 255, 0));
            }
        }
        if (toggles[2].isOn || !Filtered)
        {
            foreach (Quest quest in QuestManager.instance.completeQuests)
            {
                CreateQuestOption(quest, new Color(0, 255, 0));
            }
        }

        void CreateQuestOption(Quest quest, Color color)
        {
            GameObject questOption = Instantiate(selectPrefab, transform);
            questOption.GetComponent<TextMeshProUGUI>().color = color;
            questOption.GetComponent<TextMeshProUGUI>().text = quest.info.questName;
            questOption.GetComponent<QuestListOption>().quest = quest;
            questOption.GetComponent<Toggle>().group = this;
        }
    }

    public override void RefreshInfo()
    {
        base.RefreshInfo();
        if (CurrentSelected != null)
        {
            foreach (Transform child in infoObject.transform.Find("Objectives Panel").Find("Requirements Start"))
            {
                Destroy(child.gameObject);
            }

            switch (CurrentSelected.questState)
            {
                case Quest.QuestState.inactive:
                    infoTransform.Find("Info Text").GetComponent<TextMeshProUGUI>().text = CurrentSelected.info.questStartInfo;
                    break;
                case Quest.QuestState.active:
                    infoTransform.Find("Info Text").GetComponent<TextMeshProUGUI>().text = CurrentSelected.GetCurrentStep().info;
                    break;
                case Quest.QuestState.completed:
                    infoTransform.Find("Info Text").GetComponent<TextMeshProUGUI>().text = CurrentSelected.info.questPostInfo;
                    break;
            }
            if (CurrentSelected.GetCurrentStep() == null)
            {
                return;
            }

            if (CurrentSelected.GetCurrentStep() is HaveItemsStep)
            {
                int counter = 0;
                foreach (HaveItemsStep.RequiredItem item in ((HaveItemsStep)CurrentSelected.GetCurrentStep()).requiredItems)
                {
                    GameObject itemDisplay = Instantiate(UIManager.instance.uiPrefabs.LoadAsset<GameObject>("ItemRequirementDisplay"), infoTransform.Find("Objectives Panel").Find("Requirements Start"));
                    itemDisplay.transform.localPosition = new Vector2(itemDisplay.transform.localPosition.x, itemDisplay.transform.localPosition.y + counter * -16);
                    itemDisplay.GetComponent<Image>().sprite = item.requiredItem.sprite;
                    itemDisplay.transform.Find("Quanity Requirement Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.CheckCurrentInventoryForItem(item.requiredItem.itemID) + "/" + item.requiredItemQuanity;
                    counter++;
                }
            }
        }
    }

    public void RefreshItemQuanity()
    {
        if (CurrentSelected == null)
        {
            return;
        }
        if (CurrentSelected.GetCurrentStep() == null)
        {
            return;
        }
        int counter = 0;
        foreach (HaveItemsStep.RequiredItem item in ((HaveItemsStep)CurrentSelected.GetCurrentStep()).requiredItems)
        {
            Transform itemDisplay = infoTransform.Find("Objectives Panel").Find("Requirements Start").GetChild(counter);
            itemDisplay.transform.Find("Quanity Requirement Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.CheckCurrentInventoryForItem(item.requiredItem.itemID) + "/" + item.requiredItemQuanity;
            counter++;
        }
    }
}
