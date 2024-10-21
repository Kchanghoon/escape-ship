using UnityEngine;
using DG.Tweening;  // DoTween 라이브러리 추가

public class AutoDoorController : MonoBehaviour
{
    public Transform doorLeft;  // 왼쪽 문 Transform
    public Transform doorRight;  // 오른쪽 문 Transform
    public Transform player;  // 플레이어의 Transform, 거리 계산용

    public float leftStartPosX;  // 왼쪽 문의 시작 Z 좌표
    public float rightStartPosX;  // 오른쪽 문의 시작 Z 좌표
    public float endPosX = 3f;  // 문이 열렸을 때의 이동 거리
    public float duration = 1f;  // 문이 열리고 닫히는 애니메이션 지속 시간
    public float interactionDistance = 5f;  // 플레이어가 문과 상호작용할 수 있는 거리
    public Ease motionEase = Ease.OutQuad;  // DoTween의 애니메이션 이징 설정

    private bool isDoorOpen = false;  // 문이 열려 있는지 여부를 저장
    private bool isAnimating = false;  // 현재 애니메이션이 진행 중인지 여부를 저장

    void Start()
    {
        // 시작 시 문들의 Z 좌표를 저장
        leftStartPosX = doorLeft.localPosition.x;
        rightStartPosX = doorRight.localPosition.x;
    }

    void Update()
    {
        // 플레이어와 문 사이의 거리를 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 플레이어가 상호작용할 수 있는 거리 안에 있을 때 자동으로 문을 열고, 범위를 벗어나면 닫음
        if (distanceToPlayer <= interactionDistance && !isDoorOpen && !isAnimating)
        {
            OpenDoor();  // 문 열기
        }
        else if (distanceToPlayer > interactionDistance && isDoorOpen && !isAnimating)
        {
            CloseDoor();  // 문 닫기
        }
    }

    // 문을 여는 메서드
    void OpenDoor()
    {
        isAnimating = true;  // 애니메이션이 진행 중임을 표시
        DOTween.Sequence()
            .Append(doorLeft.DOLocalMoveX(leftStartPosX + endPosX, duration))  // 왼쪽 문을 열기
            .Join(doorRight.DOLocalMoveX(rightStartPosX - endPosX, duration))  // 오른쪽 문을 열기
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
            .Append(doorLeft.DOLocalMoveX(leftStartPosX, duration))  // 왼쪽 문을 닫기
            .Join(doorRight.DOLocalMoveX(rightStartPosX, duration))  // 오른쪽 문을 닫기
            .SetEase(motionEase)  // 이징 설정
            .OnComplete(() =>
            {
                isAnimating = false;  // 애니메이션 완료
                isDoorOpen = false;  // 문이 닫혔음
            });
    }
}
