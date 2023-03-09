using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeWeapon : Weapon
{
    [Header("Range Weapon Properties")]
    public GameObject projectilePrefab;
    public Vector2 firePosition;
}
