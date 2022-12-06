using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillTree/SkillTree", fileName = "SkillTree")]
public class SkillTree : ScriptableObject
{
    public List<SkillTreeUpgrade> skills;
}
