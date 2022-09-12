using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public abstract class InteractableEntity : Entity
{
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public abstract void PlayerInRange(PlayerEntity playerEntitiy);

    public virtual void PlayerOutOfRange(PlayerEntity playerEntitiy)
    {
        if (playerEntitiy.core == PlayerManager.instance.activeCore)
        {
            UIManager.instance.actionButton.SetDefaultButton();
            playerEntitiy.currentInteractable = null;
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
