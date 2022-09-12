using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableCharacterEntity : InteractableEntity
{
    public Vector2 chatBoxSize = new Vector2(100,100);
    public int chatBoxDistanceAbove = 60;
    public string chatText = "I need something better to say.";

    public override void PlayerInRange(PlayerEntity playerEntitiy)
    {
        UIManager.instance.actionButton.SetCurrentButton(Interact, UIManager.instance.uiSprites.GetSprite("Action Buttons_7"),
            UIManager.instance.uiSprites.GetSprite("Action Buttons_8"), UIManager.instance.uiSprites.GetSprite("Action Buttons_6"));
        playerEntitiy.currentInteractable = this;
    }

    protected virtual void Interact()
    {
        Speak();
    }

    protected virtual void Speak()
    {
        Transform dialogueBox = Instantiate(UIManager.instance.uiPrefabs.LoadAsset<GameObject>("Dialogue Box"), transform).transform.GetChild(0);
        dialogueBox.GetComponent<RectTransform>().sizeDelta = chatBoxSize;
        dialogueBox.localPosition = new Vector2(dialogueBox.localPosition.x, chatBoxDistanceAbove);
        dialogueBox.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(chatBoxSize.x - 10, chatBoxSize.y - 10);
        dialogueBox.GetChild(0).GetComponent<TextMeshProUGUI>().text = chatText;
    }
}
