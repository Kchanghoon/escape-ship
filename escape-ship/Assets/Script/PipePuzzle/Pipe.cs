using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Pipe : MonoBehaviour, IPointerClickHandler
{
    public PipeType pipeType;  // 파이프 종류
    public bool isChecked = false;  // 파이프가 체크되었는지 여부
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

    // 다른 파이프와의 연결 상태를 확인하는 함수
    // 두 파이프가 서로 연결되었는지 확인하는 함수
    public bool IsConnected(Pipe otherPipe)
    {
        foreach (var point in connectionPoints)
        {
            foreach (var otherPoint in otherPipe.connectionPoints)
            {
                // 연결 포인트 디버그 출력
                Debug.Log($"현재 파이프 {gameObject.name} 연결 포인트: {point}, 다른 파이프 {otherPipe.gameObject.name} 연결 포인트: {otherPoint}");

                // 파이프의 연결 포인트가 반대 방향이거나 같은 경우 연결된 것으로 간주
                if (point == otherPoint)  // 같은 방향일 경우도 연결된 것으로 간주
                {
                    Debug.Log($"{gameObject.name}이(가) {otherPipe.gameObject.name}과 연결되었습니다.");
                    otherPipe.isChecked = true;  // 연결된 파이프의 체크 상태를 업데이트
                    return true;
                }
            }
        }

        Debug.Log($"{gameObject.name}이(가) {otherPipe.gameObject.name}과 연결되지 않았습니다.");
        return false;
    }

}
