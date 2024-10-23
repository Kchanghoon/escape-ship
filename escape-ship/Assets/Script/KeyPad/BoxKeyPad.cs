using TMPro;
using UnityEngine;

public class BoxKeyPad : MonoBehaviour
{
    private bool isMouseOverItem = false;  // 마우스가 오브젝트 위에 있는지 여부를 저장
    [SerializeField] TextMeshProUGUI pickUpText;  // 상호작용 안내 텍스트
    [SerializeField] GameObject keyPadPanel;  // 키패드 UI 패널
    [SerializeField] Transform player;  // 플레이어의 Transform
    [SerializeField] float interactDistance = 3f;  // 상호작용 가능 거리
    [SerializeField] Canvas keyPadCanvas;  // 키패드 UI가 있는 캔버스
    [SerializeField] string correctPassword;  // 올바른 비밀번호
    [SerializeField] KeypadController keyPadController;  // 키패드 컨트롤러

    private int originalSortingOrder;  // 캔버스의 원래 정렬 순서
    private bool isUnlocked = false;  // 문이 열렸는지 여부를 저장하는 플래그

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);  // 처음에 상호작용 안내 텍스트 비활성화
        keyPadPanel.SetActive(false);  // 키패드 패널 비활성화
        originalSortingOrder = keyPadCanvas.sortingOrder;  // 캔버스의 원래 정렬 순서를 저장
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;  // KeyManager에서 Play 키 이벤트 등록

    }

    private void Update()
    {
        // 플레이어와 키패드 오브젝트 사이의 거리를 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 마우스가 오브젝트 위에 있고 상호작용 가능한 거리 내에 있으면 상호작용 안내 텍스트 표시
        if (isMouseOverItem && distanceToPlayer <= interactDistance && !isUnlocked)
        {
            ShowPickUpText();  // 상호작용 안내 텍스트 활성화
        }
        else
        {
            HidePickUpText();  // 상호작용 안내 텍스트 비활성화
        }
    }

    // 상호작용 안내 텍스트를 활성화하는 메서드
    private void ShowPickUpText()
    {
        pickUpText.gameObject.SetActive(true);  // 텍스트 활성화
        pickUpText.text = "E키를 눌러 비밀번호 입력창을 여세요";  // 안내 메시지 설정
    }

    // 상호작용 안내 텍스트를 비활성화하는 메서드
    private void HidePickUpText()
    {
        pickUpText.gameObject.SetActive(false);  // 텍스트 비활성화
    }

    // 마우스가 오브젝트 위에 있을 때 호출되는 메서드
    private void OnMouseEnter()
    {
        isMouseOverItem = true;  // 마우스가 오브젝트 위에 있음
    }

    // 마우스가 오브젝트에서 벗어날 때 호출되는 메서드
    private void OnMouseExit()
    {
        isMouseOverItem = false;  // 마우스가 오브젝트 위에 없음
    }

    // 플레이어가 E키를 눌렀을 때 호출되는 메서드
    public void OnPlay()
    {
        // 문이 이미 열렸으면 키패드 패널을 열지 않음
        if (isUnlocked) return;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 인벤토리에서 선택된 아이템을 가져옴
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();

        // 마우스가 오브젝트 위에 있고, 상호작용 가능한 거리 내에 있으며, 선택된 아이템이 null이 아닌지 확인
        if (isMouseOverItem && distanceToPlayer <= interactDistance && selectedItem != null &&
            (selectedItem.id == "7" || selectedItem.id == "6" || selectedItem.id == "5"))
        {
            OpenKeyPadPanel();
        }
        else
        {
            Debug.Log("조건을 만족하지 않거나 선택된 아이템이 없습니다.");
        }
    }


    // 키패드 패널을 여는 메서드
    private void OpenKeyPadPanel()
    {
        keyPadPanel.SetActive(true);  // 키패드 UI 패널 활성화
        keyPadCanvas.sortingOrder = 999;  // 캔버스의 정렬 순서를 최상위로 설정
        Time.timeScale = 0;  // 시간 정지 (게임 일시정지)

        // KeyPadController에 현재 키패드를 설정
        keyPadController.SetActiveBoxKeyPad(this);

        // 마우스 커서 잠금 해제
        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.SetCursorState(false);  // 커서 잠금 해제
        }
    }

    // 입력된 비밀번호를 확인하는 메서드
    public void CheckPassword(string inputPassword)
    {
        if (inputPassword == correctPassword)
        {
            Debug.Log("비밀번호가 맞습니다. 문이 열립니다.");

            var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
            if (selectedItem != null) // 선택된 아이템이 null이 아닌 경우
            {
                if (selectedItem.id == "7")
                {
                    ItemController.Instance.DeleteItemQuantity("7");
                    ItemController.Instance.AddItem("6");
                }
                else if (selectedItem.id == "6")
                {
                    ItemController.Instance.DeleteItemQuantity("6");
                    ItemController.Instance.AddItem("5");

                }
                else if (selectedItem.id == "5")
                {
                    ItemController.Instance.AddItem("11");
                }
            }

            isUnlocked = true;  // 문이 열렸음을 표시
            CloseKeyPad();  // 키패드 패널 닫기
        }
        else
        {
            Debug.Log("비밀번호가 틀렸습니다.");
        }
    }

    // 키패드 패널을 닫는 메서드
    public void CloseKeyPad()
    {
        keyPadPanel.SetActive(false);  // 키패드 패널 비활성화
        keyPadCanvas.sortingOrder = originalSortingOrder;  // 캔버스 정렬 순서를 원래대로 복원
        Time.timeScale = 1;  // 시간 재개 (게임 일시정지 해제)

        // 마우스 커서 잠금
        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.SetCursorState(true);  // 커서 잠금
        }
    }
}
