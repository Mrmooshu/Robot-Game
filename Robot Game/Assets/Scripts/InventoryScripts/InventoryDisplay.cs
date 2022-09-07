using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    private Camera uiCamera;
    public Inventory currentInventory;
    private GameObject slotPreFab;
    private GameObject itemPreFab;
    public GameObject itemArea;
    public GameObject pageNumber;
    public int columns = 7;
    public int slotsPerPage = 36;

    public void Start()
    {
        uiCamera = GameObject.Find("UI Camera").GetComponent<Camera>();
        slotPreFab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("ItemSlot");
        itemPreFab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("InventoryItem");
        RefreshInventory();
    }

    public virtual void RefreshInventory()
    {
        currentInventory = PlayerManager.instance.activeCore.inventory;
        CreateInventory();
    }

    protected void CreateInventory()
    {
        foreach (Transform child in itemArea.transform)
        {
            if (child.GetComponent<InventorySlot>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        int x = 0;
        int y = 0;
        float slotSize = 34f;
        for (int i = slotsPerPage * currentInventory.currentPage - slotsPerPage; i < currentInventory.GetSize() && i < slotsPerPage * currentInventory.currentPage; i++)
        {
            GameObject slotInstance = Instantiate(slotPreFab, itemArea.transform);
            slotInstance.transform.localPosition = new Vector2((x * slotSize), (-y * slotSize));
            slotInstance.GetComponent<InventorySlot>().inventoryDisplay = this;
            slotInstance.GetComponent<InventorySlot>().inventory = currentInventory;
            slotInstance.GetComponent<InventorySlot>().inventoryIndex = i;

            x++;

            if (currentInventory.GetItem(i) != null)
            {
                GameObject itemInstance = Instantiate(itemPreFab, slotInstance.transform);
                InventoryItem invenItem = itemInstance.GetComponent<InventoryItem>();
                invenItem.item = currentInventory.GetItem(i);
                invenItem.cam = uiCamera;
                invenItem.transform.GetChild(0).GetComponent<Image>().sprite = Database.GetItem(currentInventory.GetItem(i).itemID).sprite;
                if (!Database.GetItem(invenItem.item.itemID).stackable)
                {
                    invenItem.transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    invenItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentInventory.GetItem(i).quanity.ToString();
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

    // used by page select arrows
    public void IncrementPage()
    {
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

    // used by page select arrows
    public void DecrementPage()
    {
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

}
