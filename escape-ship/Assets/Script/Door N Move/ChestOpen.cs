using TMPro;
using UnityEngine;
using DG.Tweening;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private Transform lid;  // 상자의 뚜껑 (움직일 부분)
    [SerializeField] private Vector3 closedPosition = Vector3.zero;  // 닫혀있을 때의 로컬 위치값
    [SerializeField] private float openPositionX = 1f;  // 열렸을 때의 X축 위치값
    [SerializeField] private float duration = 1f;  // 열리는 속도
    [SerializeField] private Ease motionEase = Ease.InOutQuad;  // 애니메이션 Ease

    [SerializeField] private TextMeshProUGUI statusText;  // 상태 메시지를 출력할 TextMeshPro 변수
    [SerializeField] private float interactionDistance = 3f;  // 상호작용 가능 거리

    private bool isOpen = false;  // 상자가 열렸는지 여부를 기록
    private bool isMouseOverChest = false;  // 마우스가 상자 위에 있는지 여부
    private bool hasUsedBattery = false;  // 배터리를 사용하여 상자를 처음 열었는지 확인하는 플래그
    private Transform playerTransform;  // 플레이어의 Transform을 저장

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // 플레이어의 Transform 가져오기
        lid.localPosition = closedPosition;  // 처음엔 닫힌 상태로 시작
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;

        if (statusText != null)
        {
            statusText.gameObject.SetActive(false);  // 처음에는 상태 메시지 비활성화
        }
    }

    private void Update()
    {
        // 플레이어와 상자 사이의 거리 계산
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        // 플레이어가 상호작용 가능한 거리 안에 있을 때만 마우스를 올리면 상호작용 가능
        if (distanceToPlayer <= interactionDistance && isMouseOverChest)
        {
            statusText.gameObject.SetActive(true);  // 상태 메시지 활성화
            UpdateStatusText();  // 상태 텍스트 표시
        }
        else
        {
            statusText.gameObject.SetActive(false);  // 상태 메시지 비활성화
        }
    }

    // 마우스가 상자 위에 있을 때 호출되는 함수
    private void OnMouseEnter()
    {
        isMouseOverChest = true;  // 마우스가 상자 위에 있음
    }

    // 마우스가 상자에서 벗어났을 때 호출되는 함수
    private void OnMouseExit()
    {
        isMouseOverChest = false;  // 마우스가 상자에서 벗어남
        statusText.gameObject.SetActive(false);  // 상태 메시지 비활성화
    }

    // KeyManager에서 Play 액션이 호출될 때 상자 열기/닫기 처리
    public void OnPlay()
    {
        // 플레이어가 상호작용 가능한 거리 내에 있고, 마우스가 상자 위에 있을 때 상자를 열 수 있음
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        if (distanceToPlayer <= interactionDistance && isMouseOverChest)
        {
            // 상자를 처음 열 때만 배터리를 소모
            if (!hasUsedBattery)
            {
                // 현재 선택한 아이템이 배터리(ID = 1)인지 확인
                var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
                if (selectedItem != null && selectedItem.id == "1")
                {
                    // 상자를 열거나 닫은 후 배터리 수량 감소
                    ItemController.Instance.DecreaseItemQuantity("1");  // 배터리 수량 감소
                    hasUsedBattery = true;  // 배터리를 사용했음을 기록
                    UpdateStatusText();

                    ToggleChest();
                }
                else
                {
                    Debug.Log("배터리가 선택되지 않았습니다.");
                }
            }
            else
            {
                // 이미 배터리를 사용했으므로 배터리 없이 상자를 열거나 닫음
                ToggleChest();
            }
        }
    }

    // 상태 메시지를 업데이트하는 함수
    private void UpdateStatusText()
    {
        if (hasUsedBattery)
        {
        }
        else
        {
            statusText.text = "배터리가 필요합니다. E키를 눌러 배터리를 넣어주세요.";
        }
    }

    // 상자를 열거나 닫는 함수
    private void ToggleChest()
    {
        if (isOpen)
        {
            // 상자가 열려있으면 닫기 (로컬 위치 사용)
            lid.DOLocalMoveX(closedPosition.x, duration).SetEase(motionEase);
            statusText.gameObject.SetActive(true);  // 상자가 닫히면 텍스트 활성화
        }
        else
        {
            // 상자가 닫혀있으면 열기 (X축으로 이동)
            lid.DOLocalMoveX(openPositionX, duration).SetEase(motionEase);
            statusText.gameObject.SetActive(false);  // 상자가 열리면 텍스트 비활성화
        }

        isOpen = !isOpen;  // 상태를 반전시킴
    }

}
