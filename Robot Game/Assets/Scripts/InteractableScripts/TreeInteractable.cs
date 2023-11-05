using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DropTableRoller))]

public class TreeInteractable : Interactable
{
    public override void PlayerInRange(MinionEntity playerEntitiy)
    {
        if (playerEntitiy.data == PlayerManager.instance.activeMinion && PlayerManager.instance.activeMinion.HasFunction<WoodcuttingFunction>())
        {
            UIManager.instance.actionButton.SetCurrentButton(WoodcutAction, icon);
            playerEntitiy.currentInteractable = this;
        }
    }
    private void WoodcutAction()
    {
        ((WoodcuttingFunction)PlayerManager.instance.activeMinion.GetFunction<WoodcuttingFunction>()).StartChopping();
    }

    public void RollDrop()
    {
        GetComponent<DropTableRoller>().RollDrop(100, 150);
    }
}
