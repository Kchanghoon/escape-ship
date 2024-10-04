using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject correctPiece; // 이 슬롯에 들어가야 하는 올바른 조각
    [SerializeField] GameObject puzzlePanel;  // 퍼즐 패널
    [SerializeField] PuzzleTrigger puzzleTrigger; // PuzzleTrigger 참조

    public void OnDrop(PointerEventData eventData)
    {
        DraggablePiece droppedPiece = eventData.pointerDrag.GetComponent<DraggablePiece>();

        if (droppedPiece != null && droppedPiece.gameObject == correctPiece)
        {
            // 올바른 조각이 드롭된 경우
            droppedPiece.transform.position = transform.position; // 슬롯에 조각 배치
            CheckPuzzleCompletion();
        }
        else
        {
            // 조각이 올바르지 않으면 다시 원래 자리로 이동 (선택적)
            droppedPiece.transform.position = transform.position;
        }
    }

    void CheckPuzzleCompletion()
    {
        // 퍼즐이 완성되었는지 확인하는 로직
        Debug.Log("올바른 조각이 드롭되었습니다!");

        // 퍼즐 완료 시 PuzzleTrigger의 CompletePuzzle 호출
        if (puzzleTrigger != null)
        {
            puzzleTrigger.CompletePuzzle();
        }
        else
        {
            Debug.LogError("PuzzleTrigger가 할당되지 않았습니다!");
        }
    }
}
