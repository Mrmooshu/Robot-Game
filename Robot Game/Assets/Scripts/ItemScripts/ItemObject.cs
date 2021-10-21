using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;

    public void SetItem(Item item)
    {
        this.item = item;
        GetComponent<SpriteRenderer>().sprite = item.sprite;
    }
}
