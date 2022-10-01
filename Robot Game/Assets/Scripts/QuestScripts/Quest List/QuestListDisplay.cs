using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestListDisplay : ToggleGroup
{
    public static QuestListDisplay instance;

    private GameObject questPrefab;
    public static bool showInactiveToggle = true;
    public static bool showActiveToggle = true;
    public static bool showCompleteToggle = true;
    public static Quest selectedQuest;
    public GameObject questInfoGo;

    protected override void Start()
    {
        base.Start();
        if (instance == null)
        {
            instance = this;
        }

        questPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("QuestListOption");
        transform.parent.parent.Find("Inactive Toggle").GetComponent<Toggle>().isOn = showInactiveToggle;
        transform.parent.parent.Find("Active Toggle").GetComponent<Toggle>().isOn = showActiveToggle;
        transform.parent.parent.Find("Complete Toggle").GetComponent<Toggle>().isOn = showCompleteToggle;

        RefreshList();
    }

    protected override void OnDisable()
    {
        SetAllTogglesOff();
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
                questOption.GetComponent<Toggle>().group = this;
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
                questOption.GetComponent<Toggle>().group = this;
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
                questOption.GetComponent<Toggle>().group = this;
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
