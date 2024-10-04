using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject correctPiece;  // �� ���Կ� ���� �ϴ� �ùٸ� ����
    [SerializeField] PuzzleTrigger puzzleTrigger;  // PuzzleTrigger ����
    private bool isCorrectPiecePlaced = false;  // �ùٸ� ������ ��ġ�Ǿ����� ����

    public void OnDrop(PointerEventData eventData)
    {
        DraggablePiece droppedPiece = eventData.pointerDrag.GetComponent<DraggablePiece>();

        if (droppedPiece != null && droppedPiece.gameObject == correctPiece)
        {
            // �ùٸ� ������ ��ӵ� ���
            droppedPiece.transform.position = transform.position;  // ���Կ� ���� ��ġ
            isCorrectPiecePlaced = true;  // �ùٸ� ������ ��ġ�Ǿ����� ���
            CheckPuzzleCompletion();
        }
        else
        {
            // �߸��� ������ ��ӵ� ��� �ƹ��� ó���� ���� ����
            Debug.Log("�߸��� �����Դϴ�. �ƹ� ���۵� ���� �ʽ��ϴ�.");
        }
    }

    void CheckPuzzleCompletion()
    {
        // ��� ������ �Ϸ�Ǿ����� PuzzleTrigger���� Ȯ��
        if (puzzleTrigger != null)
        {
            puzzleTrigger.CheckAllSlots();  // ���� �Ϸ� ���� Ȯ��
        }
        else
        {
            Debug.LogError("PuzzleTrigger�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }

    public bool IsCorrectPiecePlaced()
    {
        return isCorrectPiecePlaced;  // ���� ������ �ùٸ��� ��ġ�Ǿ����� ���� ��ȯ
    }
}
