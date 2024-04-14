using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public enum ItemTags
    {
        Fuel,Smeltable
    }

    [Header("Item Properties")]
    public int itemID;
    public string itemName = "default name";
    public string itemDescription = "default description";
    public bool stackable = true;
    public Sprite sprite;
    public float mass = 1;
    public float gravity = 1;
    public ItemTags[] tags;
}
