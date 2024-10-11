using UnityEngine;

public class ObjectPipe : MonoBehaviour
{
    [SerializeField] private GameObject panel;  // 연결된 패널
    [SerializeField] private Transform player;  // 플레이어의 Transform
    [SerializeField] private float interactionDistance = 3f;  // 플레이어와 오브젝트 간의 상호작용 거리
    [SerializeField] private Canvas panelCanvas;  // 패널의 Canvas (우선순위 변경을 위해 필요)
    private int originalSortingOrder;  // 원래 Canvas의 sortingOrder
    private bool isPanelActive = false;  // 패널이 현재 활성화되어 있는지 여부
    private bool isMouseOverObject = false;  // 마우스가 오브젝트 위에 있는지 여부

    void Start()
    {
        // 시작 시 패널을 비활성화
        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("패널이 할당되지 않았습니다.");
        }

        if (panelCanvas != null)
        {
            originalSortingOrder = panelCanvas.sortingOrder;  // Canvas의 원래 우선순위 저장
        }
        else
        {
            Debug.LogWarning("panelCanvas가 할당되지 않았습니다.");
        }

        KeyManager.Instance.keyDic[KeyAction.Play] += TryTogglePanel;
    }

    // 마우스가 오브젝트 위에 있을 때 호출되는 함수
    private void OnMouseEnter()
    {
        isMouseOverObject = true;
    }

    // 마우스가 오브젝트에서 벗어났을 때 호출되는 함수
    private void OnMouseExit()
    {
        isMouseOverObject = false;
    }

    // 패널을 토글할 수 있는지 확인하고 패널을 토글
    private void TryTogglePanel()
    {
        if (IsPlayerInRange() && isMouseOverObject)
        {
            TogglePanel();
        }
        else
        {
            Debug.Log("플레이어가 너무 멀거나 마우스가 오브젝트 위에 있지 않습니다.");
        }
    }

    // 패널을 토글하는 함수
    private void TogglePanel()
    {
        if (panel != null)
        {
            isPanelActive = !isPanelActive;  // 패널의 활성화 상태를 반전
            panel.SetActive(isPanelActive);  // 패널을 활성화 또는 비활성화

            if (isPanelActive)
            {
                // 패널이 활성화될 때 Canvas의 우선순위를 높임
                if (panelCanvas != null)
                {
                    panelCanvas.sortingOrder = 999;  // 우선순위를 최상위로 설정
                }
            }
            else
            {
                // 패널이 비활성화될 때 원래 우선순위로 복원
                if (panelCanvas != null)
                {
                    panelCanvas.sortingOrder = originalSortingOrder;  // 원래 순서로 복원
                }
            }

            Debug.Log($"패널 상태가 변경되었습니다: {(isPanelActive ? "활성화됨" : "비활성화됨")}");
        }
        else
        {
            Debug.LogWarning("패널이 연결되어 있지 않습니다.");
        }
    }

    // 플레이어가 일정 거리 내에 있는지 확인하는 함수
    private bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        return distanceToPlayer <= interactionDistance;
    }
}
