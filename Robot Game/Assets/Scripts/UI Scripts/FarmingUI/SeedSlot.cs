using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeedSlot : MonoBehaviour, IDropHandler
{
    public TextMeshProUGUI nametext;
    public TextMeshProUGUI stagetext;
    public TextMeshProUGUI durationtext;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<InventoryItem>())
        {
            if (Database.GetItem(eventData.pointerDrag.GetComponent<InventoryItem>().item.itemID) is Seed)
            {
                var seed = (Seed)Database.GetItem(eventData.pointerDrag.GetComponent<InventoryItem>().item.itemID);

                eventData.pointerDrag.transform.GetComponentInParent<ISlot>().RemoveFromSlot();
                PlantSeed(seed);
            }
        }
    }

    public void PlantSeed(Seed seed)
    {
        PlayerManager.instance.farming.currentPlot.currentseed = seed;
        PlayerManager.instance.farming.currentPlot.currentstage = seed.numberofstages;
        PlayerManager.instance.farming.currentPlot.stagedurationcountdown = seed.stageduration;
    }

    private void Update()
    {
        if (PlayerManager.instance.farming.currentPlot.currentseed != null)
        {
            nametext.text = PlayerManager.instance.farming.currentPlot.currentseed.itemName;
            stagetext.text = "" + PlayerManager.instance.farming.currentPlot.currentstage;
            durationtext.text = "" + PlayerManager.instance.farming.currentPlot.stagedurationcountdown;
        }
        else
        {
            nametext.text = "Empty";
            stagetext.text = "";
            durationtext.text = "";
        }
    }
}
