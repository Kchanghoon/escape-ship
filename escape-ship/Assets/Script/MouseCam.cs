using System.Collections;
using UnityEngine;

public class MouseCam : Singleton<MouseCam>
{
    public float sensitivity = 300f;
    public float rotationX;
    public float rotationY;
    public float smoothTime = 0.1f;  // 회전의 부드러움을 위한 변수
    private Vector3 currentRotation;  // 현재 회전
    private Vector3 smoothVelocity = Vector3.zero;  // 보간 속도를 위한 변수
    private bool isPaused = false;  // 게임이 일시 중지되었는지 여부

    [SerializeField] private GameObject pauseMenuUI;  // PauseMenu UI 참조

    void Start()
    {
        KeyManager.Instance.keyDic[KeyAction.Setting] += OnOption;
    }

    void Update()
    {
        // 게임이 일시 중지되지 않았을 때만 카메라 회전 가능
        if (!isPaused)
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

    // 옵션 메뉴를 열고 닫을 때 호출되는 함수
    void OnOption()
    {
        if (pauseMenuUI.activeSelf) return;  // PauseMenu가 활성화되어 있을 때는 옵션을 열 수 없음

        TogglePause();  // 일시정지 상태 토글
    }

    // 게임 일시정지 상태를 설정하는 함수
    public void TogglePause()
    {
        isPaused = !isPaused;
        SetCursorState(!isPaused);
    }

    // 게임 재개 시 호출될 함수
    public void ResumeGame()
    {
        isPaused = false;
        SetCursorState(true);  // 커서 잠금 및 회전 활성화
    }

    // 게임을 완전히 일시정지하는 메서드
    public void PauseGame()
    {
        isPaused = true;
        SetCursorState(false);  // 커서 표시 및 잠금 해제
    }

    // 커서 상태를 일관되게 설정하는 함수
    public void SetCursorState(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLocked;
    }
}
