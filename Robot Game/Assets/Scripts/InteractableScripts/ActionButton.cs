using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActionButton : Button
{
    public enum buttons {
        none, mine, chop, fish
    }

    protected override void Start()
    {
        base.Start();
        SetCurrentButton(buttons.none);
    }

    public void SetCurrentButton(buttons buttonType)
    {
        // clear any listners on this button
        onClick.RemoveAllListeners();

        SpriteState state = new SpriteState();
        switch (buttonType){

            case buttons.none:
                GetComponent<Image>().sprite = UIManager.instance.uiSprites.GetSprite("Action Buttons_1");
                state.highlightedSprite = UIManager.instance.uiSprites.GetSprite("Action Buttons_0");
                state.pressedSprite = UIManager.instance.uiSprites.GetSprite("Action Buttons_2");
                break;

            case buttons.mine:
                onClick.AddListener(MineAction);
                GetComponent<Image>().sprite = UIManager.instance.uiSprites.GetSprite("Action Buttons_4");
                state.highlightedSprite = UIManager.instance.uiSprites.GetSprite("Action Buttons_3");
                state.pressedSprite = UIManager.instance.uiSprites.GetSprite("Action Buttons_5");
                break;
        }
        spriteState = state;
    }

    private void MineAction()
    {
        PlayerManager.instance.activeCore.bodyObject.GetComponent<GolemEntity>().ToggleMining();
    }
}
