using UnityEngine;
using DG.Tweening;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private Transform lid;  // 상자의 뚜껑 (움직일 부분)
    [SerializeField] private Vector3 closedPosition = Vector3.zero;  // 닫혀있을 때의 로컬 위치값
    [SerializeField] private float openPositionX = 1f;  // 열렸을 때의 X축 위치값
    [SerializeField] private float duration = 1f;  // 열리는 속도
    [SerializeField] private Ease motionEase = Ease.InOutQuad;  // 애니메이션 Ease
    private bool isOpen = false;  // 상자가 열렸는지 여부를 기록
    private bool playerInRange = false;  // 플레이어가 가까이 있는지 확인하는 플래그
    private bool hasUsedBattery = false;  // 배터리를 사용하여 상자를 처음 열었는지 확인하는 플래그

    void Start()
    {
        lid.localPosition = closedPosition;  // 처음엔 닫힌 상태로 시작
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;
    }

    // KeyManager에서 Play 액션이 호출될 때 상자 열기/닫기 처리
    public void OnPlay()
    {
        if (playerInRange)
        {
            // 상자를 처음 열 때만 배터리를 소모
            if (!hasUsedBattery)
            {
                // 현재 선택한 아이템이 배터리(ID = 1)인지 확인
                var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
                if (selectedItem != null && selectedItem.id == "1")
                {
                    // 상자를 열거나 닫은 후 배터리 수량 감소
                    ToggleChest();
                    ItemController.Instance.DecreaseItemQuantity("1");  // 배터리 수량 감소
                    hasUsedBattery = true;  // 배터리를 사용했음을 기록
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

    // 상자를 열거나 닫는 함수
    private void ToggleChest()
    {
        if (isOpen)
        {
            // 상자가 열려있으면 닫기 (로컬 위치 사용)
            lid.DOLocalMoveX(closedPosition.x, duration).SetEase(motionEase);
        }
        else
        {
            // 상자가 닫혀있으면 열기 (X축으로 이동)
            lid.DOLocalMoveX(openPositionX, duration).SetEase(motionEase);
        }

        isOpen = !isOpen;  // 상태를 반전시킴
    }

    // 플레이어가 트리거 범위 안으로 들어왔을 때 호출되는 함수
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // 플레이어에게 'Player' 태그가 있는지 확인
        {
            playerInRange = true;  // 플레이어가 범위 안에 있음
        }
    }

    // 플레이어가 트리거 범위에서 벗어났을 때 호출되는 함수
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // 플레이어가 범위를 벗어남
        {
            playerInRange = false;  // 플레이어가 범위 밖으로 나감
        }
    }
}
