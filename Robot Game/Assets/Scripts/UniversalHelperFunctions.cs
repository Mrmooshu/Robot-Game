using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UniversalHelperFunctions
{
    public static bool LayerMaskCompare(LayerMask mask, Collider2D collision)
    {
        return ((1 << collision.gameObject.layer) & mask) != 0;
    }
}
