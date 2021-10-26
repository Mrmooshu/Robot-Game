using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemData data;
    public int quanity;

    public Item(ItemData data, int quanity)
    {
        this.data = data;
        this.quanity = quanity;
    }
}
