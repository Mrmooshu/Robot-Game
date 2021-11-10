using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody
{
    public string variantName;
    public int level = 0;
    public int weaponId;

    public PlayerBody(string variantName)
    {
        this.variantName = variantName;
    }
}
