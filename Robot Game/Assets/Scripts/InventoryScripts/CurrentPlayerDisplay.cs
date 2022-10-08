using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentPlayerDisplay : MonoBehaviour, IDropHandler
{
    protected GameObject slotPrefab;
    protected GameObject objectPrefab;

    private void Start()
    {
        slotPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("PlayerSlot");
        objectPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("InventoryPlayer");
        RefreshSlot();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            BodyInventory.Move(eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().currentInventory, PlayerManager.instance.activeCore.currentBody, eventData.pointerDrag.transform.parent.GetComponent<PlayerInventorySlot>().inventoryIndex);
            eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<BodyInventoryDisplay>().RefreshInventory();
            RefreshSlot();
        }
    }

    public void RefreshSlot()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<PlayerInventorySlot>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        GameObject bodyInstance = Instantiate(objectPrefab, transform);
        InventoryPlayer invenPlayer = bodyInstance.GetComponent<InventoryPlayer>();
        invenPlayer.body = PlayerManager.instance.activeCore.currentBody;
        invenPlayer.transform.GetChild(0).GetComponent<Image>().sprite = Database.GetVariant(PlayerManager.instance.activeCore.currentBody.variantName).sprite;
    }
}
