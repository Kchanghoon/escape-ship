using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스 추가
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    public Transform doorLeft;
    public Transform doorRight;

    public float leftStartPosZ;
    public float rightStartPosZ;
    public float endPosZ = 3f;  // 문이 열릴 때 이동할 거리
    public float duration = 1f;  // 문이 열리는 데 걸리는 시간
    public Ease motionEase = Ease.OutQuad;

    public TextMeshProUGUI OpenText;  // "E키를 눌러 문을 여시오" TMP 텍스트를 위한 변수
    public TextMeshProUGUI CloseText;  // "E키를 눌러 문을 여시오" TMP 텍스트를 위한 변수
    private bool isDoorOpen = false;  // 문이 열려 있는지 여부를 저장하는 변수
    private bool isAnimating = false;  // 애니메이션이 실행 중인지 확인하는 플래그
    private bool playerInRange = false;  // 플레이어가 범위 안에 있는지 확인하는 플래그

    void Start()
    {
        // 시작 위치 저장
        leftStartPosZ = doorLeft.localPosition.z;
        rightStartPosZ = doorRight.localPosition.z;

        // 시작 시 텍스트 숨기기
        OpenText.gameObject.SetActive(false);  // TMP 텍스트 숨김
        CloseText.gameObject.SetActive(false);
        // KeyManager에서 Play 액션을 등록
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;
    }

    // KeyManager에서 Play 액션이 호출될 때 상자 열기/닫기 처리
    public void OnPlay()
    {
        if (playerInRange && !isAnimating)
        {
            // 노랑 카드키(ID = 2)를 들고 있어야만 문을 열 수 있음
            var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
            if (selectedItem != null && selectedItem.id == "2")
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
            else
            {
                Debug.Log("노랑 카드키를 들고 있어야 문을 열 수 있습니다.");
            }
        }
    }

    // 플레이어가 트리거 범위 안에 들어왔을 때
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // 플레이어에게 'Player' 태그가 붙어 있다고 가정
        {
            playerInRange = true;  // 플레이어가 범위 안에 있음을 기록
            if (isDoorOpen == false)
            {
                OpenText.gameObject.SetActive(true);  // TMP 텍스트 표시
                CloseText.gameObject .SetActive(false);
            }
            else
            {
                CloseText.gameObject.SetActive(true);
                OpenText.gameObject.SetActive(false);
            }
        }
    }

    // 플레이어가 트리거 범위를 벗어났을 때
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;  // 플레이어가 범위를 벗어났음을 기록
            OpenText.gameObject.SetActive(false);  // TMP 텍스트 숨김
            CloseText.gameObject.SetActive(false);
        }
    }

    // 문을 여는 함수
    void OpenDoor()
    {
        isAnimating = true;  // 애니메이션이 실행 중임을 기록
        DOTween.Sequence()
            .Append(doorLeft.DOLocalMoveZ(leftStartPosZ + endPosZ, duration))
            .Join(doorRight.DOLocalMoveZ(rightStartPosZ - endPosZ, duration))
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // 애니메이션이 끝났음을 기록
                isDoorOpen = true;  // 문이 열린 상태로 기록
            });
    }

    // 문을 닫는 함수
    void CloseDoor()
    {
        isAnimating = true;  // 애니메이션이 실행 중임을 기록
        DOTween.Sequence()
            .Append(doorLeft.DOLocalMoveZ(leftStartPosZ, duration))
            .Join(doorRight.DOLocalMoveZ(rightStartPosZ, duration))
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // 애니메이션이 끝났음을 기록
                isDoorOpen = false;  // 문이 닫힌 상태로 기록
            });
    }
}
