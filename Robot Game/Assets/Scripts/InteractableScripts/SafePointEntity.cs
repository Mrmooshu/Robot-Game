using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePointEntity : InteractableEntity
{
    public override void PlayerInRange(PlayerEntity playerEntitiy)
    {
        if (playerEntitiy.core == PlayerManager.instance.activeCore)
        {
            UIManager.instance.actionButton.SetCurrentButton(SafeAction, UIManager.instance.uiSprites.GetSprite("Action Buttons_7"),
                UIManager.instance.uiSprites.GetSprite("Action Buttons_8"), UIManager.instance.uiSprites.GetSprite("Action Buttons_6"));
            playerEntitiy.currentInteractable = this;
        }
    }
    private void SafeAction()
    {
        GameObject menu = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("SafePointMenu");
        UIManager.ChangeMenu(menu, true);
    }
}
