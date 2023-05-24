using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionUploadPanel : MonoBehaviour
{
    public GameObject minionOptionPrefab;

    protected void Start()
    {
        SafePointEntity.InRangeChange += RefreshList;
        RefreshList();
    }

    private void OnDestroy()
    {
        SafePointEntity.InRangeChange -= RefreshList;
    }

    public virtual void RefreshList()
    {
        CreateList();
    }

    protected void CreateList()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (MinionEntity minion in SafePointEntity.minionsInRangeToInteract)
        {
            if (!(PlayerManager.instance.activeMinion == minion.data))
            {
                GameObject option = Instantiate(minionOptionPrefab, transform);
                InventoryPlayer invenPlayer = option.GetComponent<InventoryPlayer>();
                invenPlayer.unit = minion.data;
                option.transform.GetChild(0).GetComponent<Image>().sprite = GeneralManager.instance.entityPrefabs.LoadAsset<GameObject>(minion.data.variantName).GetComponent<MinionEntity>().icon;
            }
        }
    }
}
