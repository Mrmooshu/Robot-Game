using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacter : InteractableCharacter
{
    public Quest quest;
    protected override void Interact()
    {
        if (quest.questState == Quest.QuestState.inactive)
        {
            quest.BeginQuest();
            chatText = quest.GetCurrentStep().dialogue;
            Speak();
        }
        else if (quest.questState == Quest.QuestState.active)
        {
            chatText = quest.GetCurrentStep().dialogue;
            Speak();
            quest.GetCurrentStep().CheckProgress();
        }
        else if (quest.questState == Quest.QuestState.completed)
        {
            chatText = quest.info.postQuestDialogue;
            Speak();
            if (quest.GetCurrentStep() != null)
            {
                quest.GetCurrentStep().CheckProgress();
            }
        }
    }
}
