using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour, IDataSave
{
    public List<GameObject> minionBlueprints;

    public static PlayerManager instance;
    public UniversalPlayerData universal;
    public List<MinionData> activeMinions;
    public MinionInventory minionInventory;
    public ItemInventory bankInventory;

    public List<MinionEntity> minionEntities;

    public MinionData activeMinion;

    public event Action minionChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SpawnMinions();
        }
    }
    public void SpawnMinions()
    {
        UniversalPlayerData.InitializePassives();
        foreach (MinionData core in activeMinions)
        {
            if (core != null)
            {
                SpawnMinion(core);
            }
        }
        SetActiveMinion(activeMinions[0]);
    }

    public void SpawnMinion(MinionData minion)
    {
        GameObject newMinion;
        //newMinion = Instantiate(GeneralManager.instance.entityPrefabs.LoadAsset<GameObject>(minion.variantName));
        newMinion = Instantiate(minionBlueprints.Where(x => x.name == minion.variantName).Single());
        MinionEntity minionEntity = newMinion.GetComponent<MinionEntity>();
        minionEntities.Add(minionEntity);
        minionEntity.Initialize(minion);
        UniversalPlayerData.AddMinionPassives(minionEntity);
        newMinion.transform.position = minionEntity.data.savedPosition;
        minion.activity = MinionData.Activity.Idle;
        minionChanged?.Invoke();
    }

    public void StoreMinion(MinionData minion)
    {
        minion.activity = MinionData.Activity.Storage;
        DespawnMinion(minion);
    }

    public void DespawnMinion(MinionData minion)
    {
        var entity = minion.GetEntity();
        UniversalPlayerData.RemoveMinionPassives(minion.GetEntity());
        minion.savedPosition = minion.GetEntity().transform.position;
        minionEntities.Remove(minion.GetEntity());
        Destroy(entity.gameObject);
        minionChanged?.Invoke();
    }

    public void RespawnMinion(MinionData minion)
    {
        DespawnMinion(minion);
        SpawnMinion(minion);
    }

    public void SetActiveMinion(MinionData minion)
    {
        if (activeMinion.GetEntity() != null)
        {
            activeMinion.GetEntity().GetComponent<SortingGroup>().sortingOrder = 0;
        }
        activeMinion = minion;
        activeMinion.GetEntity().GetComponent<SortingGroup>().sortingOrder = 1;
        ControlThisMinion(minion.GetEntity());
        minionChanged?.Invoke();
    }

    private void ControlThisMinion(MinionEntity player)
    {
        GeneralManager.instance.virtualCam.Follow = player.transform;
        player.transform.SetAsLastSibling();
        if (player.currentInteractable != null)
        {
            player.currentInteractable.PlayerInRange(player);
        }
        else
        {
            UIManager.instance.actionButton.SetDefaultButton();
        }
    }
    public static void TeleportHere(string safePointName)
    {
        instance.activeMinion.GetEntity().transform.position = Database.GetSafePoint(safePointName).teleportSpot.transform.position;
    }

    /// <summary>
    /// Use this to find if an item is in an inventory.
    /// </summary>
    /// /// <param name ="itemID">The ID of the item being checked for. </param>
    /// <returns>The quanity of the item if found or 0 if not. </returns>
    public static BigInteger CheckCurrentInventoryForItem(int itemID)
    {
        return CheckInventoryForItem(itemID, instance.activeMinion.inventory);
    }

    /// <summary>
    /// Use this to find if an item is in the bank inventory.
    /// </summary>
    /// /// <param name ="itemID">The ID of the item being checked for. </param>
    /// <returns>The quanity of the item if found or 0 if not. </returns>
    public static BigInteger CheckBankInventoryForItem(int itemID)
    {
        return CheckInventoryForItem(itemID, instance.bankInventory);
    }

    public static BigInteger CheckInventoryForItem(int itemID, ItemInventory inventory)
    {
        BigInteger total = 0;
        foreach (Item item in inventory.inventory)
        {
            if (item != null)
            {
                if (item.itemID == itemID)
                {
                    total += item.quanity;
                }
            }
        }
        return total;
    }

    public void InvokeMinionChange()
    {
        minionChanged?.Invoke();
    }

    public void LoadData(GameData data)
    {
        universal = data.universal;
        activeMinions = data.activeMinions;
        minionInventory = data.minionInventory;
        bankInventory = data.bankInventory;
    }

    public void SaveData(ref GameData data)
    {
        data.universal = universal;
        data.activeMinions = activeMinions;
        data.minionInventory = minionInventory;
        data.bankInventory = bankInventory;
    }
}
