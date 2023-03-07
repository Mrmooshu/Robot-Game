using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System;
using Cinemachine;

public class PlayerManager : MonoBehaviour, IDataSave
{
    public static PlayerManager instance;



    public List<PlayerData> players;
    public List<MinionData> activeMinions;
    public MinionInventory minionInventory;
    public ItemInventory bankInventory;

    public List<PlayerEntity> playerEntities;
    public List<MinionEntity> minionEntities;

    [SerializeField] private GameObject playerPrefab;
    public CinemachineVirtualCamera virtualCam;
    public GameObject PlayerHolderObject;

    public PlayerData activePlayer;

    public event Action playerChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            playerEntities = new List<PlayerEntity>();
            foreach (PlayerData player in players)
            {
                if (player != null)
                {
                    SpawnPlayer(player);
                }
            }
            SetActivePlayer(players[0]);
            foreach (MinionData core in activeMinions)
            {
                if (core != null)
                {
                    SpawnMinion(core);
                }
            }
        }
    }

    public void SpawnPlayer(PlayerData player)
    {
        GameObject newPlayer;
        newPlayer = Instantiate(playerPrefab, PlayerHolderObject.transform);
        PlayerEntity playerEntity = newPlayer.GetComponent<PlayerEntity>();
        playerEntity.Initialize(player);
        newPlayer.transform.position = playerEntity.data.position;
        playerEntities.Add(playerEntity);
    }

    public void SpawnMinion(MinionData minion)
    {
        GameObject newMinion;
        newMinion = Instantiate(GeneralManager.instance.entityPrefabs.LoadAsset<GameObject>(minion.variantName), PlayerHolderObject.transform);
        MinionEntity minionEntity = newMinion.GetComponent<MinionEntity>();
        minionEntity.Initialize(minion);
        newMinion.transform.position = minionEntity.data.position;
        minionEntities.Add(minionEntity);
    }

    public void RespawnMinion(MinionData minion)
    {
        minion.position = minion.GetEntity().transform.position;
        Destroy(minion.GetEntity().gameObject);
        minionEntities.Remove((MinionEntity)minion.GetEntity());
        SpawnMinion(minion);
    }

    public void SetActivePlayer(PlayerData player)
    {
        activePlayer = player;
        ControlThisPlayer((PlayerEntity)player.GetEntity());
        playerChanged?.Invoke();
    }

    private void ControlThisPlayer(PlayerEntity player)
    {
        virtualCam.Follow = player.transform;
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
        instance.activePlayer.GetEntity().transform.position = Database.GetSafePoint(safePointName).cord;
    }

    /// <summary>
    /// Use this to find if an item is in an inventory.
    /// </summary>
    /// /// <param name ="itemID">The ID of the item being checked for. </param>
    /// <returns>The quanity of the item if found or 0 if not. </returns>
    public static BigInteger CheckCurrentInventoryForItem(int itemID)
    {
        return CheckInventoryForItem(itemID, instance.activePlayer.inventory);
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
        players = data.players;
        activeMinions = data.activeMinions;
        minionInventory = data.minionInventory;
        bankInventory = data.bankInventory;
    }

    public void SaveData(ref GameData data)
    {
        data.players = players;
        data.activeMinions = activeMinions;
        data.minionInventory = minionInventory;
        data.bankInventory = bankInventory;
    }
}
