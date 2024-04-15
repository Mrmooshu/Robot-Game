using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UniversalHelperFunctions
{
    //used to save dictionaries in json
    [System.Serializable]
    public struct SavedData<T>
    {
        public string name;
        public T data;

        public SavedData(string name, T data)
        {
            this.name = name;
            this.data = data;
        }
    }

    public static bool LayerMaskCompare(LayerMask mask, Collider2D collision)
    {
        return ((1 << collision.gameObject.layer) & mask) != 0;
    }

    public static IEnumerator DelayedAction(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}
