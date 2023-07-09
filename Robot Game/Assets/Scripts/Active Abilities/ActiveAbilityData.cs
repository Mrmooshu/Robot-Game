using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActiveAbility/ActiveAbility", fileName = "ActiveAbility")]
public class ActiveAbilityData : ScriptableObject
{
    public enum ActiveAbilityOwner{
        general,golem
    }

    public ActiveAbilityOwner owner = ActiveAbilityOwner.general;
    public Sprite[] Icon; //Icons after the first are used for actives with multiple stages
    public string InternalName;
    public string DisplayName;
    public string Description;
    public float CoolDown;
    public float Cost;
    public float CostType;
}
