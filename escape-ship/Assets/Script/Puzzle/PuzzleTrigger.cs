using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public PuzzleManager puzzleManager;  // ���� �Ŵ��� ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // �÷��̾ ��ȣ�ۿ� ������Ʈ�� �����ϸ�
        {
            puzzleManager.StartPuzzle();  // ���� ����
        }
    }
}
