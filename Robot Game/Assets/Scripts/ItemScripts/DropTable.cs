using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/DropTable", fileName = "DropTable")]
public class DropTable : ScriptableObject
{
    public List<Item> tableItems;
    public List<int> dropRateDenominator;
}
