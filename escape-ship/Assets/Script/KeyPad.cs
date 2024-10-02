using System.Collections;
using TMPro;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    private bool isMouseOverItem = false;  // ������ �����ۿ� ���������� ����
    [SerializeField] TextMeshProUGUI pickUpText;  // ȭ�鿡 ����� �ؽ�Ʈ (TextMeshPro ��� ��)
    [SerializeField] GameObject keyPadPanel;  // ��й�ȣ �Է� �г�
    [SerializeField] Transform player;  // �÷��̾��� Transform
    [SerializeField] float interactDistance = 3f;  // ��ȣ�ۿ� ���� �Ÿ�
    [SerializeField] Canvas keyPadCanvas;  // Ű�е� �г��� Canvas
    private int originalSortingOrder;  // ������ Canvas ����

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);  // ó���� �ؽ�Ʈ�� ��Ȱ��ȭ
        keyPadPanel.SetActive(false);  // �гε� ó������ ��Ȱ��ȭ
        originalSortingOrder = keyPadCanvas.sortingOrder;  // �ʱ� Canvas ������ ����
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;
        KeyManager.Instance.keyDic[KeyAction.Setting] += ClosePlay;
    }

    private void Update()
    {
        // �÷��̾�� KeyPad ������ �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // ���콺�� KeyPad�� ���� ��, ���� �Ÿ� ���� ������ ��ȣ�ۿ� �ؽ�Ʈ�� ������
        if (isMouseOverItem && distanceToPlayer <= interactDistance)
        {
            pickUpText.gameObject.SetActive(true);  // �ؽ�Ʈ Ȱ��ȭ
            pickUpText.text = "EŰ�� ���� ��й�ȣ �е带 ������";  // �ȳ� ���� ����       
        }
        else
        {
            pickUpText.gameObject.SetActive(false);  // ���� ���̰ų� ������ ����� �ؽ�Ʈ ��Ȱ��ȭ
        }
    }

    // �÷��̾ ���콺�� KeyPad�� �÷��� �� (������ �������� ��)
    private void OnMouseEnter()
    {
        isMouseOverItem = true;  // ������ KeyPad�� ������
    }

    // �÷��̾ ���콺�� KeyPad���� ���� �� (������ ����� ��)
    private void OnMouseExit()
    {
        isMouseOverItem = false;  // ������ KeyPad���� ���
    }

     

    public void OnPlay()
    {
        keyPadPanel.SetActive(true);  // �г� Ȱ��ȭ
        keyPadCanvas.sortingOrder = 999;  // Canvas�� �ֻ����� ����
        Time.timeScale = 0;  // �г��� Ȱ��ȭ�Ǹ� ������ �Ͻ�����
        // Ŀ�� ���� ����
        Cursor.visible = true;  
        Cursor.lockState = CursorLockMode.None;

    }

    public void ClosePlay()
    {
        keyPadPanel.SetActive(false);  // �г� ��Ȱ��ȭ
        keyPadCanvas.sortingOrder = originalSortingOrder;  // Canvas ������ ���� ������ �ǵ���
        Time.timeScale = 1;  // ���� �ٽ� ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
