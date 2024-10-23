using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : Singleton<MouseCam>
{
    public float sensitivity = 500f;
    public float rotationX;
    public float rotationY;
    public float smoothTime = 0.1f;  // ȸ���� �ε巯���� ���� ����
    private Vector3 currentRotation;  // ���� ȸ��
    private Vector3 smoothVelocity = Vector3.zero;  // ���� �ӵ��� ���� ����
    private bool isPaused = false;  // ������ �Ͻ� �����Ǿ����� ����

    [SerializeField] private GameObject pauseMenuUI;  // PauseMenu UI ����

    void Start()
    {
        SetCursorState(true);  // ���� ���� �� Ŀ�� ���
        KeyManager.Instance.keyDic[KeyAction.Inventory] += OnInventory;
        KeyManager.Instance.keyDic[KeyAction.Setting] += OnOption;
    }

    void Update()
    {
        // ������ �Ͻ� �������� �ʾ��� ���� ī�޶� ȸ�� ����
        if (!isPaused)
        {
            float mouseMoveX = Input.GetAxis("Mouse X");
            float mouseMoveY = Input.GetAxis("Mouse Y");

            rotationY += mouseMoveX * sensitivity * Time.deltaTime;
            rotationX -= mouseMoveY * sensitivity * Time.deltaTime;

            // X�� ȸ���� �����Ͽ� ���� �����ϰ� �������� �ʵ���
            rotationX = Mathf.Clamp(rotationX, -50f, 60f);

            // ���� ȸ������ �����Ͽ� �ε巴�� ó��
            Vector3 targetRotation = new Vector3(rotationX, rotationY, 0);
            currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref smoothVelocity, smoothTime);

            // ī�޶��� EulerAngles ������Ʈ
            transform.eulerAngles = currentRotation;
        }
    }

    // �κ��丮 UI�� ���� ���� �� ȣ��Ǵ� �Լ�
    void OnInventory()
    {
        if (pauseMenuUI.activeSelf)
        {
            Debug.Log("Pause �޴��� Ȱ��ȭ�� ���¿����� �κ��丮 �Է��� �Ұ��մϴ�.");
            return;
        }

        isPaused = !isPaused;
        SetCursorState(!isPaused);  // Ŀ�� ����� ���
    }

    // �ɼ� �޴��� ���� ���� �� ȣ��Ǵ� �Լ�
    void OnOption()
    {
        isPaused = !isPaused;
        SetCursorState(!isPaused);  // �ɼ� �޴� ���¿� ���� Ŀ�� ���¸� ����
    }

    // Ŀ�� ���¸� �ϰ��ǰ� �����ϴ� �Լ�
    public void SetCursorState(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // ���� ����� �� ȣ��� �Լ� (���� �簳 �� Ŀ�� ���� �ʱ�ȭ)
    public void OnGameResume()
    {
        isPaused = false;
        SetCursorState(true);  // ���� �簳 �� Ŀ�� ���
    }
}
