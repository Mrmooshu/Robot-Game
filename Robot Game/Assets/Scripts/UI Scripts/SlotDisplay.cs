using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public abstract class SlotDisplay : MonoBehaviour, IDropHandler, ISlot
{
    protected GameObject objectPrefab;

    protected virtual void Start()
    {
        objectPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("InventoryItem");
        CreateSlot();
        PlayerManager.instance.minionChanged += RefreshSlot;
    }

    protected virtual void OnDestroy()
    {
        PlayerManager.instance.minionChanged -= RefreshSlot;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<InventoryItem>())
        {
            Swap(eventData.pointerDrag.transform.parent);
        }
    }

    public virtual void RefreshSlot()
    {
        CreateSlot();
    }
    protected virtual void CreateSlot()
    {
        var item = GetItem();

        foreach (Transform child in transform)
        {
            var ItemObj = child.GetComponent<InventoryItem>();
            if (ItemObj != null && item != null)
            {
                ItemObj.item = item;
                ItemObj.Initialize();
                StackCheck(ItemObj);
                return;
            }
            else if(ItemObj != null)
            {
                Destroy(child.gameObject);
            }
        }

        if (item != null)
        {
            GameObject itemInstance = Instantiate(objectPrefab, transform);
            InventoryItem invenItem = itemInstance.GetComponent<InventoryItem>();
            invenItem.item = item;
            invenItem.transform.GetChild(0).GetComponent<Image>().sprite = Database.GetItem(item.itemID).sprite;
            StackCheck(invenItem);
        }

        void StackCheck(InventoryItem invenItem)
        {
            if (!Database.GetItem(invenItem.item.itemID).stackable)
            {
                invenItem.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                invenItem.transform.GetChild(1).gameObject.SetActive(true);
                invenItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.quanity.ToString();
            }
        }
    }
    public virtual void RemoveFromSlot()
    {
        if (Database.GetItem(GetItem().itemID) is Equipable)
        {
            ((Equipable)Database.GetItem(GetItem().itemID)).Unequip();
        }
        GetItem() = null;
        RefreshSlot();
    }

    public virtual void Swap(Transform inventorySlot, bool condition = true)
    {
        if (inventorySlot == transform)
        {
            return;
        }

        if (inventorySlot.GetComponentInChildren<InventoryItem>())
        {
            if (condition)
            {
                //Unequip
                if (GetItem() != null) {
                    if (Database.GetItem(GetItem().itemID).GetType().IsSubclassOf(typeof(Equipable)))
                    {
                        ((Equipable)Database.GetItem(GetItem().itemID)).Unequip();
                    }
                }
                //Swap
                ItemInventory.Move(inventorySlot.GetComponentInParent<ItemInventoryDisplay>().currentInventory, ref GetItem(), inventorySlot.GetComponent<ItemInventorySlot>().inventoryIndex);
                //Equip
                if (GetItem() != null)
                {
                    if (Database.GetItem(GetItem().itemID).GetType().IsSubclassOf(typeof(Equipable)))
                    {
                        ((Equipable)Database.GetItem(GetItem().itemID)).Equip(ref PlayerManager.instance.activeMinion.GetEntity().stats);
                    }
                }
                //Refresh
                inventorySlot.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
                RefreshSlot();
            }
        }
        else
        {
            //swapping with emtpy slot
            if (Database.GetItem(GetItem().itemID) is Equipable)
            {
                ((Equipable)Database.GetItem(GetItem().itemID)).Unequip();
            }
            ItemInventory.Move(inventorySlot.GetComponentInParent<ItemInventoryDisplay>().currentInventory, ref GetItem(), inventorySlot.GetComponent<ItemInventorySlot>().inventoryIndex);
            inventorySlot.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
            RefreshSlot();
        }
    }

    public abstract ref Item GetItem();
}
