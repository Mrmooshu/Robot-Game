using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacter : InteractableCharacterEntity
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
            quest.GetCurrentStep().CheckProgress();
        }
    }
}
