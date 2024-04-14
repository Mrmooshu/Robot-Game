using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<InventoryItem>())
        {
            var activeMinion = PlayerManager.instance.activeMinion.GetEntity();

            var droppedItem = GeneralManager.SpawnItem(activeMinion.groundCheck.transform, eventData.pointerDrag.GetComponent<InventoryItem>().item);
            //GameObject droppedItem = Instantiate(itemPrefab, PlayerManager.instance.activeMinion.GetEntity().groundCheck.position, Quaternion.identity);
            //droppedItem.GetComponent<ItemObject>().SetItem(eventData.pointerDrag.GetComponent<InventoryItem>().item);
            droppedItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(120, 150) * activeMinion.facingDirection, 200));

            eventData.pointerDrag.transform.GetComponentInParent<ISlot>().RemoveFromSlot();
        }
    }
}
