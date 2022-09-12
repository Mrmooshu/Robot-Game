using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Go Items Step", fileName = "HaveItemsStep")]
public class HaveItemsStep : Quest.QuestStep
{
    public Item[] requiredItems;

    public override void Initialize(Quest quest)
    {
        base.Initialize(quest);
    }
}