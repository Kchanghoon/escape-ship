using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : MonoBehaviour
{
    public float sensitivity = 500f;
    public float rotationX;
    public float rotationY;
    private bool isCursorLocked = true;  // 커서 잠금 여부 플래그
    public float smoothTime = 0.1f;  // 회전의 부드러움을 위한 변수
    private Vector3 currentRotation;  // 현재 회전
    private Vector3 smoothVelocity = Vector3.zero;  // 보간 속도를 위한 변수

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // 게임 시작 시 커서를 잠금 상태로 설정
        Cursor.visible = false;  // 커서 숨기기
    }

    void Update()
    {
        // 탭키를 누르면 커서 잠금 상태를 변경
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleCursorLock();
        }

        // 커서가 잠금 상태일 때만 화면 회전 가능
        if (isCursorLocked)
        {
            float mouseMoveX = Input.GetAxis("Mouse X");
            float mouseMoveY = Input.GetAxis("Mouse Y");

            rotationY += mouseMoveX * sensitivity * Time.deltaTime;
            rotationX -= mouseMoveY * sensitivity * Time.deltaTime;

            // X축 회전을 제한하여 고개가 과도하게 젖혀지지 않도록
            rotationX = Mathf.Clamp(rotationX, -50f, 40f);

            // 현재 회전값을 보간하여 부드럽게 처리
            Vector3 targetRotation = new Vector3(rotationX, rotationY, 0);
            currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref smoothVelocity, smoothTime);

            // 카메라의 EulerAngles 업데이트
            transform.eulerAngles = currentRotation;
        }
    }

    // 커서 잠금 상태를 토글하는 함수
    void ToggleCursorLock()
    {
        isCursorLocked = !isCursorLocked;

        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;  // 커서를 잠금
            Cursor.visible = false;  // 커서 숨기기
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;  // 커서를 해제
            Cursor.visible = true;  // 커서 보이기
        }
    }
}
