using UnityEngine;

public class DragBlockManager : MonoBehaviour
{
    public RectTransform gameArea;
    public float gridSize = 50f;  // 그리드 크기

    // 게임 영역 내에 있는지 여부 확인
    public bool IsWithinGameArea(Vector2 targetPosition, RectTransform currentBlock)
    {
        Rect gameAreaRect = gameArea.rect;
        Vector2 blockSize = currentBlock.rect.size;

        Vector2 minPosition = targetPosition - (blockSize * currentBlock.pivot);
        Vector2 maxPosition = minPosition + blockSize;

        return minPosition.x >= gameAreaRect.xMin && maxPosition.x <= gameAreaRect.xMax &&
               minPosition.y >= gameAreaRect.yMin && maxPosition.y <= gameAreaRect.yMax;
    }

    // 충돌 감지 함수 (EndPosition과의 충돌 무시)
    public bool IsCollision(Vector2 targetPosition, RectTransform currentBlock, RectTransform endPosition)
    {
        // 현재 블록의 목표 위치를 기준으로 사각형 계산
        Rect currentBlockRect = new Rect(targetPosition, currentBlock.rect.size);
        Debug.Log($"현재 블록 Rect: {currentBlockRect}");

        // 충돌 감지
        foreach (var otherBlock in GetComponentsInChildren<RectTransform>())
        {
            if (otherBlock == currentBlock || otherBlock == gameArea || otherBlock == endPosition)
                continue;  // 자기 자신, 게임 영역, 그리고 EndPosition은 무시

            // 다른 블록의 RectTransform 정보 가져오기
            Rect otherBlockRect = new Rect(otherBlock.anchoredPosition, otherBlock.rect.size);

            // 두 사각형이 겹치는지 확인
            if (currentBlockRect.Overlaps(otherBlockRect))
            {
                Debug.Log($"충돌한 오브젝트: {otherBlock.name}");
                return true;
            }
        }

        return false;
    }

    // 모든 블록을 그리드에 맞게 스냅시키는 함수
    public void SnapAllBlocksToGrid()
    {
        foreach (var block in GetComponentsInChildren<RectTransform>())
        {
            if (block == gameArea) continue;  // 게임 영역은 제외

            SnapToGrid(block);  // 각 블록을 그리드에 맞게 정렬
        }
    }

    // 블록을 그리드에 맞게 정렬하는 함수
    private void SnapToGrid(RectTransform block)
    {
        Vector2 anchoredPosition = block.anchoredPosition;

        // 그리드에 맞게 위치를 계산
        Vector2 snappedPosition = new Vector2(
            Mathf.Round(anchoredPosition.x / gridSize) * gridSize,
            Mathf.Round(anchoredPosition.y / gridSize) * gridSize
        );

        block.anchoredPosition = snappedPosition;  // 블록의 위치를 그리드에 맞게 조정
    }
}
