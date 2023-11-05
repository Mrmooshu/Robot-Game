using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestListDisplay : ToggleGroup
{
    public static QuestListDisplay instance;

    public GameObject questPrefab;
    public Toggle InactiveToggle;
    public Toggle ActiveToggle;
    public Toggle CompleteToggle;
    public static Quest selectedQuest;
    public GameObject questInfoGo;
    public bool Filtered { get { return new List<bool>(){ InactiveToggle.isOn, ActiveToggle.isOn, CompleteToggle.isOn}.Any(x => x == true); } }

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

        if (InactiveToggle.isOn || !Filtered)
        {
            foreach (Quest quest in QuestManager.instance.inactiveQuests)
            {
                CreateQuestOption(quest, new Color(255, 0, 0));
            }
        }
        if (ActiveToggle.isOn || !Filtered)
        {
            foreach (Quest quest in QuestManager.instance.activeQuests)
            {
                CreateQuestOption(quest, new Color(255, 255, 0));
            }
        }
        if (CompleteToggle.isOn || !Filtered)
        {
            foreach (Quest quest in QuestManager.instance.completeQuests)
            {
                CreateQuestOption(quest, new Color(0, 255, 0));
            }
        }

        void CreateQuestOption(Quest quest, Color color)
        {
            GameObject questOption = Instantiate(questPrefab, transform);
            questOption.GetComponent<TextMeshProUGUI>().color = color;
            questOption.GetComponent<TextMeshProUGUI>().text = quest.info.questName;
            questOption.GetComponent<QuestListOption>().quest = quest;
            questOption.GetComponent<Toggle>().group = this;
        }
    }
}
