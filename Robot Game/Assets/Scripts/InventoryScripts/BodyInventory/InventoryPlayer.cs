using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPlayer : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private CanvasGroup canvasGroup;
    public MinionData unit;
    public bool draggable;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            transform.position = eventData.position;
            canvasGroup.alpha = .5f;
            canvasGroup.blocksRaycasts = false;
            transform.parent.SetAsLastSibling();
            transform.parent.parent.parent.parent.SetAsLastSibling();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            transform.localPosition = Vector2.zero;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SafePointEntity.selectedMinion = unit;
    }
}
