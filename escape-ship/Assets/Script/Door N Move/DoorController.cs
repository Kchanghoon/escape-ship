using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스 추가
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    public Transform doorLeft;
    public Transform doorRight;
    public Transform player;  // 플레이어의 Transform을 추가하여 거리 계산

    public float leftStartPosZ;
    public float rightStartPosZ;
    public float endPosZ = 3f;  // 문이 열릴 때 이동할 거리
    public float duration = 1f;  // 문이 열리는 데 걸리는 시간
    public float interactionDistance = 5f;  // 플레이어와 문 사이의 상호작용 거리
    public Ease motionEase = Ease.OutQuad;

    public TextMeshProUGUI statusText;  // 상태를 표시할 TMP 텍스트
    private bool isDoorOpen = false;  // 문이 열려 있는지 여부를 저장하는 변수
    private bool isAnimating = false;  // 애니메이션이 실행 중인지 확인하는 플래그
    private bool isMouseOverDoor = false;  // 마우스가 문 위에 있는지 여부

    void Start()
    {
        // 시작 위치 저장
        leftStartPosZ = doorLeft.localPosition.z;
        rightStartPosZ = doorRight.localPosition.z;

        // 시작 시 텍스트 숨기기
        statusText.gameObject.SetActive(false);  // TMP 텍스트 숨김

        // KeyManager에서 Play 액션을 등록
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;
    }

    void Update()
    {
        // 플레이어와 문의 거리가 상호작용 거리 내에 있는지 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 플레이어가 상호작용 거리 안에 있고, 마우스가 문 위에 있을 때만 상태 문구를 표시
        if (distanceToPlayer <= interactionDistance && isMouseOverDoor)
        {
            UpdateStatusText();
        }
        else
        {
            statusText.gameObject.SetActive(false);  // 문구 숨기기
        }
    }

    // KeyManager에서 Play 액션이 호출될 때 상자 열기/닫기 처리
    public void OnPlay()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= interactionDistance && isMouseOverDoor && !isAnimating)
        {
            // 노랑 카드키(ID = 2)를 들고 있어야만 문을 열 수 있음
            var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
            if (selectedItem != null && (selectedItem.id == "5" || selectedItem.id == "6" || selectedItem.id == "7" || selectedItem.id == "8"))
            {
                if (isDoorOpen)
                {
                    CloseDoor();  // 문이 열려 있으면 닫음
                }
                else
                {
                    OpenDoor();  // 문이 닫혀 있으면 염
                }
                statusText.text = "E키를 눌러 문을 여닫아주세요.";  // 문 상태에 따른 텍스트 갱신
            }
            else
            {
                statusText.text = "보안 카드가 필요합니다.";
            }
        }
    }

    // 상태 텍스트를 업데이트하는 함수
    private void UpdateStatusText()
    {
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
        if (selectedItem == null || (selectedItem.id != "5" && selectedItem.id != "6" && selectedItem.id != "7" && selectedItem.id != "8"))
        {
            statusText.text = "최소 3급 보안 카드가 필요합니다.";  // 보안 카드가 없을 때
        }
        else
        {
            statusText.text = "E키를 눌러 문을 여닫아주세요.";  // 보안 카드가 있을 때
        }
        statusText.gameObject.SetActive(true);  // 텍스트 표시
    }

    // 마우스가 문 위에 있을 때 호출
    private void OnMouseEnter()
    {
        isMouseOverDoor = true;
    }

    // 마우스가 문에서 벗어났을 때 호출
    private void OnMouseExit()
    {
        isMouseOverDoor = false;
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
