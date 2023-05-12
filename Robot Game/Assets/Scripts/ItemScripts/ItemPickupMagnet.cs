using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupMagnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ItemObject item))
        {
            if (item.canBePickedUp)
            {
                int pullPower = PlayerManager.instance.activeMinion == GetComponentInParent<MinionEntity>().data ? 5 : 10;
                
                item.GetComponent<Rigidbody2D>().velocity = ((transform.position - item.transform.position).normalized * pullPower);
            }
        }
    }
}
