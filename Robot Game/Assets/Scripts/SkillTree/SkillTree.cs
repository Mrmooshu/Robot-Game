using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public SkillUpgradeDisplay[] skills { get => transform.GetComponentsInChildren<SkillUpgradeDisplay>(); }
}
