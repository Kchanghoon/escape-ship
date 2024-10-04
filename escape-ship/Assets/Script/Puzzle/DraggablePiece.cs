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
        canvasGroup.alpha = 0.6f; // �巡�� �� ���� ����
        canvasGroup.blocksRaycasts = false; // �巡�� �߿��� �ٸ� UI ��ȣ�ۿ� �����ϰ� ��
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta; // �巡�� �� ��ġ �̵�
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // ���� ���·� ����
        canvasGroup.blocksRaycasts = true; // �巡�� �� �ٽ� ��ȣ�ۿ� ����
    }

    internal Vector3 GetOriginalPosition()
    {
        throw new NotImplementedException();
    }
}
