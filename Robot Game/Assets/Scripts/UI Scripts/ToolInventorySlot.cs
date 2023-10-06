using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ToolInventorySlot : SlotDisplay<Tool>
{
    public override ref Item GetItem()
    {
        return ref PlayerManager.instance.activeMinion.tool;
    }
}
