using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : MonoBehaviour
{
    public float sensitivity = 500f;
    public float rotationX;
    public float rotationY;
    private bool isCursorLocked = true;  // Ŀ�� ��� ���� �÷���

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // ���� ���� �� Ŀ���� ��� ���·� ����
        Cursor.visible = false;  // Ŀ�� �����
    }

    void Update()
    {
        // ��Ű�� ������ Ŀ�� ��� ���¸� ����
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleCursorLock();
        }

        // Ŀ���� ��� ������ ���� ȭ�� ȸ�� ����
        if (isCursorLocked)
        {
            float mouseMoveX = Input.GetAxis("Mouse X");
            float mouseMoveY = Input.GetAxis("Mouse Y");

            rotationY += mouseMoveX * sensitivity * Time.deltaTime;
            rotationX -= mouseMoveY * sensitivity * Time.deltaTime;

            rotationX = Mathf.Clamp(rotationX, -50f, 40f);  // ���� �����ϰ� �������� �ʵ��� ����

            transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
        }
    }

    // Ŀ�� ��� ���¸� ����ϴ� �Լ�
    void ToggleCursorLock()
    {
        isCursorLocked = !isCursorLocked;

        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;  // Ŀ���� ���
            Cursor.visible = false;  // Ŀ�� �����
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;  // Ŀ���� ����
            Cursor.visible = true;  // Ŀ�� ���̱�
        }
    }
}
