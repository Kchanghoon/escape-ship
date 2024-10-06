using UnityEngine;
using UnityEngine.EventSystems;

public class Pipe : MonoBehaviour, IPointerClickHandler
{
    private int currentRotation = 0;  // ���� ȸ�� ���� (0, 90, 180, 270��)
    public RectTransform rectTransform;  // UI �̹����� RectTransform ����
    public Vector2Int[] connectionPoints;  // �������� ���� ����Ʈ (�Ա��� �ⱸ ����)

    // �� ���� (��, ��, ��, ��)�� ��Ÿ���� ���� (0: ��, 1: ������, 2: �Ʒ�, 3: ����)
    private Vector2Int[] defaultConnectionPoints = new Vector2Int[2] { new Vector2Int(0, 1), new Vector2Int(0, -1) };  // �⺻ ���� (��-�Ʒ�)

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();  // RectTransform�� �����ɴϴ�
        UpdateConnectionPoints();  // ó�� ������ �� ���� ����Ʈ�� �����մϴ�.
    }

    // IPointerClickHandler �������̽��� �޼��� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        RotatePipe();  // Ŭ�� �� ������ ȸ��
    }

    void RotatePipe()
    {
        // 90���� ȸ��
        currentRotation = (currentRotation + 90) % 360;
        rectTransform.localRotation = Quaternion.Euler(0, 0, currentRotation);  // Z���� �������� ȸ��

        // ȸ�� �� ���� ���� ������Ʈ
        UpdateConnectionPoints();

        Debug.Log("Pipe rotated to " + currentRotation + " degrees.");
    }

    // ������ ȸ�� �� �Ա��� �ⱸ ����Ʈ�� ������Ʈ�ϴ� �Լ�
    void UpdateConnectionPoints()
    {
        connectionPoints = new Vector2Int[defaultConnectionPoints.Length];

        for (int i = 0; i < defaultConnectionPoints.Length; i++)
        {
            // ȸ���� ������ ���� ���� ����Ʈ�� ������Ʈ
            connectionPoints[i] = RotateVector(defaultConnectionPoints[i], currentRotation);
        }
    }

    // Ư�� ���͸� �־��� ������ŭ ȸ����Ű�� �Լ�
    Vector2Int RotateVector(Vector2Int vector, int angle)
    {
        // ������ ���� ���͸� ȸ����Ŵ (90��, 180��, 270�� ȸ���� ����)
        switch (angle)
        {
            case 90:
                return new Vector2Int(vector.y, -vector.x);
            case 180:
                return new Vector2Int(-vector.x, -vector.y);
            case 270:
                return new Vector2Int(-vector.y, vector.x);
            default:
                return vector;  // 0�� ȸ�� (�⺻ ����)
        }
    }

    // �ٸ� ���������� ���� ���¸� Ȯ���ϴ� �Լ�
    public bool IsConnected(Pipe otherPipe)
    {
        foreach (var point in connectionPoints)
        {
            foreach (var otherPoint in otherPipe.connectionPoints)
            {
                // �������� ���� ����Ʈ�� �ݴ��� ��� ����� (���� ������ �ݴ��� ���)
                if (point == -otherPoint)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
