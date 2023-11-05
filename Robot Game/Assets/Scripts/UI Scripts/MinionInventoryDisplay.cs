using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * Inventory Display used to store minions
 * 
 * 
 */
public class MinionInventoryDisplay : InventoryDisplay
{

    public MinionInventory currentInventory;

    public void Start()
    {
        slotPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("PlayerSlot");
        objectPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("InventoryPlayer");
        RefreshInventory();
        PlayerManager.instance.minionChanged += RefreshInventory;
        MinionInventory.inventoryUpdated += RefreshInventory;
    }

    private void OnDestroy()
    {
        PlayerManager.instance.minionChanged -= RefreshInventory;
        MinionInventory.inventoryUpdated -= RefreshInventory;
    }

    protected override void CreateInventory()
    {
        foreach (Transform child in inventoryArea.transform)
        {
            if (child.GetComponent<PlayerInventorySlot>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        int x = 0;
        int y = 0;
        float slotSize = 39f;
        for (int i = 0; i < currentInventory.GetSize(); i++)
        {
            GameObject slotInstance = Instantiate(slotPrefab, inventoryArea.transform);
            slotInstance.transform.localPosition = new Vector2((x * slotSize), (-y * slotSize));
            slotInstance.GetComponent<PlayerInventorySlot>().inventoryIndex = i;

            x++;

            if (currentInventory.GetSlotByIndex(i) != null)
            {
                GameObject bodyInstance = Instantiate(objectPrefab, slotInstance.transform);
                InventoryPlayer invenPlayer = bodyInstance.GetComponent<InventoryPlayer>();
                invenPlayer.unit = currentInventory.GetSlotByIndex(i);
                invenPlayer.transform.GetChild(0).GetComponent<Image>().sprite = GeneralManager.instance.entityPrefabs.LoadAsset<GameObject>(currentInventory.GetSlotByIndex(i).variantName).GetComponent<MinionEntity>().icon;
            }

            if (x >= columns)
            {
                x = 0;
                y++;
            }
        }
    }

    public override void UpdateCurrentInventory()
    {
        currentInventory = PlayerManager.instance.minionInventory;
    }

    //static methods
    public static void UploadMinion()
    {
        SafePointEntity.UploadMinion();
    }

    public static void DeployMinion()
    {
        SafePointEntity.DeployMinion();
    }

    public static void SwapMinion()
    {
        SafePointEntity.SwapMinion();
    }
}
