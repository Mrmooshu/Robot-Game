using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]

public abstract class Interactable : MonoBehaviour
{
    public Sprite icon;

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public abstract void PlayerInRange(MinionEntity playerEntitiy);

    public virtual void PlayerOutOfRange(MinionEntity playerEntitiy)
    {
        if (playerEntitiy.data == PlayerManager.instance.activeMinion)
        {
            UIManager.instance.actionButton.SetDefaultButton();
            playerEntitiy.currentInteractable = null;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<MinionEntity>() != null)
            {
                PlayerInRange(collision.GetComponent<MinionEntity>());
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<MinionEntity>() != null)
            {
                PlayerOutOfRange(collision.GetComponent<MinionEntity>());
            }
        }
    }

    public virtual void InteractAction(MinionData data)
    {
    }
}
