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
        for (int i = slotsPerPage * currentInventory.currentPage - slotsPerPage; i < currentInventory.GetSize() && i < slotsPerPage * currentInventory.currentPage; i++)
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
            pageNumber.GetComponent<TextMeshProUGUI>().text = "" + currentInventory.currentPage;
        }
    }

    public override void UpdateCurrentInventory()
    {
        currentInventory = PlayerManager.instance.minionInventory;
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
