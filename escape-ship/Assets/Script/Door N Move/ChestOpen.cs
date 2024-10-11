using TMPro;
using UnityEngine;
using DG.Tweening;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private Transform lid;  // ������ �Ѳ� (������ �κ�)
    [SerializeField] private Vector3 closedPosition = Vector3.zero;  // �������� ���� ���� ��ġ��
    [SerializeField] private float openPositionX = 1f;  // ������ ���� X�� ��ġ��
    [SerializeField] private float duration = 1f;  // ������ �ӵ�
    [SerializeField] private Ease motionEase = Ease.InOutQuad;  // �ִϸ��̼� Ease

    [SerializeField] private TextMeshProUGUI statusText;  // ���� �޽����� ����� TextMeshPro ����
    [SerializeField] private float interactionDistance = 3f;  // ��ȣ�ۿ� ���� �Ÿ�

    private bool isOpen = false;  // ���ڰ� ���ȴ��� ���θ� ���
    private bool isMouseOverChest = false;  // ���콺�� ���� ���� �ִ��� ����
    private bool hasUsedBattery = false;  // ���͸��� ����Ͽ� ���ڸ� ó�� �������� Ȯ���ϴ� �÷���
    private Transform playerTransform;  // �÷��̾��� Transform�� ����

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // �÷��̾��� Transform ��������
        lid.localPosition = closedPosition;  // ó���� ���� ���·� ����
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;

        if (statusText != null)
        {
            statusText.gameObject.SetActive(false);  // ó������ ���� �޽��� ��Ȱ��ȭ
        }
    }

    private void Update()
    {
        // �÷��̾�� ���� ������ �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        // �÷��̾ ��ȣ�ۿ� ������ �Ÿ� �ȿ� ���� ���� ���콺�� �ø��� ��ȣ�ۿ� ����
        if (distanceToPlayer <= interactionDistance && isMouseOverChest)
        {
            statusText.gameObject.SetActive(true);  // ���� �޽��� Ȱ��ȭ
            UpdateStatusText();  // ���� �ؽ�Ʈ ǥ��
        }
        else
        {
            statusText.gameObject.SetActive(false);  // ���� �޽��� ��Ȱ��ȭ
        }
    }

    // ���콺�� ���� ���� ���� �� ȣ��Ǵ� �Լ�
    private void OnMouseEnter()
    {
        isMouseOverChest = true;  // ���콺�� ���� ���� ����
    }

    // ���콺�� ���ڿ��� ����� �� ȣ��Ǵ� �Լ�
    private void OnMouseExit()
    {
        isMouseOverChest = false;  // ���콺�� ���ڿ��� ���
        statusText.gameObject.SetActive(false);  // ���� �޽��� ��Ȱ��ȭ
    }

    // KeyManager���� Play �׼��� ȣ��� �� ���� ����/�ݱ� ó��
    public void OnPlay()
    {
        // �÷��̾ ��ȣ�ۿ� ������ �Ÿ� ���� �ְ�, ���콺�� ���� ���� ���� �� ���ڸ� �� �� ����
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        if (distanceToPlayer <= interactionDistance && isMouseOverChest)
        {
            // ���ڸ� ó�� �� ���� ���͸��� �Ҹ�
            if (!hasUsedBattery)
            {
                // ���� ������ �������� ���͸�(ID = 1)���� Ȯ��
                var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
                if (selectedItem != null && selectedItem.id == "1")
                {
                    // ���ڸ� ���ų� ���� �� ���͸� ���� ����
                    ItemController.Instance.DecreaseItemQuantity("1");  // ���͸� ���� ����
                    hasUsedBattery = true;  // ���͸��� ��������� ���
                    UpdateStatusText();

                    ToggleChest();
                }
                else
                {
                    Debug.Log("���͸��� ���õ��� �ʾҽ��ϴ�.");
                }
            }
            else
            {
                // �̹� ���͸��� ��������Ƿ� ���͸� ���� ���ڸ� ���ų� ����
                ToggleChest();
            }
        }
    }

    // ���� �޽����� ������Ʈ�ϴ� �Լ�
    private void UpdateStatusText()
    {
        if (hasUsedBattery)
        {
        }
        else
        {
            statusText.text = "���͸��� �ʿ��մϴ�. EŰ�� ���� ���͸��� �־��ּ���.";
        }
    }

    // ���ڸ� ���ų� �ݴ� �Լ�
    private void ToggleChest()
    {
        if (isOpen)
        {
            // ���ڰ� ���������� �ݱ� (���� ��ġ ���)
            lid.DOLocalMoveX(closedPosition.x, duration).SetEase(motionEase);
            statusText.gameObject.SetActive(true);  // ���ڰ� ������ �ؽ�Ʈ Ȱ��ȭ
        }
        else
        {
            // ���ڰ� ���������� ���� (X������ �̵�)
            lid.DOLocalMoveX(openPositionX, duration).SetEase(motionEase);
            statusText.gameObject.SetActive(false);  // ���ڰ� ������ �ؽ�Ʈ ��Ȱ��ȭ
        }

        isOpen = !isOpen;  // ���¸� ������Ŵ
    }

}
