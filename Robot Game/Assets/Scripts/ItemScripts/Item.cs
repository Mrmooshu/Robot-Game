using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

[System.Serializable]
public class Item : ISerializationCallbackReceiver
{
    public int itemID;
    [System.NonSerialized] public BigInteger quanity;

    [SerializeField] private string quanityString;

    public Item(int itemID, BigInteger quanity)
    {
        this.itemID = itemID;
        this.quanity = quanity;
        quanityString = quanity.ToString();
    }

    public void OnAfterDeserialize()
    {
        if (quanityString != null)
        {
            quanity = BigInteger.Parse(quanityString);
        }
    }

    public void OnBeforeSerialize()
    {
        if (quanity != null)
        {
            quanityString = quanity.ToString();
        }
        else
        {
            quanityString = "";
        }
    }
}
