using UnityEngine;

public class ItemBasedTogglePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;  // 토글할 패널 오브젝트
    [SerializeField] private string requiredItemId = "specialItem";  // 패널을 열기 위해 필요한 아이템의 ID
    [SerializeField] private Canvas ToggleCanvas;  // 패널이 속한 Canvas
    private bool isPanelActive = false;  // 패널이 현재 활성화되어 있는지 여부를 저장하는 변수
    private int originalSortingOrder;  // Canvas의 원래 sortingOrder 값을 저장
    [SerializeField] int sortingCanvas;  // 패널이 활성화될 때 사용할 sortingOrder 값

    private void Start()
    {
        // Canvas의 원래 sortingOrder 값을 저장
        originalSortingOrder = ToggleCanvas.sortingOrder;

        // 키 이벤트에 패널 토글 메서드를 연결
        KeyManager.Instance.keyDic[KeyAction.Panel] += TryTogglePanel;

        // 시작할 때 패널을 비활성화
        if (panel != null)
        {
            panel.SetActive(false);
            Debug.Log("패널 초기 상태: 비활성화");
        }
        else
        {
            Debug.LogWarning("패널이 할당되지 않았습니다.");
        }
    }

    // 선택된 아이템이 패널을 열 수 있는지 확인하는 메서드
    private void TryTogglePanel()
    {
        // 인벤토리에서 선택된 아이템 가져오기
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();

        // 선택된 아이템이 있으며, 그 아이템이 패널을 열 수 있는 아이템인지 확인
        if (selectedItem != null && selectedItem.id == requiredItemId)
        {
            TogglePanel();  // 패널을 토글
        }
        else
        {
            Debug.Log($"필요한 아이템이 선택되지 않았습니다. 필요한 아이템 ID: {requiredItemId}");
        }
    }

    // 패널을 활성화 또는 비활성화하는 메서드
    private void TogglePanel()
    {
        if (panel != null)
        {
            isPanelActive = !isPanelActive;  // 패널 상태를 반전

            if (isPanelActive)
            {
                ToggleCanvas.sortingOrder = sortingCanvas;  // 활성화 시 캔버스의 정렬 순서 변경
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;  // 마우스 커서 보이게 하고 잠금 해제
            }
            else
            {
                ToggleCanvas.sortingOrder = originalSortingOrder;  // 비활성화 시 원래 정렬 순서 복구
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;  // 마우스 커서 숨기고 잠금
            }

            panel.SetActive(isPanelActive);  // 패널을 활성화 또는 비활성화
            Debug.Log($"패널 상태가 변경되었습니다. 현재 상태: {(isPanelActive ? "활성화" : "비활성화")}");
        }
        else
        {
            Debug.LogWarning("패널이 할당되지 않았습니다.");
        }
    }
}
