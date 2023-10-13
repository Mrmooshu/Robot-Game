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

    public void Swap(Transform inventoryItem, bool condition = true)
    {
        MinionInventory.Move(inventoryItem.GetComponentInParent<MinionInventoryDisplay>().currentInventory, transform.GetComponentInParent<MinionInventoryDisplay>().currentInventory, inventoryItem.parent.GetComponent<PlayerInventorySlot>().inventoryIndex, inventoryIndex);
        inventoryItem.GetComponentInParent<MinionInventoryDisplay>().RefreshInventory();
        transform.GetComponentInParent<MinionInventoryDisplay>().RefreshInventory();
    }

    public void RemoveFromSlot()
    {
        //delete character
    }
}
