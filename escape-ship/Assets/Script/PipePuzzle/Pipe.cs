using UnityEngine;
using UnityEngine.EventSystems;

public class Pipe : MonoBehaviour, IPointerClickHandler
{
    private int currentRotation = 0;  // 현재 회전 각도 (0, 90, 180, 270도)
    public RectTransform rectTransform;  // UI 이미지의 RectTransform 참조
    public Vector2Int[] connectionPoints;  // 파이프의 연결 포인트 (입구와 출구 방향)

    // 네 방향 (상, 하, 좌, 우)을 나타내는 벡터 (0: 위, 1: 오른쪽, 2: 아래, 3: 왼쪽)
    private Vector2Int[] defaultConnectionPoints = new Vector2Int[2] { new Vector2Int(0, 1), new Vector2Int(0, -1) };  // 기본 방향 (위-아래)

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();  // RectTransform을 가져옵니다
        UpdateConnectionPoints();  // 처음 시작할 때 연결 포인트를 설정합니다.
    }

    // IPointerClickHandler 인터페이스의 메서드 구현
    public void OnPointerClick(PointerEventData eventData)
    {
        RotatePipe();  // 클릭 시 파이프 회전
    }

    void RotatePipe()
    {
        // 90도씩 회전
        currentRotation = (currentRotation + 90) % 360;
        rectTransform.localRotation = Quaternion.Euler(0, 0, currentRotation);  // Z축을 기준으로 회전

        // 회전 후 연결 상태 업데이트
        UpdateConnectionPoints();

        Debug.Log("Pipe rotated to " + currentRotation + " degrees.");
    }

    // 파이프 회전 후 입구와 출구 포인트를 업데이트하는 함수
    void UpdateConnectionPoints()
    {
        connectionPoints = new Vector2Int[defaultConnectionPoints.Length];

        for (int i = 0; i < defaultConnectionPoints.Length; i++)
        {
            // 회전된 각도에 따라 연결 포인트를 업데이트
            connectionPoints[i] = RotateVector(defaultConnectionPoints[i], currentRotation);
        }
    }

    // 특정 벡터를 주어진 각도만큼 회전시키는 함수
    Vector2Int RotateVector(Vector2Int vector, int angle)
    {
        // 각도에 따라 벡터를 회전시킴 (90도, 180도, 270도 회전만 지원)
        switch (angle)
        {
            case 90:
                return new Vector2Int(vector.y, -vector.x);
            case 180:
                return new Vector2Int(-vector.x, -vector.y);
            case 270:
                return new Vector2Int(-vector.y, vector.x);
            default:
                return vector;  // 0도 회전 (기본 상태)
        }
    }

    // 다른 파이프와의 연결 상태를 확인하는 함수
    public bool IsConnected(Pipe otherPipe)
    {
        foreach (var point in connectionPoints)
        {
            foreach (var otherPoint in otherPipe.connectionPoints)
            {
                // 파이프의 연결 포인트가 반대일 경우 연결됨 (서로 방향이 반대인 경우)
                if (point == -otherPoint)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
