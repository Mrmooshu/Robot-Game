using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Turn In Step", fileName = "Turn In Step")]
public class TurnInQuestStep : Quest.QuestStep
{
    public override void CheckProgress()
    {
        quest.CompleteQuest();
    }
}
