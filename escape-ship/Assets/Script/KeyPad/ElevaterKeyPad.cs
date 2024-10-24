using System.Collections;
using UnityEngine;

public class ElevaterKeyPad : MonoBehaviour
{
    private bool isMouseOverItem = false;
    [SerializeField] GameObject keyPadPanel;
    [SerializeField] Transform player;
    [SerializeField] float interactDistance = 3f;
    [SerializeField] Canvas keyPadCanvas;
    [SerializeField] string correctPassword;
    [SerializeField] ZDoorMotion doorMotion;
    [SerializeField] AudioSource doorSound;
    [SerializeField] AudioSource elevatorMoveSound;
    [SerializeField] KeypadController keyPadController;
    [SerializeField] float elevatorMoveDuration = 3f;
    [SerializeField] StageManager stageManager;  // StageManager �ν��Ͻ� ����

    [SerializeField] BlackOutChange blackOutChange;  // BlackOutChange ��ũ��Ʈ ����
    private int originalSortingOrder;

    private void Start()
    {
        // StageManager �ν��Ͻ� ��������
        stageManager = StageManager.Instance;

        keyPadPanel.SetActive(false);
        originalSortingOrder = keyPadCanvas.sortingOrder;

        // KeyAction.Play �̺�Ʈ�� OnPlay �޼��� ����
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;

        if (doorSound == null)
        {
            Debug.LogWarning("AudioSource�� �Ҵ���� �ʾҽ��ϴ�. ���尡 ������� �ʽ��ϴ�.");
        }

        if (elevatorMoveSound == null)
        {
            Debug.LogWarning("���������� �̵� �Ҹ��� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
    }

    private void OnMouseEnter()
    {
        isMouseOverItem = true;
    }

    private void OnMouseExit()
    {
        isMouseOverItem = false;
    }

    // �÷��̾ Play Ű�� ������ �� ȣ��� �޼���
    public void OnPlay()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (isMouseOverItem && distanceToPlayer <= interactDistance)
        {
            OpenKeyPadPanel();
        }
    }

    private void OpenKeyPadPanel()
    {
        keyPadPanel.SetActive(true);
        keyPadCanvas.sortingOrder = 999;

        Time.timeScale = 0;  // �ð� ���� (���� �Ͻ�����)
        keyPadController.SetActiveElevaterKeyPad(this); // ElevaterKeyPad�� ����

        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.SetCursorState(false);  // Ŀ���� ��� ����
        }
    }

    public void CheckPassword(string inputPassword)
    {
        if (inputPassword == correctPassword)
        {
            Debug.Log("��й�ȣ�� �½��ϴ�. ���������Ͱ� �̵��մϴ�.");

            if (doorSound != null)
            {
                doorSound.Play();
            }

            // Ű�е带 ���� �ݰ� ���ƿ� �� ���������� �̵� ó��
            CloseKeyPad();
            StartCoroutine(HandleElevatorTransition());
            doorMotion.CloseDoor();
        }
        else
        {
            Debug.Log("��й�ȣ�� Ʋ�Ƚ��ϴ�.");
        }
    }

    // ���ƿ��� ���������� �̵� ó��
    private IEnumerator HandleElevatorTransition()
    {        // ���������� �̵� �Ҹ� ���
        if (elevatorMoveSound != null)
        {
            elevatorMoveSound.Play();
        }
        // ���ƿ� ����
        if (blackOutChange != null)
        {
            yield return StartCoroutine(blackOutChange.StartBlackOut());
        }

        doorMotion.CloseDoor();

        // ���ƿ� ����
        if (blackOutChange != null)
        {
            yield return StartCoroutine(blackOutChange.EndBlackOut());
        }
        stageManager.ActivateStage(2);

    }

    public void CloseKeyPad()
    {
        // Ű�е带 �ڷ�ƾ ���� ���� ���� ����
        keyPadPanel.SetActive(false);
        keyPadCanvas.sortingOrder = originalSortingOrder;

        Time.timeScale = 1;  // �ð� �簳 (���� �Ͻ����� ����)
        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.SetCursorState(true);  // Ŀ���� ���
        }
    }

    private void OnDestroy()
    {
        // OnDestroy���� �̺�Ʈ�� �����Ͽ� �޸� ���� ����
        KeyManager.Instance.keyDic[KeyAction.Play] -= OnPlay;
    }
}
