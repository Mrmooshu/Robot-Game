using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveQuest : MonoBehaviour
{
    public Quest quest;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<MinionEntity>() != null)
            {
                ActivateQuest();
            }
        }
    }
    public void ActivateQuest()
    {
        if (quest.questState == Quest.QuestState.inactive)
        {
            quest.BeginQuest();
        }

    }
}
