using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ItemData
{
    [Header("Weapon Properties")]
    public RuntimeAnimatorController animController;
    public int baseAttackSpeed;
    public int baseDamage;

    [System.Serializable]
    public struct HitColliderInfo
    {
        public Vector2 position;
        public float radius;
    }
}
