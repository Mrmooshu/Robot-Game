using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody
{
    public string variantName;
    public int level = 0;
    public Weapon weapon;

    public PlayerBody(string variantName)
    {
        this.variantName = variantName;
    }
}
