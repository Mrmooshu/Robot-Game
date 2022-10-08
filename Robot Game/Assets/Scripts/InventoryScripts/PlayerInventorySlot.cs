using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInventorySlot : MonoBehaviour, IDropHandler
{
    public int inventoryIndex;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.transform.parent.GetComponent<CurrentPlayerDisplay>())
            {
                if (transform.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().currentInventory.inventory[inventoryIndex] != null)
                {
                    BodyInventory.Move(transform.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().currentInventory, PlayerManager.instance.activeCore.currentBody, inventoryIndex);
                    transform.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().RefreshInventory();
                    eventData.pointerDrag.transform.parent.GetComponent<CurrentPlayerDisplay>().RefreshSlot();
                }
            }
            else
            {
                BodyInventory.Move(eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().currentInventory, transform.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().currentInventory, eventData.pointerDrag.transform.parent.GetComponent<PlayerInventorySlot>().inventoryIndex, inventoryIndex);
                eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().RefreshInventory();
                transform.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().RefreshInventory();
            }
        }
    }
}
