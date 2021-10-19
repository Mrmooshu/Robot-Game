using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Default", fileName = "Default Item")]
public class Item : ScriptableObject
{
    [Header("Item Properties")]
    public int itemID;
    public string itemName = "default name";
    public string itemDescription = "default description";
    public Sprite sprite;
}
