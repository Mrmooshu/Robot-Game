using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ToolInventorySlot : SlotDisplay<Item>, IDropHandler, ISlot
{

    private void Start()
    {
        objectPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("InventoryItem");
        CreateSlot();
        PlayerManager.instance.minionChanged += RefreshSlot;
    }

    private void OnDestroy()
    {
        PlayerManager.instance.minionChanged -= RefreshSlot;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Swap(eventData.pointerDrag.transform.parent);
        }
    }

    public void Swap(Transform inventorySlot)
    {
        //if other slot is not and item slot
        if (!inventorySlot.GetComponent<ItemInventorySlot>())
        {
            return;
        }

        if (inventorySlot.GetComponentInChildren<InventoryItem>())
        {
            if (Database.GetItem(inventorySlot.GetComponentInChildren<InventoryItem>().item.itemID) is Tool)
            {
                ItemInventory.Move(inventorySlot.parent.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory, ref PlayerManager.instance.activeMinion.tool, inventorySlot.GetComponent<ItemInventorySlot>().inventoryIndex);
                inventorySlot.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
                RefreshSlot();
            }
        }
        else
        {
            ItemInventory.Move(inventorySlot.GetComponentInParent<ItemInventoryDisplay>().currentInventory, ref PlayerManager.instance.activeMinion.tool, inventorySlot.GetComponent<ItemInventorySlot>().inventoryIndex);
            inventorySlot.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
            RefreshSlot();
        }
    }

    public override void RemoveFromSlot()
    {
        PlayerManager.instance.activeMinion.tool = null;
    }

    public override void RefreshSlot()
    {
        CreateSlot();
    }

    protected override void CreateSlot()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<InventoryItem>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        if (PlayerManager.instance.activeMinion.tool != null)
        {
            GameObject itemInstance = Instantiate(objectPrefab, transform);
            InventoryItem invenItem = itemInstance.GetComponent<InventoryItem>();
            invenItem.item = PlayerManager.instance.activeMinion.tool;
            invenItem.transform.GetChild(0).GetComponent<Image>().sprite = Database.GetItem(PlayerManager.instance.activeMinion.tool.itemID).sprite;
            if (!Database.GetItem(invenItem.item.itemID).stackable)
            {
                invenItem.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                invenItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerManager.instance.activeMinion.tool.quanity.ToString();
            }
        }
    }
}
