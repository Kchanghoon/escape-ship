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

    public DragBlockManager blockManager;  // DragBlockManager 참조
    public DraggableTrigger blockTrigger;  // DraggableTrigger 참조

    public float gridSize = 50f;
    public float snapDuration = 0.3f;
    private Vector2 snappedPosition;  // snappedPosition 변수를 클래스 필드로 추가

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out dragOffset);
        startPosition = rectTransform.anchoredPosition;
    }

    // 드래그 중
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

        // DragBlockManager에서 endPosition을 전달하여 충돌 감지
        if (blockManager.IsWithinGameArea(targetPosition, rectTransform) &&
            !blockManager.IsCollision(targetPosition, rectTransform, blockTrigger.endPosition))  // blockTrigger에서 endPosition을 전달
        {
            rectTransform.anchoredPosition = targetPosition;
        }
    }

    // 드래그 종료
    public void OnEndDrag(PointerEventData eventData)
    {
        SnapToGrid();

        // SnapToGrid 애니메이션이 완료된 후 퍼즐 클리어 여부를 확인
        rectTransform.DOAnchorPos(snappedPosition, snapDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                Debug.Log("애니메이션 완료, 퍼즐 체크 시작");
                blockTrigger.CheckPuzzleCompletion();
            });
    }

    // 그리드에 스냅시키는 함수
    private void SnapToGrid()
    {
        snappedPosition = new Vector2(
            Mathf.Round(rectTransform.anchoredPosition.x / gridSize) * gridSize,
            Mathf.Round(rectTransform.anchoredPosition.y / gridSize) * gridSize
        );

        rectTransform.DOAnchorPos(snappedPosition, snapDuration).SetEase(Ease.OutQuad);
    }
}
