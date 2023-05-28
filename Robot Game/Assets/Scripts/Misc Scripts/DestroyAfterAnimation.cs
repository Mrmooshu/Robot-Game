using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    [SerializeField] protected GameObject go;
    public void DestroyObject()
    {
        Destroy(go);
    }
}
