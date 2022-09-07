using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SafePoint/SafePointData", fileName = "SafePoint Data")]
public class SafePointData : ScriptableObject
{
    [Header("Item Properties")]
    public string locationName = "default name";
    public string mapName;
    public Vector2 cord;
}

