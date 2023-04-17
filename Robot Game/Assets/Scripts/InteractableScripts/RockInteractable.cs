using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DropTableRoller))]

public class RockInteractable : Interactable
{
    //TODO needt oupdate this so that it works for minions
    public override void PlayerInRange(MinionEntity playerEntitiy)
    {
        if (playerEntitiy.data == PlayerManager.instance.activeMinion && PlayerManager.instance.activeMinion.GetEntity() is GolemEntity)
        {
            UIManager.instance.actionButton.SetCurrentButton(MineAction, UIManager.instance.uiSprites.GetSprite("Action Buttons_4"),
                UIManager.instance.uiSprites.GetSprite("Action Buttons_3"), UIManager.instance.uiSprites.GetSprite("Action Buttons_5"));
            playerEntitiy.currentInteractable = this;
        }
    }
    private void MineAction()
    {
        ((GolemEntity)(PlayerManager.instance.activeMinion.GetEntity())).ToggleMining();
    }

    public void RollDrop()
    {
        GetComponent<DropTableRoller>().RollDrop(100,150);
    }
}
