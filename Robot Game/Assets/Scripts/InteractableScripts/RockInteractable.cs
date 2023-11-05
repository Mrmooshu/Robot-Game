using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DropTableRoller))]

public class RockInteractable : Interactable
{
    public int baseExp = 1;

    public override void PlayerInRange(MinionEntity playerEntitiy)
    {
        if (playerEntitiy.data == PlayerManager.instance.activeMinion && PlayerManager.instance.activeMinion.HasFunction<MiningFunction>())
        {
            UIManager.instance.actionButton.SetCurrentButton(MineAction, icon);
            playerEntitiy.currentInteractable = this;
        }
    }
    private void MineAction()
    {
        ((MiningFunction)PlayerManager.instance.activeMinion.GetFunction<MiningFunction>()).StartMining();
    }

    public void RollDrop()
    {
        GetComponent<DropTableRoller>().RollDrop(100,150);
    }

    public override void InteractAction(MinionData data)
    {
        if (data.HasFunction<MiningFunction>())
        {
            data.GetFunction<MiningFunction>().level.AddExp(baseExp);
            GetComponent<DropTableRoller>().RollDrop();
        }
    }
}
