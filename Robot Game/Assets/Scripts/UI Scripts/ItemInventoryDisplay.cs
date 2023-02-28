using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryDisplay : InventoryDisplay
{
    public ItemInventory currentInventory;

    public void Start()
    {
        slotPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("ItemSlot");
        objectPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("InventoryItem");
        RefreshInventory();
        PlayerManager.instance.playerChanged += RefreshInventory;
        ItemInventory.inventoryUpdated += RefreshInventory;
    }

    private void OnDestroy()
    {
        PlayerManager.instance.playerChanged -= RefreshInventory;
        ItemInventory.inventoryUpdated -= RefreshInventory;
    }

    public override void RefreshInventory()
    {
        UpdateCurrentInventory();
        CreateInventory();
    }

    protected override void CreateInventory()
    {
        foreach (Transform child in inventoryArea.transform)
        {
            if (child.GetComponent<ItemInventorySlot>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        int x = 0;
        int y = 0;
        float slotSize = 34f;
        for (int i = slotsPerPage * currentInventory.currentPage - slotsPerPage; i < currentInventory.GetSize() && i < slotsPerPage * currentInventory.currentPage; i++)
        {
            GameObject slotInstance = Instantiate(slotPrefab, inventoryArea.transform);
            slotInstance.transform.localPosition = new Vector2((x * slotSize), (-y * slotSize));
            slotInstance.GetComponent<ItemInventorySlot>().inventoryIndex = i;

            x++;

            if (currentInventory.GetSlotByIndex(i) != null)
            {
                GameObject itemInstance = Instantiate(objectPrefab, slotInstance.transform);
                InventoryItem invenItem = itemInstance.GetComponent<InventoryItem>();
                invenItem.item = currentInventory.GetSlotByIndex(i);
                invenItem.transform.GetChild(0).GetComponent<Image>().sprite = Database.GetItem(currentInventory.GetSlotByIndex(i).itemID).sprite;
                if (!Database.GetItem(invenItem.item.itemID).stackable)
                {
                    invenItem.transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    invenItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentInventory.GetSlotByIndex(i).quanity.ToString();
                }
            }

            if (x >= columns)
            {
                x = 0;
                y++;
            }
            pageNumber.GetComponent<TextMeshProUGUI>().text = "" + currentInventory.currentPage;
        }
    }

    public override void IncrementPage()
    {
        UpdateCurrentInventory();
        if (currentInventory.currentPage * slotsPerPage < currentInventory.inventorySize)
        {
            currentInventory.currentPage++;
        }

        else
        {
            currentInventory.currentPage = 1;
        }
        RefreshInventory();
    }

    public override void DecrementPage()
    {
        UpdateCurrentInventory();
        if (currentInventory.currentPage <= 1)
        {
            int lastPage = currentInventory.inventorySize / slotsPerPage;
            if (currentInventory.inventorySize % slotsPerPage != 0)
            {
                lastPage++;
            }
            currentInventory.currentPage = lastPage;
        }
        else
        {
            currentInventory.currentPage--;
        }
        RefreshInventory();
    }

    public override void UpdateCurrentInventory()
    {
        currentInventory = PlayerManager.instance.activePlayer.inventory;
    }
}
