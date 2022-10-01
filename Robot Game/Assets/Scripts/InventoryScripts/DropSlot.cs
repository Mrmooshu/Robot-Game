using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public GameObject itemPrefab;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<InventoryItem>())
        {
            GameObject droppedItem = Instantiate(itemPrefab, PlayerManager.instance.activeCore.GetPlayer().transform.position, Quaternion.identity);
            droppedItem.GetComponent<ItemObject>().SetItem(eventData.pointerDrag.GetComponent<InventoryItem>().item);
            droppedItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50, 50), 80));

            eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<InventoryDisplay>().currentInventory.Remove(eventData.pointerDrag.transform.parent.GetComponent<InventorySlot>().inventoryIndex);
            eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<InventoryDisplay>().RefreshInventory();
        }
    }
}
