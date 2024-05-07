using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;
    public bool canBePickedUp = false;

    private void Awake()
    {
        StartCoroutine(WaitForPickup());
    }

    private IEnumerator WaitForPickup()
    {
        canBePickedUp = false;
        yield return new WaitForSeconds(1.5f);
        canBePickedUp = true;
    }

    public void SetItem(Item item)
    {
        this.item = item;
        var itemdata = Database.GetItem(item.itemID);
        var rig = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = itemdata.sprite;
        rig.mass = itemdata.mass;
        rig.gravityScale = itemdata.gravity;
        var sizescale = (int)Database.GetItem(item.itemID).size;
        transform.localScale = new Vector3(0.25f * sizescale, 0.25f * sizescale, 1);
    }
}
