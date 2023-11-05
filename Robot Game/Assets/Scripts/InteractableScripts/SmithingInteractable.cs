using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithingInteractable : Interactable
{
    public int smithIndex;

    public override void PlayerInRange(MinionEntity playerEntitiy)
    {
        if (playerEntitiy.data == PlayerManager.instance.activeMinion)
        {
            UIManager.instance.actionButton.SetCurrentButton(SmeltAction, icon);
            playerEntitiy.currentInteractable = this;
        }
    }

    private void SmeltAction()
    {
        RemoteMenuToggle.ToggleThis(RemoteMenuToggle.instance.smithingToggle);
        PlayerManager.instance.smithing.currentStation = PlayerManager.instance.smithing.stations[smithIndex];
    }
}
