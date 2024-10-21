using DG.Tweening;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public Transform laserOrigin;  // 레이저가 발사되는 위치
    public LineRenderer laserLineRenderer;  // 레이저를 시각적으로 표현할 LineRenderer
    public float maxLaserDistance = 100f;  // 레이저의 최대 거리
    public LayerMask reflectableLayers;  // 레이저가 반사될 수 있는 레이어 (거울이나 목표 등)
    public Transform doorLeft;  // 왼쪽 문 Transform
    public Transform doorRight;  // 오른쪽 문 Transform

    public float leftStartPosZ;  // 왼쪽 문의 시작 Z 좌표
    public float rightStartPosZ;  // 오른쪽 문의 시작 Z 좌표
    public float endPosZ = 3f;  // 문이 열렸을 때의 이동 거리
    public float duration = 1f;  // 문이 열리고 닫히는 애니메이션 지속 시간
    public Ease motionEase = Ease.OutQuad;  // DoTween의 애니메이션 이징 설정
    private bool isDoorOpen = false;  // 문이 열려 있는지 여부를 저장
    private bool isAnimating = false;  // 현재 애니메이션이 진행 중인지 여부를 저장
    private bool targetHit = false; // 레이저가 목표에 닿았는지 여부

    private void Start()
    {
        // laserOrigin이 null인 경우, 기본적으로 이 오브젝트의 Transform을 사용
        if (laserOrigin == null)
        {
            laserOrigin = transform;
        }
        // 시작 시 문들의 Z 좌표를 저장
        leftStartPosZ = doorLeft.localPosition.z;
        rightStartPosZ = doorRight.localPosition.z;
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
        bool hitTarget = false;  // 목표에 닿았는지 확인하는 플래그

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
                    hitTarget = true;  // 목표에 닿았다는 것을 기록
                    if (!isDoorOpen)
                    {
                        CompletePuzzle();  // 퍼즐 완료 처리 및 문 열기
                    }
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

        // 목표에 닿지 않았을 경우 문을 닫음
        if (!hitTarget && isDoorOpen)
        {
            CloseDoor();
        }
    }

    void CompletePuzzle()
    {
        isAnimating = true;  // 애니메이션이 진행 중임을 표시
        DOTween.Sequence()
            .Append(doorLeft.DOLocalMoveZ(leftStartPosZ + endPosZ, duration))  // 왼쪽 문을 열기
            .Join(doorRight.DOLocalMoveZ(rightStartPosZ - endPosZ, duration))  // 오른쪽 문을 열기
            .SetEase(motionEase)  // 이징 설정
            .OnComplete(() =>
            {
                isAnimating = false;  // 애니메이션 완료
                isDoorOpen = true;  // 문이 열렸음
            });
    }

    // 문을 닫는 메서드
    void CloseDoor()
    {
        isAnimating = true;  // 애니메이션이 진행 중임을 표시
        DOTween.Sequence()
            .Append(doorLeft.DOLocalMoveZ(leftStartPosZ, duration))  // 왼쪽 문을 닫기
            .Join(doorRight.DOLocalMoveZ(rightStartPosZ, duration))  // 오른쪽 문을 닫기
            .SetEase(motionEase)  // 이징 설정
            .OnComplete(() =>
            {
                isAnimating = false;  // 애니메이션 완료
                isDoorOpen = false;  // 문이 닫혔음
            });
    }
}
