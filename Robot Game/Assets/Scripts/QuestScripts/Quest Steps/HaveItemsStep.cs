using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Have Items Step", fileName = "HaveItemsStep")]
public class HaveItemsStep : Quest.QuestStep
{
    [System.Serializable]
    public struct RequiredItem
    {
        public ItemData requiredItem;
        public int requiredItemQuanity;
        [System.NonSerialized] public BigInteger gatheredItemQuanity;
    }

    public RequiredItem[] requiredItems;

    public override void Initialize(Quest quest)
    {
        base.Initialize(quest);
        Inventory.inventoryUpdated += UpdateItemTracking;
    }

    private void UpdateItemTracking()
    {
        for(int i = 0; i < requiredItems.Length; i++)
        {
            requiredItems[i].gatheredItemQuanity = PlayerManager.CheckCurrentInventoryForItem(requiredItems[i].requiredItem.itemID);
        }
    }

    public override void CheckProgress()
    {
        Evaluate();
    }

    public override void Evaluate()
    {
        for (int i = 0; i < requiredItems.Length; i++)
        {
            if (requiredItems[i].gatheredItemQuanity < requiredItems[i].requiredItemQuanity)
            {
                return;
            }
        }
        base.Evaluate();
    }

    protected override void CleanUp()
    {
        base.CleanUp();
        Inventory.inventoryUpdated -= UpdateItemTracking;
    }
}