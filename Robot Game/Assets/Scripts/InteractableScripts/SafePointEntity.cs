using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SafePointEntity : Interactable
{
    public static List<MinionEntity> minionsInRangeToInteract;
    public static MinionData selectedMinion;

    public static event Action InRangeChange;

    private void Awake()
    {
        minionsInRangeToInteract = new List<MinionEntity>();
    }

    public override void PlayerInRange(MinionEntity playerEntitiy)
    {
        if (playerEntitiy.data == PlayerManager.instance.activeMinion)
        {
            UIManager.instance.actionButton.SetCurrentButton(SafeAction, UIManager.instance.uiSprites.GetSprite("Action Buttons_7"),
                UIManager.instance.uiSprites.GetSprite("Action Buttons_8"), UIManager.instance.uiSprites.GetSprite("Action Buttons_6"));
            playerEntitiy.currentInteractable = this;
        }
        minionsInRangeToInteract.Add(playerEntitiy);
        InRangeChange?.Invoke();
    }

    public override void PlayerOutOfRange(MinionEntity playerEntitiy)
    {
        base.PlayerOutOfRange(playerEntitiy);
        minionsInRangeToInteract.Remove(playerEntitiy);
        InRangeChange?.Invoke();
    }

    private void SafeAction()
    {
        if (GameObject.Find("UI Canvas/UI(Clone)/SafePointMenu").activeInHierarchy == true)
        {
            UIManager.CloseMainUi();
        }
        else
        {
            GameObject.Find("UI Canvas/UI(Clone)/SafePointMenu").SetActive(true);
            GameObject.Find("UI Canvas/UI(Clone)/MainPlayerMenu").SetActive(false);
            GameObject.Find("UI Canvas/UI(Clone)/SwapPlayerMenu").SetActive(false);
        }
    }

    //static methods
    public static void UploadMinion()
    {
        if (PlayerManager.instance.activeMinions.Contains(selectedMinion))
        {
            PlayerManager.instance.minionInventory.inventory[Array.IndexOf(PlayerManager.instance.minionInventory.inventory, null)] = selectedMinion;
            PlayerManager.instance.activeMinions.Remove(selectedMinion);
            PlayerManager.instance.StoreMinion(selectedMinion);
        }
    }

    public static void DeployMinion()
    {
        if (PlayerManager.instance.minionInventory.inventory.ToList().Contains(selectedMinion) && selectedMinion != null)
        {
            var temp = selectedMinion;
            PlayerManager.instance.minionInventory.inventory[Array.IndexOf(PlayerManager.instance.minionInventory.inventory, selectedMinion)] = null;
            PlayerManager.instance.activeMinions.Add(temp);
            temp.savedPosition = PlayerManager.instance.activeMinion.GetEntity().transform.position;
            PlayerManager.instance.SpawnMinion(temp);
        }

    }

    public static void SwapMinion()
    {
        if (PlayerManager.instance.minionInventory.inventory.ToList().Contains(selectedMinion) && selectedMinion != null)
        {
            var temp = selectedMinion;
            var temp2 = PlayerManager.instance.activeMinion;
            PlayerManager.instance.minionInventory.inventory[Array.IndexOf(PlayerManager.instance.minionInventory.inventory, selectedMinion)] = null;
            PlayerManager.instance.activeMinions.Add(temp);
            temp.savedPosition = PlayerManager.instance.activeMinion.GetEntity().transform.position;
            PlayerManager.instance.SpawnMinion(selectedMinion);
            PlayerManager.instance.SetActiveMinion(temp);

            PlayerManager.instance.StoreMinion(temp2);
            PlayerManager.instance.minionInventory.inventory[Array.IndexOf(PlayerManager.instance.minionInventory.inventory, null)] = temp2;
            PlayerManager.instance.activeMinions.Remove(temp2);
            PlayerManager.instance.InvokeMinionChange();
        }
    }
}
