using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveAbilityIcon : UIDraggable
{
    public string activeAbility;
    public bool onAbilityBar = false;
    public int stage { get; private set; } = 0;

    private void Start()
    {
        GetComponentInChildren<Image>().sprite = Database.GetActiveAbility(activeAbility).Icon[stage];
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //TODO
        // add examine functionality for offbar
        // add activate functionality for onbar
    }

    public void Activate()
    {
        var am = PlayerManager.instance.activeMinion;
        var ame = am.GetEntity();
        var ability = am.ActiveAbilities[Array.FindIndex(PlayerManager.instance.activeMinion.ActiveAbilities, x => x.name == activeAbility)];

        //check if ability is assigned and if is off of cooldown
        if (ability.name != "" && ability.cooldown <= 0)
        {
            TryToAdvanceStage();
            ame.CalculateASForAnimator();
            ame.SendMessage(ability.name, this);
        }
    }

    public void TryToAdvanceStage()
    {
        if (stage+1 < Database.GetActiveAbility(activeAbility).Icon.Length)
        {
            stage++;
            GetComponentInChildren<Image>().sprite = Database.GetActiveAbility(activeAbility).Icon[stage];
        }
    }

    public void ResetStage()
    {
        stage = 0;
        GetComponentInChildren<Image>().sprite = Database.GetActiveAbility(activeAbility).Icon[stage];
    }
}
