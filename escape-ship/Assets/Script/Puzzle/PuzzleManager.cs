using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] GameObject puzzlePanel;  // ���� �г�
    [SerializeField] int totalPieces;  // ����� �� �� ���� ���� ��
    private int matchedPieces = 0;  // ������ ���� ���� ��

    void Start()
    {
        puzzlePanel.SetActive(false);  // ó���� ���� �г��� ����
    }

    // ���� ����
    public void StartPuzzle()
    {
        puzzlePanel.SetActive(true);  // ���� �г� Ȱ��ȭ
        Time.timeScale = 0;  // ���� �߿� ���� �ð� ����
        Cursor.visible = true;  // Ŀ�� ǥ��
        Cursor.lockState = CursorLockMode.None;
    }

    // ���� ������ �������� �� ȣ��� �Լ�
    public void PieceMatched()
    {
        matchedPieces++;

        if (matchedPieces >= totalPieces)
        {
            CompletePuzzle();  // ���� �Ϸ�
        }
    }

    // ���� �Ϸ� �� ó��
    private void CompletePuzzle()
    {
        Debug.Log("������ ��� ������ϴ�!");
        puzzlePanel.SetActive(false);  // ���� �г� ��Ȱ��ȭ
        Time.timeScale = 1;  // ���� �ð� �簳
        Cursor.visible = false;  // Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked;

        // ���� �Ϸ� �� �ٸ� ���� �߰� ���� (��: ���� ����)
    }
}
