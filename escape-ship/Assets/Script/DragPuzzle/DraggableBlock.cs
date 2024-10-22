using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DraggableBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isHorizontal;
    public bool isVertical;
    private RectTransform rectTransform;
    private Vector2 startPosition;
    private Vector2 dragOffset;

    public DragBlockManager blockManager;  // DragBlockManager ����
    public DraggableTrigger blockTrigger;  // DraggableTrigger ����

    public float gridSize = 50f;
    public float snapDuration = 0.3f;
    private Vector2 snappedPosition;  // snappedPosition ������ Ŭ���� �ʵ�� �߰�

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out dragOffset);
        startPosition = rectTransform.anchoredPosition;
    }

    // �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            blockManager.gameArea, eventData.position, eventData.pressEventCamera, out localPoint);

        Vector2 targetPosition = startPosition;

        if (isHorizontal)
        {
            targetPosition = new Vector2(localPoint.x - dragOffset.x, startPosition.y);
        }
        else if (isVertical)
        {
            targetPosition = new Vector2(startPosition.x, localPoint.y - dragOffset.y);
        }

        // DragBlockManager���� endPosition�� �����Ͽ� �浹 ����
        if (blockManager.IsWithinGameArea(targetPosition, rectTransform) &&
            !blockManager.IsCollision(targetPosition, rectTransform, blockTrigger.endPosition))  // blockTrigger���� endPosition�� ����
        {
            rectTransform.anchoredPosition = targetPosition;
        }
    }

    // �巡�� ����
    public void OnEndDrag(PointerEventData eventData)
    {
        SnapToGrid();

        // SnapToGrid �ִϸ��̼��� �Ϸ�� �� ���� Ŭ���� ���θ� Ȯ��
        rectTransform.DOAnchorPos(snappedPosition, snapDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                Debug.Log("�ִϸ��̼� �Ϸ�, ���� üũ ����");
                blockTrigger.CheckPuzzleCompletion();
            });
    }

    // �׸��忡 ������Ű�� �Լ�
    private void SnapToGrid()
    {
        snappedPosition = new Vector2(
            Mathf.Round(rectTransform.anchoredPosition.x / gridSize) * gridSize,
            Mathf.Round(rectTransform.anchoredPosition.y / gridSize) * gridSize
        );

        rectTransform.DOAnchorPos(snappedPosition, snapDuration).SetEase(Ease.OutQuad);
    }
}
