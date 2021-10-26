using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public GameObject itemPrefab;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GameObject droppedItem = Instantiate(itemPrefab, transform.parent.GetComponent<InventoryDisplay>().currentPlayer.transform.position, Quaternion.identity);
            droppedItem.GetComponent<ItemObject>().SetItem(eventData.pointerDrag.GetComponent<InventoryItem>().item);
            transform.parent.GetComponent<InventoryDisplay>().currentInventory.Remove(eventData.pointerDrag.transform.parent.GetComponent<InventorySlot>().inventoryIndex);
            transform.parent.GetComponent<InventoryDisplay>().RefreshInventory();
        }
    }
}
