using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SafePoint/SafePointDatabase", fileName = "SafePointDatabase")]
public class SafePointDatabase : ScriptableObject
{
    public List<SafePointData> safePointList;
}
