using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject currentPlayer;
    public Camera uiCamera;
    public Inventory currentInventory;
    public GameObject slotPreFab;
    public GameObject itemPreFab;
    public GameObject itemArea;
    public int columns = 7;

    public void SetPlayer(GameObject player)
    {
        currentPlayer = player;
        currentInventory = currentPlayer.GetComponent<PlayerEntity>().inventory;
        RefreshInventory();
    }

    public void RefreshInventory()
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
        float slotSize = .6f;
        for(int i = 0; i < currentInventory.GetSize(); i++)
        {
            GameObject slotInstance = Instantiate(slotPreFab, itemArea.transform);
            slotInstance.transform.position = new Vector2(itemArea.transform.position.x + (x * slotSize), itemArea.transform.position.y + (-y * slotSize));
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
                invenItem.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = currentInventory.GetItem(i).quanity.ToString();
            }

            if (x >= columns)
            {
                x = 0;
                y++;
            }
        }
    }
}
