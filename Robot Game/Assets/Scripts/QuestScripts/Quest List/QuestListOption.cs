using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestListOption : MonoBehaviour
{
    public Quest quest;

    public void SetInfoText()
    {
        transform.parent.parent.parent.parent.Find("QuestInfoPanel").Find("Info Text").GetComponent<TextMeshProUGUI>().text = quest.info.questInfo;
        QuestListDisplay.selectedQuest = quest;
    }
}
