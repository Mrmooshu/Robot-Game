using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmeltingInteractable : Interactable
{
    public override void PlayerInRange(MinionEntity playerEntitiy)
    {
        if (playerEntitiy.data == PlayerManager.instance.activeMinion)
        {
            UIManager.instance.actionButton.SetCurrentButton(SmeltAction, UIManager.instance.uiSprites.GetSprite("Action Buttons_4"),
                UIManager.instance.uiSprites.GetSprite("Action Buttons_3"), UIManager.instance.uiSprites.GetSprite("Action Buttons_5"));
            playerEntitiy.currentInteractable = this;
        }
    }

    private void SmeltAction()
    {
        if (GameObject.Find("UI Canvas/UI(Clone)/Skill Interfaces/Smithing Menu").activeInHierarchy == true)
        {
            UIManager.CloseMainUi();
        }
        else
        {
            GameObject.Find("UI Canvas/UI(Clone)/Skill Interfaces/Smithing Menu").SetActive(true);
        }
    }
}
