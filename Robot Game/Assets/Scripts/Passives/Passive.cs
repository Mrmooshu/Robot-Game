using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive : MonoBehaviour
{
    public string abilityName;
    public string abilityDescription;
    protected Entity entity;

    public abstract void InitializePassive(Entity host);

    public abstract void Refresh();
}
