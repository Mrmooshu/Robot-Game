using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemInventoryDisplay : InventoryDisplay
{
    public ItemInventory currentInventory;

    public void Start()
    {
        if (slotPrefab == null)
        {
            slotPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("ItemSlot");
        }
        objectPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("InventoryItem");
        RefreshInventory();
        PlayerManager.instance.minionChanged += RefreshInventory;
        ItemInventory.inventoryUpdated += RefreshInventory;
    }

    private void OnDestroy()
    {
        PlayerManager.instance.minionChanged -= RefreshInventory;
        ItemInventory.inventoryUpdated -= RefreshInventory;
    }

    public override void RefreshInventory()
    {
        UpdateCurrentInventory();
        CreateInventory();
    }

    protected override void CreateInventory()
    {
        var existingSlots = inventoryArea.transform.GetComponentsInChildren<ItemInventorySlot>();

        int x = 0;
        int y = 0;
        for (int i = 0; i < currentInventory.GetSize(); i++)
        {
            if (existingSlots.Any(x => x.inventoryIndex == i))
            {
                var child = existingSlots.FirstOrDefault(x => x.inventoryIndex == i);
                child.GetComponent<ItemInventorySlot>().inventory = currentInventory;
                if (child.GetComponentInChildren<InventoryItem>() != null)
                {
                    if (currentInventory.GetSlotByIndex(i) == null)
                    {
                        Destroy(child.GetComponentInChildren<InventoryItem>().gameObject);
                    }
                    else
                    {
                        child.GetComponentInChildren<InventoryItem>().item = currentInventory.GetSlotByIndex(i);
                        child.GetComponent<ItemInventorySlot>().RefreshSlot();
                    }
                }
                else if (currentInventory.GetSlotByIndex(i) != null)
                {
                    GameObject itemInstance = Instantiate(objectPrefab, inventoryArea.transform.GetChild(i).transform);
                    InventoryItem invenItem = itemInstance.GetComponent<InventoryItem>();
                    invenItem.item = currentInventory.GetSlotByIndex(i);
                    invenItem.Initialize();
                }
            }
            else
            {
                GameObject slotInstance = Instantiate(slotPrefab, inventoryArea.transform);
                slotInstance.transform.localPosition = new Vector2((x * slotSize), (-y * slotSize));
                slotInstance.GetComponent<ItemInventorySlot>().inventoryIndex = i;
                slotInstance.GetComponent<ItemInventorySlot>().inventory = currentInventory;

                if (currentInventory.GetSlotByIndex(i) != null)
                {
                    GameObject itemInstance = Instantiate(objectPrefab, slotInstance.transform);
                    InventoryItem invenItem = itemInstance.GetComponent<InventoryItem>();
                    invenItem.item = currentInventory.GetSlotByIndex(i);
                    invenItem.Initialize();
                }
            }
            x++;
            if (x >= columns)
            {
                x = 0;
                y++;
            }
        }
    }

    public override void UpdateCurrentInventory()
    {
        currentInventory = PlayerManager.instance.activeMinion.inventory;
    }
}
