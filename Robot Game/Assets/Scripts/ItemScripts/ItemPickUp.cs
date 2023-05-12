using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Item Pick Up
        if (collision.gameObject.GetComponent<ItemObject>())
        {
            if (collision.gameObject.GetComponent<ItemObject>().canBePickedUp)
            {
                if (GetComponentInParent<MinionEntity>().data.inventory.Add(collision.gameObject.GetComponent<ItemObject>().item))
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    Debug.Log("full inventory");
                }
            }

        }
    }
}
