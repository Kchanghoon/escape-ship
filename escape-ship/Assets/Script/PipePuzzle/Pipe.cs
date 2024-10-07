using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Pipe : MonoBehaviour, IPointerClickHandler
{
    public PipeType pipeType;  // ������ ����
    public bool isChecked = false;  // �������� üũ�Ǿ����� ����
    private int currentRotation = 0;
    public RectTransform rectTransform;
    public Vector2Int[] connectionPoints;

    private Vector2Int[] defaultConnectionPoints;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentRotation = Mathf.RoundToInt(rectTransform.eulerAngles.z);

        SetDefaultConnectionPoints();
        UpdateConnectionPoints();
        Debug.Log("Pipe initialized");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RotatePipe();
    }

    void RotatePipe()
    {
        currentRotation = (currentRotation + 90) % 360;
        rectTransform.DORotate(new Vector3(0, 0, currentRotation), 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(UpdateConnectionPoints);
    }

    void SetDefaultConnectionPoints()
    {
        if (pipeType == PipeType.Start)
        {
            defaultConnectionPoints = new Vector2Int[1] { new Vector2Int(1, 0) };
        }
        else if (pipeType == PipeType.End)
        {
            defaultConnectionPoints = new Vector2Int[1] { new Vector2Int(-1, 0) };
        }
        else if (pipeType == PipeType.Straight)
        {
            defaultConnectionPoints = new Vector2Int[2] { new Vector2Int(0, 1), new Vector2Int(0, -1) };
        }
        else if (pipeType == PipeType.Corner)
        {
            defaultConnectionPoints = new Vector2Int[2] { new Vector2Int(1, 0), new Vector2Int(0, -1) };
        }
        else if (pipeType == PipeType.TShape)
        {
            defaultConnectionPoints = new Vector2Int[3] { new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1) };
        }
    }

    void UpdateConnectionPoints()
    {
        connectionPoints = new Vector2Int[defaultConnectionPoints.Length];

        for (int i = 0; i < connectionPoints.Length; i++)
        {
            connectionPoints[i] = RotateVector(defaultConnectionPoints[i], currentRotation);
        }
    }

    Vector2Int RotateVector(Vector2Int vector, int angle)
    {
        switch (angle)
        {
            case 90:
                return new Vector2Int(-vector.y, vector.x);
            case 180:
                return new Vector2Int(-vector.x, -vector.y);
            case 270:
                return new Vector2Int(vector.y, -vector.x);
            default:
                return vector;
        }
    }

    // �ٸ� ���������� ���� ���¸� Ȯ���ϴ� �Լ�
    // �� �������� ���� ����Ǿ����� Ȯ���ϴ� �Լ�
    public bool IsConnected(Pipe otherPipe)
    {
        foreach (var point in connectionPoints)
        {
            foreach (var otherPoint in otherPipe.connectionPoints)
            {
                // ���� ����Ʈ ����� ���
                Debug.Log($"���� ������ {gameObject.name} ���� ����Ʈ: {point}, �ٸ� ������ {otherPipe.gameObject.name} ���� ����Ʈ: {otherPoint}");

                // �������� ���� ����Ʈ�� �ݴ� �����̰ų� ���� ��� ����� ������ ����
                if (point == otherPoint)  // ���� ������ ��쵵 ����� ������ ����
                {
                    Debug.Log($"{gameObject.name}��(��) {otherPipe.gameObject.name}�� ����Ǿ����ϴ�.");
                    otherPipe.isChecked = true;  // ����� �������� üũ ���¸� ������Ʈ
                    return true;
                }
            }
        }

        Debug.Log($"{gameObject.name}��(��) {otherPipe.gameObject.name}�� ������� �ʾҽ��ϴ�.");
        return false;
    }

}
