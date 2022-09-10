using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public List<Quest> inactiveQuests = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completeQuests = new List<Quest>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Initialize()
    {
        // set inactive for now, change when saving quest state
        foreach (Quest quest in Database.instance.questDatabase.questList)
        {
            quest.questState = Quest.QuestState.inactive;
        }

        // organize quests based on state
        foreach (Quest quest in Database.instance.questDatabase.questList)
        {
            switch (quest.questState)
            {
                case Quest.QuestState.inactive:
                    inactiveQuests.Add(quest);
                    break;
                case Quest.QuestState.active:
                    activeQuests.Add(quest);
                    break;
                case Quest.QuestState.completed:
                    completeQuests.Add(quest);
                    break;
            }
        }
    }

    public static void ProgressQuestState(Quest quest) {
        switch (quest.questState)
        {
            case Quest.QuestState.inactive:
                quest.questState = Quest.QuestState.active;
                instance.inactiveQuests.Remove(quest);
                instance.activeQuests.Add(quest);
                break;
            case Quest.QuestState.active:
                quest.questState = Quest.QuestState.completed;
                instance.activeQuests.Remove(quest);
                instance.completeQuests.Add(quest);
                break;
            case Quest.QuestState.completed:
                Debug.Log("this should never be called when completed :(");
                break;
        }

        if (QuestListDisplay.instance != null)
        {
            QuestListDisplay.instance.RefreshList();
        }
    }

}
