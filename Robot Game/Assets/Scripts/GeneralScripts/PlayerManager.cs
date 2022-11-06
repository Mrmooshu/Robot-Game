using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System;

public class PlayerManager : MonoBehaviour, IDataSave
{
    public static PlayerManager instance;

    public PlayerCore[] cores;
    public BodyInventory bodies;
    public ItemInventory bankInventory;

    public List<PlayerEntity> players;

    public GameObject golemBlueprint;
    public GameObject sentinelBlueprint;
    public GameObject automatonBlueprint;
    public Camera mainCam;
    public GameObject PlayerHolderObject;

    public PlayerCore activeCore;

    public event Action playerChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            players = new List<PlayerEntity>();
            foreach (PlayerCore core in cores)
            {
                if (core != null)
                {
                    Spawn(core);
                }
            }
            SetActiveCore(cores[0]);
        }
    }

    public void Spawn(PlayerCore core)
    {
        VariantData variant = Database.GetVariant(core.currentBody.variantName);
        GameObject newPlayer;
        switch (variant.type)
        {
            case VariantData.Type.golem:
                newPlayer = Instantiate(golemBlueprint, PlayerHolderObject.transform);
                break;
            case VariantData.Type.sentinel:
                newPlayer = Instantiate(sentinelBlueprint, PlayerHolderObject.transform);
                break;
            case VariantData.Type.automaton:
                newPlayer = Instantiate(automatonBlueprint, PlayerHolderObject.transform);
                break;
            default:
                Debug.Log("failed to spawn");
                return;
        }
        PlayerEntity playerEntity = newPlayer.GetComponent<PlayerEntity>();
        playerEntity.Initialize(core);
        playerEntity.upperAnimator.runtimeAnimatorController = variant.animController;
        newPlayer.transform.position = playerEntity.core.position;
        players.Add(playerEntity);
    }

    public void Respawn(PlayerCore core)
    {
        core.position = core.GetPlayer().transform.position;
        Destroy(core.GetPlayer().gameObject);
        players.Remove(core.GetPlayer());
        Spawn(core);
        SetActiveCore(core);
    }

    public void SetActiveCore(PlayerCore core)
    {
        activeCore = core;
        ControlThisPlayer(core.GetPlayer());
        playerChanged?.Invoke();
    }

    private void ControlThisPlayer(PlayerEntity player)
    {
        mainCam.GetComponent<TargetFollow>().followTransform = player.transform;
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
        instance.activeCore.GetPlayer().transform.position = Database.GetSafePoint(safePointName).cord;
    }

    /// <summary>
    /// Use this to find if an item is in an inventory.
    /// </summary>
    /// /// <param name ="itemID">The ID of the item being checked for. </param>
    /// <returns>The quanity of the item if found or 0 if not. </returns>
    public static BigInteger CheckCurrentInventoryForItem(int itemID)
    {
        return CheckInventoryForItem(itemID, instance.activeCore.inventory);
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

    public void LoadData(GameData data)
    {
        cores = data.cores;
        bodies = data.bodyInventory;
        bankInventory = data.bankInventory;
    }

    public void SaveData(ref GameData data)
    {
        data.cores = cores;
        data.bodyInventory = bodies;
        data.bankInventory = bankInventory;
    }
}
