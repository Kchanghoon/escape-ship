using TMPro;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    private bool isMouseOverItem = false;
    [SerializeField] TextMeshProUGUI pickUpText;
    [SerializeField] GameObject keyPadPanel;
    [SerializeField] Transform player;
    [SerializeField] float interactDistance = 3f;
    [SerializeField] Canvas keyPadCanvas;
    [SerializeField] string correctPassword = "1234";  // �ùٸ� ��й�ȣ (���� ���� ����)
    [SerializeField] ZDoorMotion doorMotion;  // �Ҵ��� �� ���� (�ٸ� ������Ʈ�� �Ҵ� ����)

    private int originalSortingOrder;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
        keyPadPanel.SetActive(false);
        originalSortingOrder = keyPadCanvas.sortingOrder;
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (isMouseOverItem && distanceToPlayer <= interactDistance)
        {
            pickUpText.gameObject.SetActive(true);
            pickUpText.text = "EŰ�� ���� ��й�ȣ �е带 ������";
        }
        else
        {
            pickUpText.gameObject.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        isMouseOverItem = true;
    }

    private void OnMouseExit()
    {
        isMouseOverItem = false;
    }

    public void OnPlay()
    {
        if (isMouseOverItem)
        {
            keyPadPanel.SetActive(true);
            keyPadCanvas.sortingOrder = 999;
            Time.timeScale = 0;
            MouseCam mouseCam = FindObjectOfType<MouseCam>();
            if (mouseCam != null)
            {
                mouseCam.UnlockCursor();
            }
        }
    }

    // ��й�ȣ Ȯ�� �Լ�
    public void CheckPassword(string inputPassword)  // �Ű������� �Էµ� ��й�ȣ �ޱ�
    {
        if (inputPassword == correctPassword)  // ���޹��� ��й�ȣ�� �´��� Ȯ��
        {
            Debug.Log("��й�ȣ�� �¾ҽ��ϴ�. ���� ���ϴ�.");
            doorMotion.OpenDoor();  // �� ����
            CloseKeyPad();  // �г� �ݱ�
        }
        else
        {
            Debug.Log("��й�ȣ�� Ʋ�Ƚ��ϴ�.");
        }
    }

    public void CloseKeyPad()
    {
        keyPadPanel.SetActive(false);
        keyPadCanvas.sortingOrder = originalSortingOrder;
        Time.timeScale = 1;
        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.LockCursor();
        }
    }
}