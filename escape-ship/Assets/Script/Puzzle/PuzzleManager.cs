using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] GameObject puzzlePanel;  // 퍼즐 패널
    [SerializeField] int totalPieces;  // 맞춰야 할 총 퍼즐 조각 수
    private int matchedPieces = 0;  // 맞춰진 퍼즐 조각 수

    void Start()
    {
        puzzlePanel.SetActive(false);  // 처음엔 퍼즐 패널을 숨김
    }

    // 퍼즐 시작
    public void StartPuzzle()
    {
        puzzlePanel.SetActive(true);  // 퍼즐 패널 활성화
        Time.timeScale = 0;  // 퍼즐 중에 게임 시간 정지
        Cursor.visible = true;  // 커서 표시
        Cursor.lockState = CursorLockMode.None;
    }

    // 퍼즐 조각이 맞춰졌을 때 호출될 함수
    public void PieceMatched()
    {
        matchedPieces++;

        if (matchedPieces >= totalPieces)
        {
            CompletePuzzle();  // 퍼즐 완료
        }
    }

    // 퍼즐 완료 시 처리
    private void CompletePuzzle()
    {
        Debug.Log("퍼즐을 모두 맞췄습니다!");
        puzzlePanel.SetActive(false);  // 퍼즐 패널 비활성화
        Time.timeScale = 1;  // 게임 시간 재개
        Cursor.visible = false;  // 커서 숨김
        Cursor.lockState = CursorLockMode.Locked;

        // 퍼즐 완료 후 다른 동작 추가 가능 (예: 문을 열기)
    }
}
