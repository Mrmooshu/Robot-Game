using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DropTableRoller))]

public class RockInteractable : Interactable
{
    public override void PlayerInRange(PlayerEntity playerEntitiy)
    {
        if (playerEntitiy.core == PlayerManager.instance.activeCore && playerEntitiy is GolemEntity)
        {
            UIManager.instance.actionButton.SetCurrentButton(MineAction, UIManager.instance.uiSprites.GetSprite("Action Buttons_4"),
                UIManager.instance.uiSprites.GetSprite("Action Buttons_3"), UIManager.instance.uiSprites.GetSprite("Action Buttons_5"));
            playerEntitiy.currentInteractable = this;
        }
    }
    private void MineAction()
    {
        ((GolemEntity)PlayerManager.instance.activeCore.GetPlayer()).ToggleMining();
    }

    public void RollDrop()
    {
        GetComponent<DropTableRoller>().RollDrop(100,150);
    }
}
