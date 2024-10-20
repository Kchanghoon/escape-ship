using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : Singleton<MouseCam>
{
    public float sensitivity = 500f;
    public float rotationX;
    public float rotationY;
    private bool isCursorLocked = true;  // 커서 잠금 여부 플래그
    public float smoothTime = 0.1f;  // 회전의 부드러움을 위한 변수
    private Vector3 currentRotation;  // 현재 회전
    private Vector3 smoothVelocity = Vector3.zero;  // 보간 속도를 위한 변수
    private bool isInOption = false;  // 옵션 메뉴가 열렸는지 여부

    [SerializeField] private GameObject pauseMenuUI;  // PauseMenu UI 참조

    void Start()
    {
        LockCursor();  // 게임 시작 시 커서 잠금
        KeyManager.Instance.keyDic[KeyAction.Inventory] += OnInventory;
        KeyManager.Instance.keyDic[KeyAction.Setting] += OnOption;
    }

    void Update()
    {
        // 커서가 잠금 상태일 때만 화면 회전 가능, 옵션 메뉴가 열려 있지 않은 상태에서만 회전
        if (isCursorLocked && !isInOption)
        {
            float mouseMoveX = Input.GetAxis("Mouse X");
            float mouseMoveY = Input.GetAxis("Mouse Y");

            rotationY += mouseMoveX * sensitivity * Time.deltaTime;
            rotationX -= mouseMoveY * sensitivity * Time.deltaTime;

            // X축 회전을 제한하여 고개가 과도하게 젖혀지지 않도록
            rotationX = Mathf.Clamp(rotationX, -50f, 60f);

            // 현재 회전값을 보간하여 부드럽게 처리
            Vector3 targetRotation = new Vector3(rotationX, rotationY, 0);
            currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref smoothVelocity, smoothTime);

            // 카메라의 EulerAngles 업데이트
            transform.eulerAngles = currentRotation;
        }
    }

    // 인벤토리 UI를 열고 닫을 때 호출되는 함수
    void OnInventory()
    {
        // PauseMenuUI가 활성화된 상태면 인벤토리 입력을 차단
        if (pauseMenuUI.activeSelf)
        {
            Debug.Log("Pause 메뉴가 활성화된 상태에서는 인벤토리 입력이 불가합니다.");
            return;  // 입력을 무시하고 함수 종료
        }
        else
        {
            // 인벤토리 UI가 열릴 때는 옵션 메뉴와 관계없이 커서 잠금을 토글
            isCursorLocked = !isCursorLocked;

            if (isCursorLocked)
            {
                LockCursor();  // 커서를 잠금
            }
            else
            {
                UnlockCursor();  // 커서를 해제
            }
        }
    }

    // 옵션 메뉴를 열고 닫을 때 호출되는 함수
    void OnOption()
    {
        // 옵션 메뉴가 열리면 커서를 해제하고, 닫으면 다시 잠금
        isInOption = !isInOption;

        if (isInOption)
        {
            UnlockCursor();  // 옵션 메뉴가 열리면 커서를 해제
        }
        else
        {
            // 옵션 메뉴가 닫힐 때는 인벤토리 상태를 고려하여 커서 잠금을 설정
            if (isCursorLocked)
            {
                LockCursor();  // 인벤토리가 열려 있지 않으면 커서 잠금
            }
        }
    }

    // 커서 잠금 함수
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // 커서 해제 함수
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}