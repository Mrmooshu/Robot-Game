using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Equipable
{
    [Header("Weapon Properties")]
    public AnimationClip animation;

    public abstract void BasicAttack(MinionEntity player);

    
}
