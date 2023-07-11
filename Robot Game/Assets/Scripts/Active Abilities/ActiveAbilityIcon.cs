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

    private MinionData am;
    private MinionEntity ame;

    private void Start()
    {
        am = PlayerManager.instance.activeMinion;
        ame = am.GetEntity();

        if (onAbilityBar)
        {
            GetComponentInChildren<Image>().sprite = Database.GetActiveAbility(activeAbility).Icon[GetActive().stage];
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //TODO
        // add examine functionality for offbar
        // add activate functionality for onbar
    }

    public void Activate()
    {
        ref var ability = ref GetActive();

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
        ref var ability = ref GetActive();

        if (ability.stage+1 < Database.GetActiveAbility(activeAbility).Icon.Length)
        {
            ability.stage++;
            GetComponentInChildren<Image>().sprite = Database.GetActiveAbility(activeAbility).Icon[ability.stage];
        }
    }

    public void ResetStage()
    {
        ref var ability = ref GetActive();

        ability.stage = 0;
        GetComponentInChildren<Image>().sprite = Database.GetActiveAbility(activeAbility).Icon[ability.stage];
    }

    public ref MinionData.active GetActive()
    {
        return ref am.ActiveAbilities[Array.FindIndex(PlayerManager.instance.activeMinion.ActiveAbilities, x => x.name == activeAbility)];
    }
}
