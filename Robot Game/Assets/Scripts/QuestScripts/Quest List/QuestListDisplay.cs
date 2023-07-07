using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestListDisplay : ToggleGroup
{
    public static QuestListDisplay instance;

    public GameObject questPrefab;
    public Toggle InactiveToggle;
    public Toggle ActiveToggle;
    public Toggle CompleteToggle;
    public static Quest selectedQuest;
    public GameObject questInfoGo;

    protected override void Start()
    {
        base.Start();
        if (instance == null)
        {
            instance = this;
        }
        InactiveToggle.onValueChanged.AddListener(delegate { instance.RefreshList(); });
        ActiveToggle.onValueChanged.AddListener(delegate { instance.RefreshList(); });
        CompleteToggle.onValueChanged.AddListener(delegate { instance.RefreshList(); });
        RefreshList();
    }

    protected override void OnDestroy()
    {
        InactiveToggle.onValueChanged.RemoveAllListeners();
        ActiveToggle.onValueChanged.RemoveAllListeners();
        CompleteToggle.onValueChanged.RemoveAllListeners();
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

        if (InactiveToggle.isOn)
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
        if (ActiveToggle.isOn)
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
        if (CompleteToggle.isOn)
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
}
