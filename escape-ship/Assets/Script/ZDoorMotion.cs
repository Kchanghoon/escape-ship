using UnityEngine;
using DG.Tweening;

public class ZDoorMotion : MonoBehaviour
{
    public Transform door;  // 상하로 움직이는 문
    public float startPosY;  // 문 시작 위치 Y
    public float endPosY = 3f;  // 문이 열릴 때 이동할 거리
    public float duration = 1f;  // 문이 열리는 데 걸리는 시간
    public Ease motionEase = Ease.OutQuad;  // 애니메이션 속도 조절

    private bool isDoorOpen = false;  // 문이 열려 있는지 여부를 저장하는 변수
    private bool isAnimating = false;  // 애니메이션이 실행 중인지 확인하는 플래그

    void Start()
    {
        // 시작 위치 저장
        startPosY = door.localPosition.y;
    }

    // 문을 여는 함수
    public void OpenDoor()
    {
        if (isAnimating || isDoorOpen) return;  // 이미 애니메이션 중이거나 문이 열려 있으면 아무 작업도 하지 않음
        isAnimating = true;  // 애니메이션이 실행 중임을 기록
        door.DOLocalMoveY(startPosY + endPosY, duration)  // 문을 위로 이동
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // 애니메이션이 끝났음을 기록
                isDoorOpen = true;  // 문이 열린 상태로 기록
            });
    }

    // 문을 닫는 함수
    public void CloseDoor()
    {
        if (isAnimating || !isDoorOpen) return;  // 이미 애니메이션 중이거나 문이 닫혀 있으면 아무 작업도 하지 않음
        isAnimating = true;  // 애니메이션이 실행 중임을 기록
        door.DOLocalMoveY(startPosY, duration)  // 문을 원래 위치로 이동
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // 애니메이션이 끝났음을 기록
                isDoorOpen = false;  // 문이 닫힌 상태로 기록
            });
    }
}
