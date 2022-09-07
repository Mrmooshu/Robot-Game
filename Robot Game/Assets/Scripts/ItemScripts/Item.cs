using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class Item
{
    public int itemID;
    public BigInteger quanity;

    public Item(int itemID, BigInteger quanity)
    {
        this.itemID = itemID;
        this.quanity = quanity;
    }
}
