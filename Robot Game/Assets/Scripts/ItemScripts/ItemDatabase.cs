using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemDatabase", fileName = "ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> itemsList;

    public List<ItemData> miningItemsList;
    public List<ItemData> woodchoppingItemsList;
    public List<ItemData> fishingItemsList;
    public List<ItemData> smithingItemsList;
    public List<ItemData> toolItemsList;
    public List<ItemData> weaponItemsList;
    public List<ItemData> miscItemsList;

    public void Initialize()
    {
        itemsList.Clear();
        itemsList.AddRange(miningItemsList);
        itemsList.AddRange(woodchoppingItemsList);
        itemsList.AddRange(fishingItemsList);
        itemsList.AddRange(smithingItemsList);
        itemsList.AddRange(toolItemsList);
        itemsList.AddRange(weaponItemsList);
        itemsList.AddRange(miscItemsList);
    }
}
