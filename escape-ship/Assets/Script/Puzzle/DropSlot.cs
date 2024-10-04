using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject correctPiece; // �� ���Կ� ���� �ϴ� �ùٸ� ����
    [SerializeField] GameObject puzzlePanel;  // ���� �г�
    [SerializeField] PuzzleTrigger puzzleTrigger; // PuzzleTrigger ����

    public void OnDrop(PointerEventData eventData)
    {
        DraggablePiece droppedPiece = eventData.pointerDrag.GetComponent<DraggablePiece>();

        if (droppedPiece != null && droppedPiece.gameObject == correctPiece)
        {
            // �ùٸ� ������ ��ӵ� ���
            droppedPiece.transform.position = transform.position; // ���Կ� ���� ��ġ
            CheckPuzzleCompletion();
        }
        else
        {
            // ������ �ùٸ��� ������ �ٽ� ���� �ڸ��� �̵� (������)
            droppedPiece.transform.position = transform.position;
        }
    }

    void CheckPuzzleCompletion()
    {
        // ������ �ϼ��Ǿ����� Ȯ���ϴ� ����
        Debug.Log("�ùٸ� ������ ��ӵǾ����ϴ�!");

        // ���� �Ϸ� �� PuzzleTrigger�� CompletePuzzle ȣ��
        if (puzzleTrigger != null)
        {
            puzzleTrigger.CompletePuzzle();
        }
        else
        {
            Debug.LogError("PuzzleTrigger�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }
}
