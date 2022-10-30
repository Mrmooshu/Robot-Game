using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTool : MonoBehaviour
{
    private PlayerEntity player;
    public Animator toolAnimator { get; protected set; }

    private void Start()
    {
        player = transform.parent.GetComponent<PlayerEntity>();
        toolAnimator = GetComponent<Animator>();
    }

    public void CallToolAction()
    {
        transform.parent.GetComponent<PlayerEntity>().ToolAction();
    }

    public void UpdateAnimators()
    {
        Tool tool = (Tool)Database.GetItem(player.core.currentBody.tool.itemID);

        toolAnimator.runtimeAnimatorController = tool.animController;
    }
}
