using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    [Header("Item Properties")]
    public int itemID;
    public string itemName = "default name";
    public string itemDescription = "default description";
    public bool stackable = true;
    public Sprite sprite;
}
