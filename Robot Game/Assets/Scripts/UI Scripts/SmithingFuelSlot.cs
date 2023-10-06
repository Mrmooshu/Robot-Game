using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmithingFuelSlot : SlotDisplay<Item>, IDropHandler, ISlot
{
    public override void RemoveFromSlot()
    {
        PlayerManager.instance.smithing.furnaceFuel[0] = null;
    }

    public override ref Item GetItem()
    {
        return ref PlayerManager.instance.smithing.furnaceFuel[0];
    }

}
