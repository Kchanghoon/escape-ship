using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // 드래그 시 투명도 변경
        canvasGroup.blocksRaycasts = false; // 드래그 중에는 다른 UI 상호작용 가능하게 함
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta; // 드래그 중 위치 이동
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // 원래 상태로 복구
        canvasGroup.blocksRaycasts = true; // 드래그 후 다시 상호작용 가능
    }

    internal Vector3 GetOriginalPosition()
    {
        throw new NotImplementedException();
    }
}
