using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DropTableRoller))]

public class RockEntity : InteractableEntity
{
    public override void PlayerInRange(PlayerEntity playerEntitiy)
    {
        if (playerEntitiy.core == PlayerManager.instance.activeCore && playerEntitiy is GolemEntity)
        {
            UIManager.instance.actionButton.SetCurrentButton(ActionButton.buttons.mine);
            playerEntitiy.currentInteractable = this;
        }
    }

    public void RollDrop()
    {
        GetComponent<DropTableRoller>().RollDrop(100,150);
    }
}
