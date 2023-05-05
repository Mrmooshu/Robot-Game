using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInventorySlot : MonoBehaviour, IDropHandler, ISlot
{
    public int inventoryIndex;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.GetComponentInParent<ISlot>().Swap(transform.GetChild(0));
        }
    }

    public void Swap(Transform inventoryItem)
    {
        MinionInventory.Move(inventoryItem.parent.parent.parent.GetComponentInParent<MinionInventoryDisplay>().currentInventory, transform.parent.parent.parent.GetComponentInParent<MinionInventoryDisplay>().currentInventory, inventoryItem.parent.GetComponent<PlayerInventorySlot>().inventoryIndex, inventoryIndex);
        inventoryItem.parent.parent.parent.GetComponentInParent<MinionInventoryDisplay>().RefreshInventory();
        transform.parent.parent.parent.GetComponentInParent<MinionInventoryDisplay>().RefreshInventory();
    }
}
