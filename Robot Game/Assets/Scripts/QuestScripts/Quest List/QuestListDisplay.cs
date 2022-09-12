using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestListDisplay : MonoBehaviour
{
    public static QuestListDisplay instance;

    private GameObject questPrefab;
    public static bool showInactiveToggle = true;
    public static bool showActiveToggle = true;
    public static bool showCompleteToggle = true;
    public static Quest selectedQuest;
    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        questPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("QuestListOption");
        transform.parent.parent.parent.Find("QuestInfoPanel").Find("Inactive Toggle").GetComponent<Toggle>().isOn = showInactiveToggle;
        transform.parent.parent.parent.Find("QuestInfoPanel").Find("Active Toggle").GetComponent<Toggle>().isOn = showActiveToggle;
        transform.parent.parent.parent.Find("QuestInfoPanel").Find("Complete Toggle").GetComponent<Toggle>().isOn = showCompleteToggle;

        if (selectedQuest != null)
        {
            transform.parent.parent.parent.Find("QuestInfoPanel").Find("Info Text").GetComponent<TextMeshProUGUI>().text = selectedQuest.info.questInfo;
        }

        RefreshList();
    }

    public virtual void RefreshList()
    {
        CreateList();
    }

    protected void CreateList()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (showInactiveToggle)
        {
            foreach (Quest quest in QuestManager.instance.inactiveQuests)
            {
                GameObject questOption =  Instantiate(questPrefab, transform);
                questOption.GetComponent<TextMeshProUGUI>().color = new Color(255,0,0);
                questOption.GetComponent<TextMeshProUGUI>().text = quest.info.questName;
                questOption.GetComponent<QuestListOption>().quest = quest;
            }
        }
        if (showActiveToggle)
        {
            foreach (Quest quest in QuestManager.instance.activeQuests)
            {
                GameObject questOption = Instantiate(questPrefab, transform);
                questOption.GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 0);
                questOption.GetComponent<TextMeshProUGUI>().text = quest.info.questName;
                questOption.GetComponent<QuestListOption>().quest = quest;
            }
        }
        if (showCompleteToggle)
        {
            foreach (Quest quest in QuestManager.instance.completeQuests)
            {
                GameObject questOption = Instantiate(questPrefab, transform);
                questOption.GetComponent<TextMeshProUGUI>().color = new Color(0, 255, 0);
                questOption.GetComponent<TextMeshProUGUI>().text = quest.info.questName;
                questOption.GetComponent<QuestListOption>().quest = quest;
            }
        }
    }

    public void SetInactiveToggle(bool value)
    {
        showInactiveToggle = value;
        if (instance != null)
        {
            RefreshList();
        }
    }
    public void SetActiveToggle(bool value)
    {
        showActiveToggle = value;
        if (instance != null)
        {
            RefreshList();
        }
    }
    public void SetCompleteToggle(bool value)
    {
        showCompleteToggle = value;
        if (instance != null)
        {
            RefreshList();
        }
    }
}
