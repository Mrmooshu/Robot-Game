using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPlayer : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    public PlayerBody body;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        canvasGroup.alpha = .5f;
        canvasGroup.blocksRaycasts = false;
        transform.parent.SetAsLastSibling();
        transform.parent.parent.parent.parent.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector2.zero;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
