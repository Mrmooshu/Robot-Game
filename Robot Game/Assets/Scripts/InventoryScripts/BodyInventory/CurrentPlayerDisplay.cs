using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentPlayerDisplay : SlotDisplay<PlayerBody>, IDropHandler, ISlot
{
    private void Start()
    {
        objectPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("InventoryPlayer");
        CreateSlot();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Swap(eventData.pointerDrag.transform);
        }
    }

    public void Swap(Transform inventoryItem)
    {
        if (inventoryItem.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().currentInventory.inventory[inventoryItem.parent.GetComponent<PlayerInventorySlot>().inventoryIndex] != null)
        {
            BodyInventory.Move(inventoryItem.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().currentInventory, ref PlayerManager.instance.activeCore.currentBody, inventoryItem.parent.GetComponent<PlayerInventorySlot>().inventoryIndex);
            inventoryItem.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().RefreshInventory();
            RefreshSlot();
        }
    }

    public override void RemoveFromSlot()
    {
        Debug.Log("should never call this function");
    }

    public override void RefreshSlot()
    {
        CreateSlot();
        PlayerManager.instance.Respawn(PlayerManager.instance.activeCore);
    }

    protected override void CreateSlot()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<InventoryPlayer>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        GameObject bodyInstance = Instantiate(objectPrefab, transform);
        InventoryPlayer invenPlayer = bodyInstance.GetComponent<InventoryPlayer>();
        invenPlayer.body = PlayerManager.instance.activeCore.currentBody;
        invenPlayer.transform.GetChild(0).GetComponent<Image>().sprite = PlayerManager.instance.activeCore.GetPlayer().GetComponent<SpriteRenderer>().sprite;
    }
}
