using System.Collections;
using TMPro;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    private bool isMouseOverItem = false;  // 에임이 아이템에 맞춰졌는지 여부
    [SerializeField] TextMeshProUGUI pickUpText;  // 화면에 출력할 텍스트 (TextMeshPro 사용 시)
    [SerializeField] GameObject keyPadPanel;  // 비밀번호 입력 패널
    [SerializeField] Transform player;  // 플레이어의 Transform
    [SerializeField] float interactDistance = 3f;  // 상호작용 가능 거리
    [SerializeField] Canvas keyPadCanvas;  // 키패드 패널의 Canvas
    private int originalSortingOrder;  // 원래의 Canvas 순서

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);  // 처음엔 텍스트를 비활성화
        keyPadPanel.SetActive(false);  // 패널도 처음에는 비활성화
        originalSortingOrder = keyPadCanvas.sortingOrder;  // 초기 Canvas 순서를 저장
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;
        KeyManager.Instance.keyDic[KeyAction.Setting] += ClosePlay;
    }

    private void Update()
    {
        // 플레이어와 KeyPad 사이의 거리 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 마우스가 KeyPad에 있을 때, 일정 거리 내에 있으면 상호작용 텍스트를 보여줌
        if (isMouseOverItem && distanceToPlayer <= interactDistance)
        {
            pickUpText.gameObject.SetActive(true);  // 텍스트 활성화
            pickUpText.text = "E키를 눌러 비밀번호 패드를 여세요";  // 안내 문구 설정       
        }
        else
        {
            pickUpText.gameObject.SetActive(false);  // 범위 밖이거나 에임이 벗어나면 텍스트 비활성화
        }
    }

    // 플레이어가 마우스를 KeyPad에 올렸을 때 (에임이 맞춰졌을 때)
    private void OnMouseEnter()
    {
        isMouseOverItem = true;  // 에임이 KeyPad에 맞춰짐
    }

    // 플레이어가 마우스를 KeyPad에서 뗐을 때 (에임이 벗어났을 때)
    private void OnMouseExit()
    {
        isMouseOverItem = false;  // 에임이 KeyPad에서 벗어남
    }

     

    public void OnPlay()
    {
        keyPadPanel.SetActive(true);  // 패널 활성화
        keyPadCanvas.sortingOrder = 999;  // Canvas를 최상위로 설정
        Time.timeScale = 0;  // 패널이 활성화되면 게임을 일시정지
        // 커서 상태 변경
        Cursor.visible = true;  
        Cursor.lockState = CursorLockMode.None;

    }

    public void ClosePlay()
    {
        keyPadPanel.SetActive(false);  // 패널 비활성화
        keyPadCanvas.sortingOrder = originalSortingOrder;  // Canvas 순서를 원래 값으로 되돌림
        Time.timeScale = 1;  // 게임 다시 진행
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
