using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Camera uiCamera;
    public int numberOfSlots;
    public List<Item> currentInventory;
    public GameObject slotPreFab;
    public GameObject itemPreFab;
    public GameObject itemArea;
    private int columns = 6;

    public void SetInventory(List<Item> inventory)
    {
        currentInventory = inventory;
        CreateInventory();
    }

    public void CreateInventory()
    {
        int x = 0;
        int y = 0;
        float slotSize = 1f;
        for(int i = 0; i < numberOfSlots; i++)
        {
            GameObject slotInstance = Instantiate(slotPreFab, itemArea.transform);
            slotInstance.transform.position = new Vector2(itemArea.transform.position.x + (x * slotSize), itemArea.transform.position.y + (-y * slotSize));

            x++;

            if (currentInventory.Count >= (x + ((y * columns) + y)))
            {
                GameObject itemInstance = Instantiate(itemPreFab, slotInstance.transform);
                InventoryItem invenItem = itemInstance.GetComponent<InventoryItem>();
                invenItem.item = currentInventory[x-1 + ((y * columns) + y)];
                invenItem.cam = uiCamera;
            }

            if (x > columns)
            {
                x = 0;
                y++;
            }
        }
    }

    public bool AddToInventory(Item item)
    {
        foreach (Transform slot in itemArea.transform)
        {
            if (slot.childCount == 1)
            {
                GameObject itemInstance = Instantiate(itemPreFab, slot.transform);
                InventoryItem invenItem = itemInstance.GetComponent<InventoryItem>();
                invenItem.item = item;
                invenItem.cam = uiCamera;
                itemInstance.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
                return true;
            }
        }
        return false;
    }
}
