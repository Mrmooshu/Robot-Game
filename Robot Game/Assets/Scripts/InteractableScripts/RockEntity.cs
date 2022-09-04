using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DropTableRoller))]

public class RockEntity : InteractableEntity
{
    private GolemEntity player;

    public override void PlayerInRange(PlayerEntity player)
    {
        if (player == PlayerManager.instance.activeCore.bodyObject.GetComponent<PlayerEntity>())
        {
            UIManager.instance.actionButton.SetCurrentButton(ActionButton.buttons.mine);
            player.currentInteractable = this;
        }
    }

    public override void PlayerOutOfRange(PlayerEntity player)
    {
        if (player == PlayerManager.instance.activeCore.bodyObject.GetComponent<PlayerEntity>())
        {
            UIManager.instance.actionButton.SetCurrentButton(ActionButton.buttons.none);
            player.currentInteractable = null;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<GolemEntity>() != null)
            {
                PlayerInRange(collision.GetComponent<GolemEntity>());
            }
            else
            {

            }
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<GolemEntity>() != null)
            {
                PlayerOutOfRange(collision.GetComponent<GolemEntity>());
            }
            else
            {

            }
        }
    }

    public void RollDrop()
    {
        GetComponent<DropTableRoller>().RollDrop(100,150);
    }
}
