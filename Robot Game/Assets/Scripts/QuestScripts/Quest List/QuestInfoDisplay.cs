using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfoDisplay : MonoBehaviour
{
    public static QuestInfoDisplay instance;

    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetInfo(Quest quest)
    {
        QuestListDisplay.selectedQuest = quest;
        transform.Find("Info Text").GetComponent<TextMeshProUGUI>().text = quest.info.questInfo;
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
}
