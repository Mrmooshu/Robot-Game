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
}
