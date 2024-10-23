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
    [SerializeField] CanvasGroup blackoutCanvasGroup;
    [SerializeField] float fadeDuration = 2f;
    [SerializeField] float elevatorMoveDuration = 3f;

    private int originalSortingOrder;

    private void Start()
    {
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

        if (blackoutCanvasGroup != null)
        {
            blackoutCanvasGroup.alpha = 0;
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

            doorMotion.CloseDoor();
            StartCoroutine(MoveElevator());
            CloseKeyPad();  // Ű�е� �ݱ�
            doorMotion.OpenDoor();
        }
        else
        {
            Debug.Log("��й�ȣ�� Ʋ�Ƚ��ϴ�.");
        }
    }

    private IEnumerator MoveElevator()
    {
        if (elevatorMoveSound != null)
        {
            elevatorMoveSound.Play();
        }

        if (blackoutCanvasGroup != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                blackoutCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            blackoutCanvasGroup.alpha = 1;
        }

        yield return new WaitForSeconds(elevatorMoveDuration);
    }

    public void CloseKeyPad()
    {
        keyPadPanel.SetActive(false);
        keyPadCanvas.sortingOrder = originalSortingOrder;

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
