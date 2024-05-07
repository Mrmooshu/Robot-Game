using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SafePointEntity : Interactable
{
    public static List<SafePointEntity> safepoints = new List<SafePointEntity>();

    public string locationName = "default name";
    public GameObject teleportSpot;

    public static List<MinionEntity> minionsInRangeToInteract;
    public static MinionData selectedMinion;

    public static event Action InRangeChange;

    private void Awake()
    {
        minionsInRangeToInteract = new List<MinionEntity>();
        safepoints.Add(this);
    }

    public override void PlayerInRange(MinionEntity playerEntitiy)
    {
        if (playerEntitiy.data == PlayerManager.instance.activeMinion)
        {
            UIManager.instance.actionButton.SetCurrentButton(SafeAction, icon);
            playerEntitiy.currentInteractable = this;
        }
        minionsInRangeToInteract.Add(playerEntitiy);
        InRangeChange?.Invoke();
        if (!PlayerManager.instance.universal.unlockedSafepoints.Contains(locationName))
        {
            PlayerManager.instance.universal.unlockedSafepoints.Add(locationName);
        }
    }

    public override void PlayerOutOfRange(MinionEntity playerEntitiy)
    {
        base.PlayerOutOfRange(playerEntitiy);
        minionsInRangeToInteract.Remove(playerEntitiy);
        InRangeChange?.Invoke();
    }

    private void SafeAction()
    {
        RemoteMenuToggle.ToggleThis("SafepointToggle");
    }
}
