using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Skill", fileName = "Skill")]
public class Skilldata : ScriptableObject
{
    public Sprite Icon;
    public string InternalName;
    public string DisplayName;
    public string Description;

    private void OnValidate()
    {
        //InternalName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(this));
    }
}
