using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class ActionButton : Button
{
    protected override void Start()
    {
        base.Start();
        SetDefaultButton();
    }

    public void SetDefaultButton()
    {
        // clear any listners on this button
        onClick.RemoveAllListeners();

        // set sprites and action for button
        SpriteState state = new SpriteState();
        GetComponent<Image>().sprite = UIManager.instance.uiSprites.GetSprite("Action Buttons_1");
        state.highlightedSprite = UIManager.instance.uiSprites.GetSprite("Action Buttons_0");
        state.pressedSprite = UIManager.instance.uiSprites.GetSprite("Action Buttons_2");
        spriteState = state;
    }

    public void SetCurrentButton(UnityAction action, Sprite button, Sprite highlighted, Sprite pressed)
    {
        // clear any listners on this button
        onClick.RemoveAllListeners();

        // set sprites and action for button
        SpriteState state = new SpriteState();
        onClick.AddListener(action);
        GetComponent<Image>().sprite = button;
        state.highlightedSprite = highlighted;
        state.pressedSprite = pressed;
        spriteState = state;
    }
}
