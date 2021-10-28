using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Item/ItemDatabase", fileName = "ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> itemsList;
}
