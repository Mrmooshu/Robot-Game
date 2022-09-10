using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveQuest : MonoBehaviour
{
    public string questName;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerEntity>() != null)
            {
                Debug.Log("Giver touched");
                ActivateQuest();
            }
        }
    }
    public void ActivateQuest()
    {
        Quest quest = Database.GetQuest(questName);
        Debug.Log(quest.questState.ToString());
        if (quest.questState == Quest.QuestState.inactive)
        {
            quest.BeginQuest(0);
            Debug.Log(questName + " started");
        }

    }
}
