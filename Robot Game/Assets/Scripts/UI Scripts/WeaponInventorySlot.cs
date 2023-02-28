using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class WeaponInventorySlot : SlotDisplay<Item>, IDropHandler, ISlot
{
    private void Start()
    {
        objectPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("InventoryItem");
        CreateSlot();
        PlayerManager.instance.playerChanged += RefreshSlot;
    }

    private void OnDestroy()
    {
        PlayerManager.instance.playerChanged -= RefreshSlot;
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
        if (inventorySlot.GetComponentInChildren<InventoryItem>())
        {
            if (Database.GetItem(inventorySlot.GetComponentInChildren<InventoryItem>().item.itemID) is Weapon)
            {
                ItemInventory.Move(inventorySlot.parent.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory, ref PlayerManager.instance.activePlayer.weapon, inventorySlot.GetComponent<ItemInventorySlot>().inventoryIndex);
                inventorySlot.parent.parent.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
                RefreshSlot();
            }
        }
        else
        {
            ItemInventory.Move(inventorySlot.parent.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory, ref PlayerManager.instance.activePlayer.weapon, inventorySlot.GetComponent<ItemInventorySlot>().inventoryIndex);
            inventorySlot.parent.parent.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
            RefreshSlot();
        }
    }

    public override void RemoveFromSlot()
    {
        PlayerManager.instance.activePlayer.weapon = null;
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

        if (PlayerManager.instance.activePlayer.weapon != null)
        {
            GameObject itemInstance = Instantiate(objectPrefab, transform);
            InventoryItem invenItem = itemInstance.GetComponent<InventoryItem>();
            invenItem.item = PlayerManager.instance.activePlayer.weapon;
            invenItem.transform.GetChild(0).GetComponent<Image>().sprite = Database.GetItem(PlayerManager.instance.activePlayer.weapon.itemID).sprite;
            if (!Database.GetItem(invenItem.item.itemID).stackable)
            {
                invenItem.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                invenItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerManager.instance.activePlayer.weapon.quanity.ToString();
            }
        }
    }
}
