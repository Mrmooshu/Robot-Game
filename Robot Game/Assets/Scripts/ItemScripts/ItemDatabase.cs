using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemDatabase", fileName = "ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> itemsList;

    public List<ItemData> miningItemsList;
    public List<ItemData> weaponsItemsList;
    public List<ItemData> miscItemsList;

    public void Initialize()
    {
        itemsList.Clear();
        itemsList.AddRange(miningItemsList);
        itemsList.AddRange(weaponsItemsList);
        itemsList.AddRange(miscItemsList);
    }
}
