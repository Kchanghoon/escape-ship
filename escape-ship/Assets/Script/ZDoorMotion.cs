using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스 추가
using DG.Tweening;

public class ZDoorMotion : MonoBehaviour
{
    public Transform door;  // 상하로 움직이는 문
    public float startPosY;  // 문 시작 위치 Y
    public float endPosY = 3f;  // 문이 열릴 때 이동할 거리
    public float duration = 1f;  // 문이 열리는 데 걸리는 시간
    public Ease motionEase = Ease.OutQuad;  // 애니메이션 속도 조절

    public TextMeshProUGUI actionText;  // TMP 텍스트 표시
    private bool isDoorOpen = false;  // 문이 열려 있는지 여부를 저장하는 변수
    private bool isAnimating = false;  // 애니메이션이 실행 중인지 확인하는 플래그
    private bool playerInRange = false;  // 플레이어가 범위 안에 있는지 확인하는 플래그

    void Start()
    {
        // 시작 위치 저장
        startPosY = door.localPosition.y;

        // 시작 시 텍스트 숨기기
        actionText.gameObject.SetActive(false);
    }

    void Update()
    {
        // E키를 눌렀을 때 애니메이션 실행 (플레이어가 범위 안에 있고 애니메이션이 실행 중이지 않은 경우에만)
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isAnimating)
        {
            if (isDoorOpen)
            {
                CloseDoor();  // 문이 열려 있으면 닫음
            }
            else
            {
                OpenDoor();  // 문이 닫혀 있으면 염
            }
        }
    }

    // 플레이어가 트리거 범위 안에 들어왔을 때
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // 플레이어에게 'Player' 태그가 붙어 있다고 가정
        {
            playerInRange = true;  // 플레이어가 범위 안에 있음을 기록
            UpdateActionText();
            actionText.gameObject.SetActive(true);  // TMP 텍스트 표시
        }
    }

    // 플레이어가 트리거 범위를 벗어났을 때
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;  // 플레이어가 범위를 벗어났음을 기록
            actionText.gameObject.SetActive(false);  // TMP 텍스트 숨김
        }
    }

    // 문을 여는 함수
    void OpenDoor()
    {
        isAnimating = true;  // 애니메이션이 실행 중임을 기록
        door.DOLocalMoveY(startPosY + endPosY, duration)  // 문을 위로 이동
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // 애니메이션이 끝났음을 기록
                isDoorOpen = true;  // 문이 열린 상태로 기록
                UpdateActionText();
            });
    }

    // 문을 닫는 함수
    void CloseDoor()
    {
        isAnimating = true;  // 애니메이션이 실행 중임을 기록
        door.DOLocalMoveY(startPosY, duration)  // 문을 원래 위치로 이동
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // 애니메이션이 끝났음을 기록
                isDoorOpen = false;  // 문이 닫힌 상태로 기록
                UpdateActionText();
            });
    }

    // 텍스트 업데이트
    void UpdateActionText()
    {
        if (isDoorOpen)
        {
            actionText.text = "E키를 눌러 문을 닫으시오";
        }
        else
        {
            actionText.text = "E키를 눌러 문을 여시오";
        }
    }
}
