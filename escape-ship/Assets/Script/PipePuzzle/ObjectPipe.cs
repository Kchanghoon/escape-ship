using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ObjectPipe : MonoBehaviour
{
    [SerializeField] private GameObject panel;  // 연결된 패널
    [SerializeField] private Transform player;  // 플레이어의 Transform
    [SerializeField] private float interactionDistance = 3f;  // 플레이어와 오브젝트 간의 상호작용 거리
    [SerializeField] private Canvas panelCanvas;  // 패널의 Canvas (우선순위 변경을 위해 필요)
    //[SerializeField] private bool puzzleclear = false;  // 퍼즐 클리어 여부를 저장하는 변수
    //public GameObject recoveryZone; // 퍼즐 완료 시 활성화할 회복존
    private int originalSortingOrder;  // 원래 Canvas의 sortingOrder
    private bool isPanelActive = false;  // 패널이 현재 활성화되어 있는지 여부
    private bool isMouseOverObject = false;  // 마우스가 오브젝트 위에 있는지 여부

    public TextMeshProUGUI statusText;  // 상태를 표시할 TMP 텍스트
     
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
        // 시작 시 텍스트 숨기기
        statusText.gameObject.SetActive(false);  // TMP 텍스트 숨김

        KeyManager.Instance.keyDic[KeyAction.Play] += TryTogglePanel;
    }

    // 마우스가 오브젝트 위에 있을 때 호출되는 함수
    private void OnMouseEnter()
    {
        isMouseOverObject = true;
        if (IsPlayerInRange() && isMouseOverObject)
        {
            Debug.Log("온마우스 엔터 안까지 작동 완료");
            statusText.gameObject.SetActive(true);
            statusText.text = "벨브가 필요합니다.";
        }
    }

    // 마우스가 오브젝트에서 벗어났을 때 호출되는 함수
    private void OnMouseExit()
    {
        isMouseOverObject = false;
        statusText.gameObject.SetActive(false);
    }

    // 패널을 토글할 수 있는지 확인하고 패널을 토글
    private void TryTogglePanel()
    {
        if (IsPlayerInRange() && isMouseOverObject)
        {
            var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
            if (selectedItem != null && selectedItem.id == "3")
            {
                if (PipeManager.Instance.puzzleclear == false)
                {
                    TogglePanel();
                }
                else
                {
                    // 퍼즐이 이미 클리어된 경우 바로 회복존 활성화
                    PipeManager.Instance.OnPuzzleComplete();
                }
            }
            else
            {
                statusText.gameObject.SetActive(true);
                statusText.text = "벨브를 선택 후 눌러주세요.";
            }
        }
        else
        {
            Debug.Log("플레이어가 너무 멀거나 마우스가 오브젝트 위에 있지 않습니다.");
            statusText.gameObject.SetActive(false);
        }
    }



    // 패널을 토글하는 함수
    private void TogglePanel()
    {
        if (panel != null)
        {
            isPanelActive = !isPanelActive;  // 패널의 활성화 상태를 반전
            panel.SetActive(isPanelActive);  // 패널을 활성화 또는 비활성화
            PipeManager.Instance.RandomPipe();

            if (isPanelActive)
            {
                // 패널이 활성화될 때 Canvas의 우선순위를 높임
                if (panelCanvas != null)
                {
                    //Time.timeScale = 0;  // 시간 재개 (게임 일시정지 해제)
                    panelCanvas.sortingOrder = 999;  // 우선순위를 최상위로 설정
                    MouseCam mouseCam = FindObjectOfType<MouseCam>();
                    if (mouseCam != null)
                    {
                        mouseCam.SetCursorState(false);  // 커서 잠금 해제
                    }
                }
            }
            else
            {
                // 패널이 비활성화될 때 원래 우선순위로 복원
                if (panelCanvas != null)
                {

                    //Time.timeScale = 1;  // 시간 재개 (게임 일시정지 해제)
                    panelCanvas.sortingOrder = originalSortingOrder;  // 원래 순서로 복원
                    MouseCam mouseCam = FindObjectOfType<MouseCam>();
                    if (mouseCam != null)
                    {
                        mouseCam.SetCursorState(true);  // 커서 잠금
                    }
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
