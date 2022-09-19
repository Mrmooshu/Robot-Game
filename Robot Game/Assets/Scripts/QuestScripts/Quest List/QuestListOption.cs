using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListOption : Toggle
{
    public Quest quest;

    protected override void Awake()
    {
        onValueChanged.AddListener(delegate { Selected(this); });
    }

    public void Selected(Toggle t)
    {
        if (t.group.AnyTogglesOn())
        {
            QuestListDisplay.instance.questInfoGo.SetActive(true);
            if (isOn)
            {
                QuestInfoDisplay.instance.SetInfo(quest);
                QuestListDisplay.selectedQuest = quest;
            }
        }
        else
        {
            QuestListDisplay.instance.questInfoGo.SetActive(false);
        }
    }
}
