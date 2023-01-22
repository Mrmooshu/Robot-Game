using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive : MonoBehaviour
{
    public string abilityName;
    public string abilityDescription;
    protected PlayerEntity entity;

    public abstract void InitializePassive(Entity target);
}
