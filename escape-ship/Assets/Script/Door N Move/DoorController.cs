using UnityEngine;
using TMPro;  // TextMeshPro 라이브러리 추가
using DG.Tweening;  // DoTween 라이브러리 추가

public class DoorController : MonoBehaviour
{
    public Transform doorLeft;  // 왼쪽 문 Transform
    public Transform doorRight;  // 오른쪽 문 Transform
    public Transform player;  // 플레이어의 Transform, 거리 계산용

    public float leftStartPosZ;  // 왼쪽 문의 시작 Z 좌표
    public float rightStartPosZ;  // 오른쪽 문의 시작 Z 좌표
    public float endPosZ = 3f;  // 문이 열렸을 때의 이동 거리
    public float duration = 1f;  // 문이 열리고 닫히는 애니메이션 지속 시간
    public float interactionDistance = 5f;  // 플레이어가 문과 상호작용할 수 있는 거리
    public Ease motionEase = Ease.OutQuad;  // DoTween의 애니메이션 이징 설정

    public TextMeshProUGUI statusText;  // 문 상태를 표시할 TextMeshPro 텍스트
    private bool isDoorOpen = false;  // 문이 열려 있는지 여부를 저장
    private bool isAnimating = false;  // 현재 애니메이션이 진행 중인지 여부를 저장
    private bool isMouseOverDoor = false;  // 마우스가 문 위에 있는지 여부를 저장

    void Start()
    {
        // 시작 시 문들의 Z 좌표를 저장
        leftStartPosZ = doorLeft.localPosition.z;
        rightStartPosZ = doorRight.localPosition.z;

        // 상태 텍스트 비활성화
        statusText.gameObject.SetActive(false);  

        // KeyManager에서 Play 키에 대한 이벤트 추가
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;
    }

    void Update()
    {
        // 플레이어와 문 사이의 거리를 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 플레이어가 문과 상호작용할 수 있는 거리 안에 있고, 마우스가 문 위에 있을 때 텍스트 표시
        if (distanceToPlayer <= interactionDistance && isMouseOverDoor)
        {
            UpdateStatusText();  // 상태 텍스트 업데이트
        }
        else
        {
            statusText.gameObject.SetActive(false);  // 텍스트 숨기기
        }
    }

    // Play 키를 눌렀을 때 문을 열거나 닫는 동작을 처리하는 메서드
    public void OnPlay()
    {
        // 플레이어와 문의 거리를 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 상호작용할 수 있는 거리에 있고, 문에 마우스가 올려져 있으며 애니메이션이 진행 중이 아닐 때만 동작
        if (distanceToPlayer <= interactionDistance && isMouseOverDoor && !isAnimating)
        {
            // 인벤토리에서 선택된 아이템을 확인 (ID 2의 아이템 필요)
            var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
            if (selectedItem != null && (selectedItem.id == "5" || selectedItem.id == "6" || selectedItem.id == "7" || selectedItem.id == "8"))
            {
                if (isDoorOpen)
                {
                    CloseDoor();  // 문이 열려 있으면 닫기
                }
                else
                {
                    OpenDoor();  // 문이 닫혀 있으면 열기
                }
                statusText.text = "E키를 눌러 문을 닫아주세요.";  // 상태 텍스트 업데이트
            }
            else
            {
                statusText.text = "열쇠가 필요합니다.";  // 열쇠가 없을 때의 텍스트
            }
        }
    }

    // 문 상태에 따라 텍스트를 업데이트하는 메서드
    private void UpdateStatusText()
    {
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
        if (selectedItem == null || (selectedItem.id != "5" && selectedItem.id != "6" && selectedItem.id != "7" && selectedItem.id != "8"))
        {
            statusText.text = "특정 열쇠가 필요합니다.";  // 열쇠가 없을 경우
        }
        else
        {
            statusText.text = "E키를 눌러 문을 여세요.";  // 열쇠가 있을 경우
        }
        statusText.gameObject.SetActive(true);  // 상태 텍스트 표시
    }

    // 마우스가 문에 들어왔을 때 호출되는 메서드
    private void OnMouseEnter()
    {
        isMouseOverDoor = true;  // 마우스가 문 위에 있음
    }

    // 마우스가 문에서 나갔을 때 호출되는 메서드
    private void OnMouseExit()
    {
        isMouseOverDoor = false;  // 마우스가 문 위에 없음
    }

    // 문을 여는 메서드
    void OpenDoor()
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
