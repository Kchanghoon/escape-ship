using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : MonoBehaviour
{
    public float sensitivity = 500f;
    public float rotationX;
    public float rotationY;
    private bool isCursorLocked = true;  // Ŀ�� ��� ���� �÷���
    public float smoothTime = 0.1f;  // ȸ���� �ε巯���� ���� ����
    private Vector3 currentRotation;  // ���� ȸ��
    private Vector3 smoothVelocity = Vector3.zero;  // ���� �ӵ��� ���� ����

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

            // X�� ȸ���� �����Ͽ� ���� �����ϰ� �������� �ʵ���
            rotationX = Mathf.Clamp(rotationX, -50f, 40f);

            // ���� ȸ������ �����Ͽ� �ε巴�� ó��
            Vector3 targetRotation = new Vector3(rotationX, rotationY, 0);
            currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref smoothVelocity, smoothTime);

            // ī�޶��� EulerAngles ������Ʈ
            transform.eulerAngles = currentRotation;
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
