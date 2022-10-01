using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBody
{
    public string variantName;
    public int level = 0;
    public Item weapon = null;

    public PlayerBody(string variantName)
    {
        this.variantName = variantName;
    }
}
