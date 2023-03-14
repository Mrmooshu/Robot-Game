using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public string sceneName;

    public override void PlayerInRange(PlayerEntity playerEntitiy)
    {
        if (playerEntitiy.data == PlayerManager.instance.activePlayer)
        {
            UIManager.instance.actionButton.SetCurrentButton(EnterAction, UIManager.instance.uiSprites.GetSprite("Action Buttons_4"),
                UIManager.instance.uiSprites.GetSprite("Action Buttons_3"), UIManager.instance.uiSprites.GetSprite("Action Buttons_5"));
            playerEntitiy.currentInteractable = this;
        }
    }

    private void EnterAction()
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        SceneManager.MoveGameObjectToScene(PlayerManager.instance.activePlayer.GetEntity().gameObject, SceneManager.GetSceneByName(sceneName));
        PlayerManager.instance.activePlayer.sceneName = sceneName;
    }
}
