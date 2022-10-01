using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System;

public class PlayerManager : MonoBehaviour, IDataSave
{
    public static PlayerManager instance;

    public int coreSlots = 1;
    public PlayerCore[] cores;

    public int bodySlots = 10;
    public PlayerBody[] bodies;

    public List<PlayerEntity> players;

    public GameObject golemBlueprint;
    public GameObject sentinelBlueprint;
    public GameObject automatonBlueprint;
    public Camera mainCam;

    public int bankSize;
    public ItemInventory bankInventory;

    public PlayerCore activeCore;

    public event Action playerChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Initialize()
    {
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

    public void Spawn(PlayerCore core)
    {
        VariantData variant = Database.GetVariant(core.currentBody.variantName);
        GameObject newPlayer;
        switch (variant.type)
        {
            case VariantData.Type.golem:
                newPlayer = Instantiate(golemBlueprint, transform);
                break;
            case VariantData.Type.sentinel:
                newPlayer = Instantiate(sentinelBlueprint, transform);
                break;
            case VariantData.Type.automaton:
                newPlayer = Instantiate(automatonBlueprint, transform);
                break;
            default:
                Debug.Log("failed to spawn");
                return;
        }
        PlayerEntity playerEntity = newPlayer.GetComponent<PlayerEntity>();
        playerEntity.Initialize(core, variant.moveSpeed, variant.jumpForce);
        newPlayer.GetComponent<Animator>().runtimeAnimatorController = variant.animController;
        players.Add(playerEntity);
    }

    public void SetActiveCore(PlayerCore core)
    {
        activeCore = core;
        ControlThisPlayer(core.GetPlayer());
        playerChanged?.Invoke();
    }

    private void ControlThisPlayer(PlayerEntity player)
    {
        player.TakeControl();
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
        foreach(Item item in instance.activeCore.inventory.inventory)
        {
            if (item != null)
            {
                if (item.itemID == itemID)
                {
                    return item.quanity;
                }
            }
        }
        return 0;
    }

    /// <summary>
    /// Use this to find if an item is in the bank inventory.
    /// </summary>
    /// /// <param name ="itemID">The ID of the item being checked for. </param>
    /// <returns>The quanity of the item if found or 0 if not. </returns>
    public static BigInteger CheckBankInventoryForItem(int itemID)
    {
        foreach (Item item in instance.bankInventory.inventory)
        {
            if (item.itemID == itemID)
            {
                return item.quanity;
            }
        }
        return 0;
    }

    public void LoadData(GameData data)
    {
        cores = data.cores;
        bodies = data.bodies;
        bankInventory = data.bankInventory;
    }

    public void SaveData(ref GameData data)
    {
        data.cores = cores;
        data.bodies = bodies;
        data.bankInventory = bankInventory;
    }
}
