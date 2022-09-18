using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListOption : MonoBehaviour
{
    public Quest quest;

    public void Selected()
    {
        QuestInfoDisplay.instance.SetInfo(quest);
    }
}
