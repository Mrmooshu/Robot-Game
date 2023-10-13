using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SmeltingQueue : MonoBehaviour, IDropHandler
{
    public GameObject smeltTaskPrefab;

    private TextMeshProUGUI capacityText;

    protected void Start()
    {
        capacityText = transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>();
        SmithingData.SmithingStation.SmithTask.smithTaskUpdated += RefreshList;
        RefreshList();
    }

    private void OnDestroy()
    {
        SmithingData.SmithingStation.SmithTask.smithTaskUpdated -= RefreshList;
    }

    public virtual void RefreshList()
    {
        CreateList();
        UpdateCapacity();
    }

    public void UpdateCapacity()
    {
        capacityText.text = "Capacity " + PlayerManager.instance.smithing.currentStation.currentCapacity + "/" + PlayerManager.instance.smithing.currentStation.capacityLimit;
    }

    protected void CreateList()
    {
        var content = transform.GetChild(0);
        var alreadyMadeTasks = new List<SmithingData.SmithingStation.SmithTask>();

        foreach (Transform child in content.transform)
        {
            child.GetComponentInChildren<SmeltOutputSlot>().task.CheckToFinishTask();
            if (!PlayerManager.instance.smithing.currentStation.tasks.Contains(child.GetComponentInChildren<SmeltOutputSlot>().task))
            {
                Destroy(child.gameObject);
            }
            else
            {
                alreadyMadeTasks.Add(child.GetComponentInChildren<SmeltOutputSlot>().task);
            }
        }

        foreach (var task in PlayerManager.instance.smithing.currentStation.tasks)
        {
            if (!alreadyMadeTasks.Contains(task))
            {
                GameObject taskGo = Instantiate(smeltTaskPrefab, content.transform);
                taskGo.GetComponentInChildren<SmeltOutputSlot>().task = task;
                taskGo.transform.GetChild(0).GetComponent<Image>().sprite = Database.GetItem(task.input.itemID).sprite;
                if (task.task == null)
                {
                    task.Initialize();
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (PlayerManager.instance.smithing.currentStation.currentCapacity >= PlayerManager.instance.smithing.currentStation.capacityLimit)
        {
            return;
        }

        if (eventData.pointerDrag.GetComponent<InventoryItem>())
        {
            if (!Database.GetItem(eventData.pointerDrag.GetComponent<InventoryItem>().item.itemID).tags.Contains(ItemData.ItemTags.Smeltable))
            {
                return;
            }
            var alreadyInList = PlayerManager.instance.smithing.currentStation.tasks.Find(x => x.input.itemID == eventData.pointerDrag.GetComponent<InventoryItem>().item.itemID);
            if (alreadyInList != null)
            {
                ItemInventory.MoveUpToLimit(eventData.pointerDrag.transform.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory, ref alreadyInList.input, eventData.pointerDrag.transform.parent.GetComponent<ItemInventorySlot>().inventoryIndex, Movelimit(alreadyInList.input));
                RefreshList();
                return;
            }

            var task = new SmithingData.SmithingStation.SmithTask();
            ItemInventory.MoveUpToLimit(eventData.pointerDrag.transform.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory, ref task.input, eventData.pointerDrag.transform.parent.GetComponent<ItemInventorySlot>().inventoryIndex, Movelimit(new Item(0,0)));
            task.targetProgress = Database.GetSmeltingRecipe(eventData.pointerDrag.GetComponent<InventoryItem>().item.itemID).progress;
            task.progress = 0;
            PlayerManager.instance.smithing.currentStation.tasks.Add(task);
            task.Initialize();
            RefreshList();

            int Movelimit(Item item)
            {
                return (int)item.quanity + PlayerManager.instance.smithing.currentStation.capacityLimit - PlayerManager.instance.smithing.currentStation.currentCapacity;
            }
        }
    }
}
