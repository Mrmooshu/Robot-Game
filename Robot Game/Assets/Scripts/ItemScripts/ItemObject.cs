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
        GetComponent<SpriteRenderer>().sprite = Database.GetItem(item.itemID).sprite;
    }
}
