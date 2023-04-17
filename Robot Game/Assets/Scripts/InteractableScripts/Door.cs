using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public GameObject LinkedDoor;

    public override void PlayerInRange(MinionEntity playerEntitiy)
    {
        if (playerEntitiy.data == PlayerManager.instance.activeMinion)
        {
            UIManager.instance.actionButton.SetCurrentButton(EnterAction, UIManager.instance.uiSprites.GetSprite("Action Buttons_4"),
                UIManager.instance.uiSprites.GetSprite("Action Buttons_3"), UIManager.instance.uiSprites.GetSprite("Action Buttons_5"));
            playerEntitiy.currentInteractable = this;
        }
    }

    private void EnterAction()
    {
        PlayerManager.instance.activeMinion.GetEntity().transform.position = LinkedDoor.transform.position;
    }
}
