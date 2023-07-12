using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;

public class AbilitySlot : MonoBehaviour, IDropHandler
{
    public int abilitySlotIndex;
    public GameObject ActiveAbilityPrefab;
    public GameObject CooldownPrefab;

    public static List<AbilitySlot> instances = new List<AbilitySlot>();

    [NonSerialized] public GameObject iconGO = null;
    [NonSerialized] public GameObject cooldownGO = null;

    private void Start()
    {
        if (instances.Count == 0)
        {
            PlayerManager.instance.minionChanged += Refresh;
        }
        instances.Add(this);
        Refresh();
    }

    private void OnDestroy()
    {
        instances.Remove(this);
        if (instances.Count == 0)
        {
            PlayerManager.instance.minionChanged -= Refresh;
        }
    }

    private void Update()
    {
        if (iconGO != null)
        {
            if (PlayerManager.instance.activeMinion.ActiveAbilities[abilitySlotIndex].cooldown > 0)
            {
                if (!cooldownGO.activeInHierarchy)
                {
                    cooldownGO.SetActive(true);
                }
                cooldownGO.GetComponentInChildren<TextMeshProUGUI>().text = "" + Mathf.Round(PlayerManager.instance.activeMinion.ActiveAbilities[abilitySlotIndex].cooldown * 10) * .1;
            }
            else if (cooldownGO.activeInHierarchy)
            {
                cooldownGO.SetActive(false);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Exit if ability in slot is on cooldown
        if (iconGO != null)
        {
            if (PlayerManager.instance.activeMinion.ActiveAbilities[abilitySlotIndex].cooldown > 0 || iconGO.GetComponent<ActiveAbilityIcon>().GetActive().stage > 0)
            {
                return;
            }
        }


        // index must be within min and max of ability slot indexes
        if (eventData.pointerDrag != null && abilitySlotIndex >= 0 && abilitySlotIndex <= 5 && eventData.pointerDrag.transform.GetComponentInParent<ActiveAbilityIcon>() == true)
        {
            var abilityName = eventData.pointerDrag.transform.GetComponentInParent<ActiveAbilityIcon>().activeAbility;

            //if ability is already on bar then remove it and place it in this index instead
            if (PlayerManager.instance.activeMinion.ActiveAbilities.Any(x => x.name == abilityName))
            {
                var index = Array.FindIndex(PlayerManager.instance.activeMinion.ActiveAbilities, x => x.name == abilityName);


                // Exit if same ability is on bar and on cooldown
                if (PlayerManager.instance.activeMinion.ActiveAbilities[index].cooldown > 0 || instances[index].iconGO.GetComponent<ActiveAbilityIcon>().GetActive().stage > 0)
                {
                    return;
                }

                PlayerManager.instance.activeMinion.ActiveAbilities[Array.FindIndex(PlayerManager.instance.activeMinion.ActiveAbilities, x => x.name == abilityName)].name = "";
                PlayerManager.instance.activeMinion.ActiveAbilities[Array.FindIndex(PlayerManager.instance.activeMinion.ActiveAbilities, x => x.name == abilityName)].stage = 0;
            }
            PlayerManager.instance.activeMinion.ActiveAbilities[abilitySlotIndex].name = abilityName;
            Refresh();
        }
    }

    private static void Refresh()
    {
        foreach (AbilitySlot slot in instances)
        {
            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
            if (Database.GetActiveAbility(PlayerManager.instance.activeMinion.ActiveAbilities[slot.abilitySlotIndex].name) != null)
            {
                // create new ability icon
                GameObject activeAbilityIconInstance = Instantiate(slot.ActiveAbilityPrefab, slot.transform);
                activeAbilityIconInstance.GetComponent<ActiveAbilityIcon>().activeAbility = PlayerManager.instance.activeMinion.ActiveAbilities[slot.abilitySlotIndex].name;
                activeAbilityIconInstance.GetComponent<ActiveAbilityIcon>().onAbilityBar = true;
                // add cooldown gameobject to ability icon
                GameObject cooldownInstance = Instantiate(slot.CooldownPrefab, activeAbilityIconInstance.transform);
                slot.iconGO = activeAbilityIconInstance;
                slot.cooldownGO = cooldownInstance;
            }
            else
            {
                slot.iconGO = null;
                slot.cooldownGO = null;
            }
        }

    }
}
