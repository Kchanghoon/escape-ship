using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject correctPiece;  // 이 슬롯에 들어가야 하는 올바른 조각
    [SerializeField] PuzzleTrigger puzzleTrigger;  // PuzzleTrigger 참조
    private bool isCorrectPiecePlaced = false;  // 올바른 조각이 배치되었는지 여부

    public void OnDrop(PointerEventData eventData)
    {
        DraggablePiece droppedPiece = eventData.pointerDrag.GetComponent<DraggablePiece>();

        if (droppedPiece != null && droppedPiece.gameObject == correctPiece)
        {
            droppedPiece.transform.position = transform.position;                              // 슬롯에 조각 배치
            isCorrectPiecePlaced = true;                                                         // 올바른 조각이 배치되었음을 기록
            CheckPuzzleCompletion();
        }
    }

    void CheckPuzzleCompletion()
    {
        // 모든 퍼즐이 완료되었는지 PuzzleTrigger에서 확인
        if (puzzleTrigger != null)
        {
            puzzleTrigger.CheckAllSlots();  // 퍼즐 완료 여부 확인
        }
    }

    public bool IsCorrectPiecePlaced()
    {
        return isCorrectPiecePlaced;  // 퍼즐 조각이 올바르게 배치되었는지 여부 반환
    }
}
