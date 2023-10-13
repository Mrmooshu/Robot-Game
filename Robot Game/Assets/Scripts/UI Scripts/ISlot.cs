using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot
{
    public void Swap(Transform slotTransform, bool condition = true);

    public void RemoveFromSlot();
}
