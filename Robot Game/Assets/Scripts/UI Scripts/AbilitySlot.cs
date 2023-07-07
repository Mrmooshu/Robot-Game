using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilitySlot : MonoBehaviour, IDropHandler
{
    public int abilitySlotIndex;
    public Sprite activeAbilitySprite;

    private void Start()
    {
        Refresh();
    }

    public void OnDrop(PointerEventData eventData)
    {
        // index must be within min and max of ability slot indexes
        if (eventData.pointerDrag != null && abilitySlotIndex >= 0 && abilitySlotIndex <= 5)
        {
            PlayerManager.instance.activeMinion.ActiveAbilities[abilitySlotIndex] = eventData.pointerDrag.transform.GetComponentInParent<ActiveAbilityIcon>().activeAbility;
            Refresh();
        }
    }

    private void Refresh()
    {
        if (Database.GetActiveAbility(PlayerManager.instance.activeMinion.ActiveAbilities[abilitySlotIndex]) != null)
        {
            activeAbilitySprite = Database.GetActiveAbility(PlayerManager.instance.activeMinion.ActiveAbilities[abilitySlotIndex]).Icon;
        }
    }
}
