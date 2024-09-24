using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : MonoBehaviour
{
    public float sensitivity = 500f;
    public float rotationX;
    public float rotationY;
    private bool isCursorLocked = true;  // 커서 잠금 여부 플래그

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

            rotationX = Mathf.Clamp(rotationX, -50f, 40f);  // 고개가 과도하게 젖혀지지 않도록 제한

            transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
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
