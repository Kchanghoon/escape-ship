using UnityEngine;

public class ItemBasedTogglePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;  // 패널 오브젝트
    [SerializeField] private string requiredItemId = "specialItem";  // 이 오브젝트에 필요한 아이템 ID
    [SerializeField] private Canvas ToggleCanvas;  // 패널의 Canvas
    private bool isPanelActive = false;  // 패널이 열려 있는 상태인지 기록
    private int originalSortingOrder;  // Canvas의 원래 sortingOrder 저장
    [SerializeField] int sortingCanvas;

    private void Start()
    {
        // Canvas의 원래 sortingOrder 저장
        originalSortingOrder = ToggleCanvas.sortingOrder;

        // 키 이벤트에 패널 토글 연결
        KeyManager.Instance.keyDic[KeyAction.Panel] += TryTogglePanel;

        // 시작 시 패널을 비활성화
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

    // 선택된 아이템이 해당 오브젝트의 패널을 열 수 있는지 확인하는 함수
    private void TryTogglePanel()
    {
        // 인벤토리에서 선택된 아이템 가져오기
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();

        // 선택된 아이템이 존재하고, 해당 오브젝트가 요구하는 아이템인지 확인
        if (selectedItem != null && selectedItem.id == requiredItemId)
        {
            TogglePanel();  // 패널 토글
        }
        else
        {
            Debug.Log($"필요한 아이템이 선택되지 않았습니다. 필요한 아이템 ID: {requiredItemId}");
        }
    }

    // 패널을 토글하는 함수
    private void TogglePanel()
    {
        if (panel != null)
        {
            isPanelActive = !isPanelActive;  // 패널 상태를 반전

            if (isPanelActive)
            {
                ToggleCanvas.sortingOrder = sortingCanvas;  // 최상위 우선순위로 설정
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                ToggleCanvas.sortingOrder = originalSortingOrder;  // 원래 우선순위로 복원
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            panel.SetActive(isPanelActive);  // 패널을 활성화/비활성화
            Debug.Log($"패널 상태가 변경되었습니다. 현재 상태: {(isPanelActive ? "활성화" : "비활성화")}");
        }
        else
        {
            Debug.LogWarning("패널이 할당되지 않았습니다.");
        }
    }
}
