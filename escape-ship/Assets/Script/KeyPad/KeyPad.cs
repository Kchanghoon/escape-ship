using TMPro;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    private bool isMouseOverItem = false;  // 마우스가 오브젝트 위에 있는지 확인
    [SerializeField] TextMeshProUGUI pickUpText;
    [SerializeField] GameObject keyPadPanel;
    [SerializeField] Transform player;
    [SerializeField] float interactDistance = 3f;  // 상호작용 가능한 거리
    [SerializeField] Canvas keyPadCanvas;
    [SerializeField] string correctPassword;  // 올바른 비밀번호
    [SerializeField] ZDoorMotion doorMotion;  // 문 동작 스크립트
    [SerializeField] AudioSource doorSound;  // 문 열림 사운드
    [SerializeField] KeypadController keyPadController;  // KeyPadController 참조

    private int originalSortingOrder;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
        keyPadPanel.SetActive(false);
        originalSortingOrder = keyPadCanvas.sortingOrder;
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;

        if (doorSound == null)
        {
            Debug.LogWarning("AudioSource가 할당되지 않았습니다. 사운드가 재생되지 않습니다.");
        }
    }

    private void Update()
    {
        // 플레이어와 키패드 오브젝트 사이의 거리 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 마우스가 오브젝트 위에 있고, 상호작용 가능한 거리 이내인지 확인
        if (isMouseOverItem && distanceToPlayer <= interactDistance)
        {
            ShowPickUpText();  // 텍스트 활성화 및 메시지 표시
        }
        else
        {
            HidePickUpText();  // 텍스트 비활성화
        }
    }

    // 텍스트 활성화
    private void ShowPickUpText()
    {
        pickUpText.gameObject.SetActive(true);
        pickUpText.text = "E키를 눌러 비밀번호 패드를 여세요";
    }

    // 텍스트 비활성화
    private void HidePickUpText()
    {
        pickUpText.gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        isMouseOverItem = true;  // 마우스가 오브젝트 위에 있을 때
    }

    private void OnMouseExit()
    {
        isMouseOverItem = false;  // 마우스가 오브젝트 위에서 벗어날 때
    }

    // 플레이어가 E키를 눌렀을 때 호출되는 함수
    public void OnPlay()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 마우스가 오브젝트 위에 있고, 상호작용 가능한 거리 이내일 때만 패널 열기
        if (isMouseOverItem && distanceToPlayer <= interactDistance)
        {
            OpenKeyPadPanel();
        }
    }

    // 키패드 패널을 여는 함수
    private void OpenKeyPadPanel()
    {
        keyPadPanel.SetActive(true);
        keyPadCanvas.sortingOrder = 999;  // 패널이 최상위로 보이도록 설정
        Time.timeScale = 0;  // 게임 일시정지

        // KeyPadController에 현재 키패드 설정
        keyPadController.SetActiveKeyPad(this);

        // 커서 잠금 해제
        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.UnlockCursor();
        }
    }

    // 입력한 비밀번호 확인
    public void CheckPassword(string inputPassword)
    {
        if (inputPassword == correctPassword)
        {
            Debug.Log("비밀번호가 맞습니다. 문을 엽니다.");

            if (doorSound != null)
            {
                doorSound.Play();
            }

            doorMotion.OpenDoor();  // 문 열기
            CloseKeyPad();  // 키패드 닫기
        }
        else
        {
            Debug.Log("비밀번호가 틀렸습니다.");
        }
    }

    // 키패드 패널을 닫는 함수
    public void CloseKeyPad()
    {
        keyPadPanel.SetActive(false);
        keyPadCanvas.sortingOrder = originalSortingOrder;
        Time.timeScale = 1;  // 게임 재개

        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.LockCursor();
        }
    }
}
