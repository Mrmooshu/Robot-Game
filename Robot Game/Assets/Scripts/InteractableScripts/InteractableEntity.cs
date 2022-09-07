using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public abstract class InteractableEntity : Entity
{
    protected PlayerEntity player;

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public abstract void PlayerInRange(PlayerEntity player);

    public virtual void PlayerOutOfRange(PlayerEntity player)
    {
        if (player.core == PlayerManager.instance.activeCore)
        {
            UIManager.instance.actionButton.SetCurrentButton(ActionButton.buttons.none);
            player.currentInteractable = null;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerEntity>() != null)
            {
                PlayerInRange(collision.GetComponent<PlayerEntity>());
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerEntity>() != null)
            {
                PlayerOutOfRange(collision.GetComponent<PlayerEntity>());
            }
        }
    }
}
