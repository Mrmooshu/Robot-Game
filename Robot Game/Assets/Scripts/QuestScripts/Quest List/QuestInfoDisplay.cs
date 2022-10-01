using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfoDisplay : MonoBehaviour
{
    public static QuestInfoDisplay instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        Quest.questStepUpdated += RefreshInfo;
        ItemInventory.inventoryUpdated += RefreshItemQuanity;
        PlayerManager.instance.playerChanged += RefreshItemQuanity;
    }

    public void OnDestroy()
    {
        Quest.questStepUpdated -= RefreshInfo;
        ItemInventory.inventoryUpdated -= RefreshItemQuanity;
        PlayerManager.instance.playerChanged -= RefreshItemQuanity;
    }

    public void RefreshInfo()
    {
        if (QuestListDisplay.selectedQuest != null)
        {
            SetInfo(QuestListDisplay.selectedQuest);
        }
    }

    public void SetInfo(Quest quest)
    {
        foreach (Transform child in transform.Find("Objectives Panel").Find("Requirements Start"))
        {
            Destroy(child.gameObject);
        }

        QuestListDisplay.selectedQuest = quest;
        switch (quest.questState)
        {
            case Quest.QuestState.inactive:
                transform.Find("Info Text").GetComponent<TextMeshProUGUI>().text = quest.info.questStartInfo;
                break;
            case Quest.QuestState.active:
                transform.Find("Info Text").GetComponent<TextMeshProUGUI>().text = quest.GetCurrentStep().info;
                break;
            case Quest.QuestState.completed:
                transform.Find("Info Text").GetComponent<TextMeshProUGUI>().text = quest.info.questPostInfo;
                break;
        }
        if (quest.GetCurrentStep() == null)
        {
            return;
        }

        if (quest.GetCurrentStep() is HaveItemsStep)
        {
            int counter = 0;
            foreach(HaveItemsStep.RequiredItem item in ((HaveItemsStep)quest.GetCurrentStep()).requiredItems)
            {
                GameObject itemDisplay = Instantiate(UIManager.instance.uiPrefabs.LoadAsset<GameObject>("ItemRequirementDisplay"), transform.Find("Objectives Panel").Find("Requirements Start"));
                itemDisplay.transform.localPosition = new Vector2(itemDisplay.transform.localPosition.x, itemDisplay.transform.localPosition.y + counter*-16);
                itemDisplay.GetComponent<Image>().sprite = item.requiredItem.sprite;
                itemDisplay.transform.Find("Quanity Requirement Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.CheckCurrentInventoryForItem(item.requiredItem.itemID) + "/" + item.requiredItemQuanity;
                counter++;
            }
        }
    }

    public void RefreshItemQuanity()
    {
        if (QuestListDisplay.selectedQuest.GetCurrentStep() == null)
        {
            return;
        }
        int counter = 0;
        foreach (HaveItemsStep.RequiredItem item in ((HaveItemsStep)QuestListDisplay.selectedQuest.GetCurrentStep()).requiredItems)
        {
            Transform itemDisplay = transform.Find("Objectives Panel").Find("Requirements Start").GetChild(counter);
            itemDisplay.transform.Find("Quanity Requirement Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.CheckCurrentInventoryForItem(item.requiredItem.itemID) + "/" + item.requiredItemQuanity;
            counter++;
        }
    }
}
