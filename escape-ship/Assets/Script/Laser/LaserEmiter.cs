using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public Transform laserOrigin;  // 레이저가 발사되는 위치
    public LineRenderer laserLineRenderer;  // 레이저를 시각적으로 표현할 LineRenderer
    public float maxLaserDistance = 100f;  // 레이저의 최대 거리
    public LayerMask reflectableLayers;  // 레이저가 반사될 수 있는 레이어 (거울이나 목표 등)

    private void Start()
    {
        // laserOrigin이 null인 경우, 기본적으로 이 오브젝트의 Transform을 사용
        if (laserOrigin == null)
        {
            laserOrigin = transform;
        }
    }

    private void Update()
    {
        FireLaser();
    }

    void FireLaser()
    {
        Vector3 laserDirection = laserOrigin.forward;  // 레이저 발사 방향
        Vector3 laserPosition = laserOrigin.position;  // 레이저 시작 위치
        laserLineRenderer.positionCount = 1;  // LineRenderer의 시작점을 레이저 시작 위치로 설정
        laserLineRenderer.SetPosition(0, laserPosition);

        bool isReflecting = true;
        int reflections = 0;
        float remainingDistance = maxLaserDistance;

        while (isReflecting && reflections < 10)  // 최대 10번까지 반사
        {
            // LayerMask 적용한 Raycast
            if (Physics.Raycast(laserPosition, laserDirection, out RaycastHit hit, remainingDistance, reflectableLayers))
            {
                laserLineRenderer.positionCount += 1;
                laserLineRenderer.SetPosition(laserLineRenderer.positionCount - 1, hit.point);

                // 레이저가 거울에 맞았을 경우 반사
                if (hit.collider.CompareTag("Mirror"))
                {
                    Vector3 incomingDirection = laserDirection;  // 레이저의 입사각
                    Vector3 normal = hit.normal;  // 거울 표면의 법선 벡터
                    laserDirection = Vector3.Reflect(incomingDirection, normal);  // 반사각 계산

                    laserPosition = hit.point;
                    reflections++;
                    remainingDistance -= hit.distance;
                }

                else if (hit.collider.CompareTag("Target"))  // 목표 지점에 맞았을 경우
                {
                    Debug.Log("레이저가 목표에 도달했습니다!");
                    CompletePuzzle();  // 퍼즐 완료 처리
                    isReflecting = false;
                }
                else
                {
                    isReflecting = false;
                }
            }
            else
            {
                isReflecting = false;
                laserLineRenderer.positionCount += 1;
                laserLineRenderer.SetPosition(laserLineRenderer.positionCount - 1, laserPosition + laserDirection * remainingDistance);
            }
        }
    }

    void CompletePuzzle()
    {
        // 퍼즐 완료 처리
        Debug.Log("퍼즐이 완료되었습니다!");
    }
}
