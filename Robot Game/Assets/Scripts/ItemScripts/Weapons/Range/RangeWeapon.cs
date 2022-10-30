using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeWeapon : Weapon
{
    [Header("Weapon Properties")]
    public GameObject projectilePrefab;
    public Vector2 firePosition;
    public float projectileSpeed;
}
