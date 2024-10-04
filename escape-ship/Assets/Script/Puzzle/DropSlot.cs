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
            // 올바른 조각이 드롭된 경우
            droppedPiece.transform.position = transform.position;  // 슬롯에 조각 배치
            isCorrectPiecePlaced = true;  // 올바른 조각이 배치되었음을 기록
            CheckPuzzleCompletion();
        }
        else
        {
            // 잘못된 조각이 드롭된 경우 아무런 처리도 하지 않음
            Debug.Log("잘못된 조각입니다. 아무 동작도 하지 않습니다.");
        }
    }

    void CheckPuzzleCompletion()
    {
        // 모든 퍼즐이 완료되었는지 PuzzleTrigger에서 확인
        if (puzzleTrigger != null)
        {
            puzzleTrigger.CheckAllSlots();  // 퍼즐 완료 여부 확인
        }
        else
        {
            Debug.LogError("PuzzleTrigger가 할당되지 않았습니다!");
        }
    }

    public bool IsCorrectPiecePlaced()
    {
        return isCorrectPiecePlaced;  // 퍼즐 조각이 올바르게 배치되었는지 여부 반환
    }
}
