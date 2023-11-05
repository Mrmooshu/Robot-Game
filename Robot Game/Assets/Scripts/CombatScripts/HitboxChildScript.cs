using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxChildScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GetComponentInParent<HitboxScript>().Hit(collider);
    }

    private void OnDisable()
    {
        if (GetComponentInParent<HitboxScript>())
        {
            GetComponentInParent<HitboxScript>().CheckToEnd();
        }
    }
}
