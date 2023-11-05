using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class ActionButton : MonoBehaviour
{
    public Sprite defaultIcon;
    private Button button { get => GetComponent<Button>(); }

    public GameObject iconGo;

    private void Start()
    {
        SetDefaultButton();
    }

    public void SetDefaultButton()
    {
        // clear any listners on this button
        button.onClick.RemoveAllListeners();

        // set sprites and action for button
        //SpriteState state = new SpriteState();
        iconGo.GetComponent<Image>().sprite = defaultIcon;
        //state.highlightedSprite = UIManager.instance.uiSprites.GetSprite("Action Buttons_0");
        //state.pressedSprite = UIManager.instance.uiSprites.GetSprite("Action Buttons_2");
        //button.spriteState = state;
    }

    public void SetCurrentButton(UnityAction action, Sprite buttonSprite)
    {
        // clear any listners on this button
        button.onClick.RemoveAllListeners();

        // set sprites and action for button
        //SpriteState state = new SpriteState();
        button.onClick.AddListener(action);
        iconGo.GetComponent<Image>().sprite = buttonSprite;
        //state.highlightedSprite = highlightedSprite;
        //state.pressedSprite = pressedSprite;
        //button.spriteState = state;
    }
}
