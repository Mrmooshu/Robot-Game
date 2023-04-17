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
        if (inventorySlot.GetComponentInChildren<InventoryItem>())
        {
            //swapping with another weapon
            if (Database.GetItem(inventorySlot.GetComponentInChildren<InventoryItem>().item.itemID) is Weapon)
            {
                if (PlayerManager.instance.activeMinion.weapon != null) { ((Weapon)Database.GetItem(PlayerManager.instance.activeMinion.weapon.itemID)).Unequip(); }
                ItemInventory.Move(inventorySlot.parent.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory, ref PlayerManager.instance.activeMinion.weapon, inventorySlot.GetComponent<ItemInventorySlot>().inventoryIndex);
                ((Weapon)Database.GetItem(PlayerManager.instance.activeMinion.weapon.itemID)).Equip(ref PlayerManager.instance.activeMinion.GetEntity().stats);
                inventorySlot.parent.parent.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
                RefreshSlot();
            }
        }
        else
        {
            //swapping with emtpy slot
            ((Weapon)Database.GetItem(PlayerManager.instance.activeMinion.weapon.itemID)).Unequip();
            ItemInventory.Move(inventorySlot.parent.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory, ref PlayerManager.instance.activeMinion.weapon, inventorySlot.GetComponent<ItemInventorySlot>().inventoryIndex);
            inventorySlot.parent.parent.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
            RefreshSlot();
        }
    }

    public override void RemoveFromSlot()
    {
        ((Weapon)Database.GetItem(PlayerManager.instance.activeMinion.weapon.itemID)).Unequip();
        PlayerManager.instance.activeMinion.weapon = null;
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

        if (PlayerManager.instance.activeMinion.weapon != null)
        {
            GameObject itemInstance = Instantiate(objectPrefab, transform);
            InventoryItem invenItem = itemInstance.GetComponent<InventoryItem>();
            invenItem.item = PlayerManager.instance.activeMinion.weapon;
            invenItem.transform.GetChild(0).GetComponent<Image>().sprite = Database.GetItem(PlayerManager.instance.activeMinion.weapon.itemID).sprite;
            if (!Database.GetItem(invenItem.item.itemID).stackable)
            {
                invenItem.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                invenItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerManager.instance.activeMinion.weapon.quanity.ToString();
            }
        }
    }
}
