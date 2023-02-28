using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableCharacter : Interactable
{
    public Vector2 chatBoxSize = new Vector2(100,100);
    public int chatBoxDistanceAbove = 60;
    public string chatText = "I need something better to say.";
    public DialogueBox currentDialogueBox;

    public override void PlayerInRange(PlayerEntity playerEntitiy)
    {
        UIManager.instance.actionButton.SetCurrentButton(Interact, UIManager.instance.uiSprites.GetSprite("Action Buttons_7"),
            UIManager.instance.uiSprites.GetSprite("Action Buttons_8"), UIManager.instance.uiSprites.GetSprite("Action Buttons_6"));
        playerEntitiy.currentInteractable = this;
    }

    public override void PlayerOutOfRange(PlayerEntity playerEntitiy)
    {
        base.PlayerOutOfRange(playerEntitiy);
        if (playerEntitiy.data == PlayerManager.instance.activePlayer && currentDialogueBox != null)
        {
            Destroy(currentDialogueBox.gameObject);
            currentDialogueBox = null;
        }
    }

    protected virtual void Interact()
    {
        Speak();
    }

    protected virtual void Speak()
    {
        if (currentDialogueBox != null)
        {
            Destroy(currentDialogueBox.gameObject);
        }
        currentDialogueBox = null;
        Transform dialogueBox = Instantiate(UIManager.instance.uiPrefabs.LoadAsset<GameObject>("Dialogue Box")).transform.GetChild(0);
        currentDialogueBox = dialogueBox.parent.GetComponent<DialogueBox>();
        currentDialogueBox.followTransform = transform;
        dialogueBox.GetComponent<RectTransform>().sizeDelta = chatBoxSize;
        dialogueBox.localPosition = new Vector2(dialogueBox.localPosition.x, chatBoxDistanceAbove);
        dialogueBox.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(chatBoxSize.x - 10, chatBoxSize.y - 10);
        dialogueBox.GetChild(0).GetComponent<TextMeshProUGUI>().text = chatText;
    }
}
