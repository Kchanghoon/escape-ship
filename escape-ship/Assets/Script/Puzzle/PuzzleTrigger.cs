using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public PuzzleManager puzzleManager;  // 퍼즐 매니저 참조

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // 플레이어가 상호작용 오브젝트에 접근하면
        {
            puzzleManager.StartPuzzle();  // 퍼즐 시작
        }
    }
}
