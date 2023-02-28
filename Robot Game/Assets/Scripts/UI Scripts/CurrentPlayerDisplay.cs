using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentPlayerDisplay : SlotDisplay<MinionData>, IDropHandler, ISlot
{

    //TODO this class needs to be fixed
    private void Start()
    {
        objectPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("InventoryPlayer");
        CreateSlot();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Swap(eventData.pointerDrag.transform);
        }
    }

    public void Swap(Transform inventoryItem)
    {
        if (inventoryItem.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().currentInventory.inventory[inventoryItem.parent.GetComponent<PlayerInventorySlot>().inventoryIndex] != null)
        {
           //MinionInventory.Move(inventoryItem.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().currentInventory, ref PlayerManager.instance.activePlayer.currentBody, inventoryItem.parent.GetComponent<PlayerInventorySlot>().inventoryIndex);
            inventoryItem.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().RefreshInventory();
            RefreshSlot();
        }
    }

    public override void RemoveFromSlot()
    {
        Debug.Log("should never call this function");
    }

    public override void RefreshSlot()
    {
        CreateSlot();
        //PlayerManager.instance.Respawn(PlayerManager.instance.activePlayer);
    }

    protected override void CreateSlot()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<InventoryPlayer>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        GameObject bodyInstance = Instantiate(objectPrefab, transform);
        InventoryPlayer invenPlayer = bodyInstance.GetComponent<InventoryPlayer>();
        //invenPlayer.unit = PlayerManager.instance.activePlayer.currentBody;
        invenPlayer.transform.GetChild(0).GetComponent<Image>().sprite = PlayerManager.instance.activePlayer.GetEntity().GetComponent<SpriteRenderer>().sprite;
    }
}
