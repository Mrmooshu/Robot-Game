using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            PlayerManager.instance.activeCore.inventory.Remove(eventData.pointerDrag.transform.parent.GetComponent<InventorySlot>().inventoryIndex);
            UIManager.instance.currentMenu.GetComponentInChildren<InventoryDisplay>().RefreshInventory();
        }
    }
}
