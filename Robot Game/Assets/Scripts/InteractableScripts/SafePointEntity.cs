using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePointEntity : InteractableEntity
{
    public override void PlayerInRange(PlayerEntity playerEntitiy)
    {
        if (playerEntitiy.core == PlayerManager.instance.activeCore)
        {
            UIManager.instance.actionButton.SetCurrentButton(ActionButton.buttons.safe);
            playerEntitiy.currentInteractable = this;
        }
    }
}
