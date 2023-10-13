using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventoryItem : UIDraggable
{
    public Item item;

    public void Initialize()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = Database.GetItem(item.itemID).sprite;
        if (!Database.GetItem(item.itemID).stackable)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.quanity.ToString();
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //TODO
        // add examine functionality
    }
}
