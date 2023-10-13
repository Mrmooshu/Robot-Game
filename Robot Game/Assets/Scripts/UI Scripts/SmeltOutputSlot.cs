using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SmeltOutputSlot : SlotDisplay
{
    public SmithingData.SmithingStation.SmithTask task;

    protected TextMeshProUGUI inputStackText;
    protected Image progressBar;
    protected TextMeshProUGUI text;
    protected SmeltingQueue queue;

    public override ref Item GetItem()
    {
        return ref task.output;
    }

    public override void Swap(Transform inventorySlot, bool condition)
    {
        if (!inventorySlot.GetComponentInChildren<InventoryItem>())
        {
            base.Swap(inventorySlot);
        }
        else if (inventorySlot.GetComponentInChildren<InventoryItem>().item != null && GetItem() != null)
        {
            if (inventorySlot.GetComponentInChildren<InventoryItem>().item.itemID == GetItem().itemID)
            {
                base.Swap(inventorySlot);
            }
        }
        GetComponentInParent<SmeltingQueue>().RefreshList();
    }


    private void Awake()
    {
        inputStackText = transform.parent.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        progressBar = transform.parent.GetChild(2).GetChild(1).GetComponent<Image>();
        text = transform.parent.GetChild(2).GetChild(1).GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        queue = transform.parent.parent.parent.GetComponent<SmeltingQueue>();
    }

    private void Update()
    {
        var progress = task.progress;
        var target = task.targetProgress;


        inputStackText.text = task.input.quanity.ToString();
        progressBar.fillAmount = progress / target;
        text.text = (int)progress + "/" + target;

        inputStackText.text = task.input.quanity.ToString();
        queue.UpdateCapacity();
        RefreshSlot();
    }
}
