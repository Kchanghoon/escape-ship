using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CheckChestOpen : MonoBehaviour
{
    [SerializeField] private Transform lid;  // 상자의 뚜껑 (상자 부분)
    [SerializeField] private Vector3 closedPosition = Vector3.zero;  // 상자가 닫혀 있을 때의 위치
    [SerializeField] private float openPositionX = 1f;  // 상자가 열렸을 때 X축으로 이동할 위치
    [SerializeField] private float duration = 1f;  // 상자가 열리거나 닫히는 속도
    [SerializeField] private Ease motionEase = Ease.InOutQuad;  // 애니메이션의 Ease 설정
    [SerializeField] private float autoCloseDelay = 5f;  // 상자가 자동으로 닫히기 전 대기 시간

    [SerializeField] private TextMeshProUGUI statusText;  // 상자 상태를 표시하는 TextMeshPro 텍스트
    [SerializeField] private float interactionDistance = 3f;  // 플레이어가 상자와 상호작용할 수 있는 거리

    private bool isOpen = false;  // 상자가 열렸는지 여부를 나타내는 변수
    private bool isMouseOverChest = false;  // 마우스가 상자 위에 있는지 여부
    private bool hasUsedBattery = false;  // 배터리를 사용했는지 여부
    private Transform playerTransform;  // 플레이어의 Transform
    private Coroutine autoCloseCoroutine;  // 자동으로 상자를 닫는 코루틴

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // 플레이어의 Transform을 가져옴
        lid.localPosition = closedPosition;  // 시작 시 상자를 닫힌 상태로 설정
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;  // 키 입력 이벤트 연결

        if (statusText != null)
        {
            statusText.gameObject.SetActive(false);  // 상태 텍스트 비활성화
        }
    }

    private void Update()
    {
        // 플레이어와 상자의 거리를 계산
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        // 플레이어가 상자와 상호작용 가능한 거리 내에 있고 마우스가 상자 위에 있으면 상태 텍스트 표시
        if (distanceToPlayer <= interactionDistance && isMouseOverChest)
        {
            statusText.gameObject.SetActive(true);  // 상태 텍스트 활성화
            UpdateStatusText();  // 상태 텍스트 업데이트
        }
        else
        {
            statusText.gameObject.SetActive(false);  // 상태 텍스트 비활성화
        }
    }

    // 마우스가 상자 위에 올라갈 때 호출되는 메서드
    private void OnMouseEnter()
    {
        isMouseOverChest = true;  // 마우스가 상자 위에 있음
    }

    // 마우스가 상자에서 벗어날 때 호출되는 메서드
    private void OnMouseExit()
    {
        isMouseOverChest = false;  // 마우스가 상자에서 벗어남
        statusText.gameObject.SetActive(false);  // 상태 텍스트 비활성화
    }

    // 키 입력이 발생했을 때 호출되는 메서드, 상자를 열거나 닫음
   public void OnPlay()
{
    // 플레이어가 상자와 상호작용할 수 있는 거리에 있고 마우스가 상자 위에 있을 때만 작동
    float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
    if (distanceToPlayer <= interactionDistance && isMouseOverChest)
    {
        // 아이템 ID가 "5", "6", "7" 중 하나인 경우 상자를 열거나 닫을 수 있음
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
        if (selectedItem != null && (selectedItem.id == "7" || selectedItem.id == "6" || selectedItem.id == "5"))
        {
            UpdateStatusText();  // 상태 텍스트 업데이트
            ToggleChest();  // 상자 열기 또는 닫기
        }
        else
        {
            // 해당하는 아이템이 없을 경우
            Debug.Log("아이템이 필요합니다.");
            statusText.text = "아이템이 필요합니다. (ID = 5, 6, 7 중 하나)";
        }
    }
}


    // 상태 텍스트를 업데이트하는 메서드
    private void UpdateStatusText()
    {
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();

        if (selectedItem != null) // 선택된 아이템이 null이 아닌 경우
        {
            if (selectedItem.id == "7")
            {
                statusText.text = "상자를 열어주세요.";
            }
            else
            {
                statusText.text = "3번 아이템 키가 필요합니다.";
            }
        }
        else
        {
            // 선택된 아이템이 없을 경우
            statusText.text = "3번 아이템 키가 필요합니다.";
        }
    }

    // 상자를 열거나 닫는 메서드
    private void ToggleChest()
    {
        if (isOpen)
        {
            // 상자가 열려 있으면 닫기 (닫힌 위치로 이동)
            lid.DOLocalMoveX(closedPosition.x, duration).SetEase(motionEase);
            statusText.gameObject.SetActive(true);  // 상자가 닫혔음을 알리는 상태 텍스트 활성화
            if (autoCloseCoroutine != null)
            {
                StopCoroutine(autoCloseCoroutine);  // 자동 닫기 코루틴 종료
            }
        }
        else
        {
            // 상자가 닫혀 있으면 열기 (X축으로 이동)
            lid.DOLocalMoveX(openPositionX, duration).SetEase(motionEase);
            statusText.gameObject.SetActive(false);  // 상자가 열렸으므로 상태 텍스트 비활성화

            // 일정 시간이 지나면 자동으로 상자를 닫기 위한 코루틴 시작
            if (autoCloseCoroutine != null)
            {
                StopCoroutine(autoCloseCoroutine);  // 기존 코루틴 종료
            }
            autoCloseCoroutine = StartCoroutine(AutoCloseChest());  // 새로운 자동 닫기 코루틴 시작
        }

        isOpen = !isOpen;  // 상자 상태를 토글
    }

    // 일정 시간이 지나면 자동으로 상자를 닫는 코루틴
    private IEnumerator AutoCloseChest()
    {
        yield return new WaitForSeconds(autoCloseDelay);  // 설정된 대기 시간 동안 대기
        if (isOpen)  // 상자가 여전히 열려 있는 경우
        {
            ToggleChest();  // 상자 닫기
        }
    }
}
