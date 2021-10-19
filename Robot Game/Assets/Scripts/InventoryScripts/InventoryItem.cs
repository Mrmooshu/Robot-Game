using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Camera cam;
    private CanvasGroup canvasGroup;
    public Item item;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f;
        transform.position = cam.ScreenToWorldPoint(screenPoint);
        canvasGroup.alpha = .5f;
        canvasGroup.blocksRaycasts = false;
        transform.parent.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector2.zero;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
