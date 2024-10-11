using UnityEngine;

public class ItemBasedTogglePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;  // 패널 오브젝트
    [SerializeField] private string requiredItemId = "specialItem";  // 패널 토글을 위해 필요한 아이템 ID
    [SerializeField] private ItemController itemController;  // 아이템 관리 컨트롤러
    [SerializeField] private Canvas ToggleCanvas;  // 패널의 Canvas
    private bool isPanelActive = false;  // 패널이 열려 있는 상태인지 기록
    private int originalSortingOrder;  // Canvas의 원래 sortingOrder 저장
    [SerializeField] int sortingCanvas;

    private void Start()
    {
        // Canvas의 원래 sortingOrder 저장
        originalSortingOrder = ToggleCanvas.sortingOrder;

        KeyManager.Instance.keyDic[KeyAction.Panel] += TogglePanel;

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

        if (itemController == null)
        {
            Debug.LogWarning("ItemController가 할당되지 않았습니다.");
        }
    }

    // 패널을 토글하는 함수
    private void TogglePanel()
    {
        // 아이템이 있는지 확인
        if (HasRequiredItem())
        {
            if (panel != null)
            {
                isPanelActive = !isPanelActive;  // 패널 상태를 반전

                // 패널이 활성화되면 Canvas의 우선순위를 높임
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
        else
        {
            Debug.Log("필요한 아이템이 없어 패널을 열 수 없습니다.");
        }
    }

    // 플레이어 인벤토리에 해당 아이템이 있는지 확인하는 함수
    private bool HasRequiredItem()
    {
        if (itemController != null)
        {
            foreach (var item in itemController.curItemDatas)
            {
                Debug.Log($"인벤토리 아이템 확인 중: {item.id}");
                if (item.id == requiredItemId)
                {
                    Debug.Log("필요한 아이템이 인벤토리에 있습니다.");
                    return true;
                }
            }
        }
        else
        {
            Debug.LogWarning("ItemController가 할당되지 않았습니다.");
        }
        Debug.Log("필요한 아이템이 인벤토리에 없습니다.");
        return false;
    }
}
