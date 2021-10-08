using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory System/Inventory")]

public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public void AddItem(ItemObject item, int quanity)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count ;i++)
        {
            if (Container[i].item == item)
            {
                Container[i].AddQuanity(quanity);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(item, quanity));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int quanity;

    public InventorySlot(ItemObject item, int quanity)
    {
        this.item = item;
        this.quanity = quanity;
    }

    public void AddQuanity(int amount)
    {
        quanity += amount;
    }

}
