using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Camera uiCamera;
    public Inventory currentInventory;
    public GameObject slotPreFab;
    public GameObject itemPreFab;
    public GameObject itemArea;
    public int columns = 7;

    public void Start()
    {
        uiCamera = GameObject.Find("UI Camera").GetComponent<Camera>();
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        currentInventory = PlayerManager.instance.activeCore.inventory;
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
        for(int i = 0; i < currentInventory.GetSize(); i++)
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
                    invenItem.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = currentInventory.GetItem(i).quanity.ToString();
                }
            }

            if (x >= columns)
            {
                x = 0;
                y++;
            }
        }
    }
}
