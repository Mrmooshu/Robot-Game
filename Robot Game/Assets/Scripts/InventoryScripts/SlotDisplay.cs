using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlotDisplay<T> : MonoBehaviour
{
    protected GameObject objectPrefab;

    public abstract void RefreshSlot();
    protected abstract void CreateSlot();
    public abstract void RemoveFromSlot();
}
