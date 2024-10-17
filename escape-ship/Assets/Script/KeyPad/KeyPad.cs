using TMPro;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    private bool isMouseOverItem = false;  // ���콺�� ������Ʈ ���� �ִ��� Ȯ��
    [SerializeField] TextMeshProUGUI pickUpText;
    [SerializeField] GameObject keyPadPanel;
    [SerializeField] Transform player;
    [SerializeField] float interactDistance = 3f;  // ��ȣ�ۿ� ������ �Ÿ�
    [SerializeField] Canvas keyPadCanvas;
    [SerializeField] string correctPassword;  // �ùٸ� ��й�ȣ
    [SerializeField] ZDoorMotion doorMotion;  // �� ���� ��ũ��Ʈ
    [SerializeField] AudioSource doorSound;  // �� ���� ����
    [SerializeField] KeypadController keyPadController;  // KeyPadController ����

    private int originalSortingOrder;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
        keyPadPanel.SetActive(false);
        originalSortingOrder = keyPadCanvas.sortingOrder;
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;

        if (doorSound == null)
        {
            Debug.LogWarning("AudioSource�� �Ҵ���� �ʾҽ��ϴ�. ���尡 ������� �ʽ��ϴ�.");
        }
    }

    private void Update()
    {
        // �÷��̾�� Ű�е� ������Ʈ ������ �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // ���콺�� ������Ʈ ���� �ְ�, ��ȣ�ۿ� ������ �Ÿ� �̳����� Ȯ��
        if (isMouseOverItem && distanceToPlayer <= interactDistance)
        {
            ShowPickUpText();  // �ؽ�Ʈ Ȱ��ȭ �� �޽��� ǥ��
        }
        else
        {
            HidePickUpText();  // �ؽ�Ʈ ��Ȱ��ȭ
        }
    }

    // �ؽ�Ʈ Ȱ��ȭ
    private void ShowPickUpText()
    {
        pickUpText.gameObject.SetActive(true);
        pickUpText.text = "EŰ�� ���� ��й�ȣ �е带 ������";
    }

    // �ؽ�Ʈ ��Ȱ��ȭ
    private void HidePickUpText()
    {
        pickUpText.gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        isMouseOverItem = true;  // ���콺�� ������Ʈ ���� ���� ��
    }

    private void OnMouseExit()
    {
        isMouseOverItem = false;  // ���콺�� ������Ʈ ������ ��� ��
    }

    // �÷��̾ EŰ�� ������ �� ȣ��Ǵ� �Լ�
    public void OnPlay()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // ���콺�� ������Ʈ ���� �ְ�, ��ȣ�ۿ� ������ �Ÿ� �̳��� ���� �г� ����
        if (isMouseOverItem && distanceToPlayer <= interactDistance)
        {
            OpenKeyPadPanel();
        }
    }

    // Ű�е� �г��� ���� �Լ�
    private void OpenKeyPadPanel()
    {
        keyPadPanel.SetActive(true);
        keyPadCanvas.sortingOrder = 999;  // �г��� �ֻ����� ���̵��� ����
        Time.timeScale = 0;  // ���� �Ͻ�����

        // KeyPadController�� ���� Ű�е� ����
        keyPadController.SetActiveKeyPad(this);

        // Ŀ�� ��� ����
        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.UnlockCursor();
        }
    }

    // �Է��� ��й�ȣ Ȯ��
    public void CheckPassword(string inputPassword)
    {
        if (inputPassword == correctPassword)
        {
            Debug.Log("��й�ȣ�� �½��ϴ�. ���� ���ϴ�.");

            if (doorSound != null)
            {
                doorSound.Play();
            }

            doorMotion.OpenDoor();  // �� ����
            CloseKeyPad();  // Ű�е� �ݱ�
        }
        else
        {
            Debug.Log("��й�ȣ�� Ʋ�Ƚ��ϴ�.");
        }
    }

    // Ű�е� �г��� �ݴ� �Լ�
    public void CloseKeyPad()
    {
        keyPadPanel.SetActive(false);
        keyPadCanvas.sortingOrder = originalSortingOrder;
        Time.timeScale = 1;  // ���� �簳

        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.LockCursor();
        }
    }
}
