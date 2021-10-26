using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/DropTable", fileName = "DropTable")]
public class DropTable : ScriptableObject
{
    [System.Serializable]
    public struct WeightedItem
    {
        public ItemData itemData;
        public int itemQuanity;
        public int weight;
    }
    // items in table should be entered in order of highest weight to lowest weight
    public WeightedItem[] tableItems;
    private int totalDropRange;

    private void Awake()
    {
        foreach (WeightedItem item in tableItems)
        {
            totalDropRange += item.weight;
        }
    }

    public Item RollTable()
    {
        if (tableItems.Length == 1)
        {
            return new Item(tableItems[0].itemData, tableItems[0].itemQuanity);
        }

        int randomNum = Random.Range(0, totalDropRange);

        foreach (WeightedItem wItem in tableItems)
        {
            if (randomNum <= wItem.weight)
            {
                return new Item(wItem.itemData, wItem.itemQuanity);
            }
            else
            {
                randomNum -= wItem.weight;
            }
        }
        return null;
    }
}
