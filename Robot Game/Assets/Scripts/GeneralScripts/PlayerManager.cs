using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public int coreSlots = 1;
    public PlayerCore[] cores;

    public int bodySlots = 10;
    public PlayerBody[] bodies;

    public GameObject golemBlueprint;
    public GameObject sentinelBlueprint;
    public GameObject automatonBlueprint;
    public Camera mainCam;

    public int bankSize = 64;
    public Inventory bankInventory;

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
        bankInventory = new Inventory(bankSize);

        cores = new PlayerCore[coreSlots];
        bodies = new PlayerBody[bodySlots];

        bodies[0] = new PlayerBody("Clay Golem");
        bodies[1] = new PlayerBody("Snow Golem");
        cores[0] = new PlayerCore(bodies[0], 40, 10);
        cores[1] = new PlayerCore(bodies[1], 88, 10);
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
        core.bodyObject = newPlayer;
    }

    public void SetActiveCore(PlayerCore core)
    {
        activeCore = core;
        ControlThisPlayer(core.bodyObject.GetComponent<PlayerEntity>());
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
        instance.activeCore.bodyObject.transform.position = Database.GetSafePoint(safePointName).cord;
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
}
