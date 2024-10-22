using UnityEngine;

public class DragBlockManager : MonoBehaviour
{
    public RectTransform gameArea;
    public float gridSize = 50f;  // �׸��� ũ��

    // ���� ���� ���� �ִ��� ���� Ȯ��
    public bool IsWithinGameArea(Vector2 targetPosition, RectTransform currentBlock)
    {
        Rect gameAreaRect = gameArea.rect;
        Vector2 blockSize = currentBlock.rect.size;

        Vector2 minPosition = targetPosition - (blockSize * currentBlock.pivot);
        Vector2 maxPosition = minPosition + blockSize;

        return minPosition.x >= gameAreaRect.xMin && maxPosition.x <= gameAreaRect.xMax &&
               minPosition.y >= gameAreaRect.yMin && maxPosition.y <= gameAreaRect.yMax;
    }

    // �浹 ���� �Լ� (EndPosition���� �浹 ����)
    public bool IsCollision(Vector2 targetPosition, RectTransform currentBlock, RectTransform endPosition)
    {
        // ���� ����� ��ǥ ��ġ�� �������� �簢�� ���
        Rect currentBlockRect = new Rect(targetPosition, currentBlock.rect.size);
        Debug.Log($"���� ��� Rect: {currentBlockRect}");

        // �浹 ����
        foreach (var otherBlock in GetComponentsInChildren<RectTransform>())
        {
            if (otherBlock == currentBlock || otherBlock == gameArea || otherBlock == endPosition)
                continue;  // �ڱ� �ڽ�, ���� ����, �׸��� EndPosition�� ����

            // �ٸ� ����� RectTransform ���� ��������
            Rect otherBlockRect = new Rect(otherBlock.anchoredPosition, otherBlock.rect.size);

            // �� �簢���� ��ġ���� Ȯ��
            if (currentBlockRect.Overlaps(otherBlockRect))
            {
                Debug.Log($"�浹�� ������Ʈ: {otherBlock.name}");
                return true;
            }
        }

        return false;
    }

    // ��� ����� �׸��忡 �°� ������Ű�� �Լ�
    public void SnapAllBlocksToGrid()
    {
        foreach (var block in GetComponentsInChildren<RectTransform>())
        {
            if (block == gameArea) continue;  // ���� ������ ����

            SnapToGrid(block);  // �� ����� �׸��忡 �°� ����
        }
    }

    // ����� �׸��忡 �°� �����ϴ� �Լ�
    private void SnapToGrid(RectTransform block)
    {
        Vector2 anchoredPosition = block.anchoredPosition;

        // �׸��忡 �°� ��ġ�� ���
        Vector2 snappedPosition = new Vector2(
            Mathf.Round(anchoredPosition.x / gridSize) * gridSize,
            Mathf.Round(anchoredPosition.y / gridSize) * gridSize
        );

        block.anchoredPosition = snappedPosition;  // ����� ��ġ�� �׸��忡 �°� ����
    }
}
