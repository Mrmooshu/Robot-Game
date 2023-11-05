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
            UIManager.instance.actionButton.SetCurrentButton(EnterAction, icon);
            playerEntitiy.currentInteractable = this;
        }
    }

    private void EnterAction()
    {
        PlayerManager.instance.activeMinion.GetEntity().transform.position = LinkedDoor.transform.position;
    }
}
