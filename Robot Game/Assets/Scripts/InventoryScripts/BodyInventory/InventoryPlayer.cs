using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPlayer : UIDraggable
{
    public MinionData unit;

    public override void OnPointerDown(PointerEventData eventData)
    {
        SafePointEntity.selectedMinion = unit;
    }
}
