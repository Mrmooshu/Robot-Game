using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnHit
{
    public DamageScript.damageData OnHit(Entity target);
}
