using UnityEngine;
using DG.Tweening;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private Transform lid;  // ������ �Ѳ� (������ �κ�)
    [SerializeField] private Vector3 closedPosition = Vector3.zero;  // �������� ���� ���� ��ġ��
    [SerializeField] private float openPositionX = 1f;  // ������ ���� X�� ��ġ��
    [SerializeField] private float duration = 1f;  // ������ �ӵ�
    [SerializeField] private Ease motionEase = Ease.InOutQuad;  // �ִϸ��̼� Ease
    private bool isOpen = false;  // ���ڰ� ���ȴ��� ���θ� ���
    private bool playerInRange = false;  // �÷��̾ ������ �ִ��� Ȯ���ϴ� �÷���
    private bool hasUsedBattery = false;  // ���͸��� ����Ͽ� ���ڸ� ó�� �������� Ȯ���ϴ� �÷���

    void Start()
    {
        lid.localPosition = closedPosition;  // ó���� ���� ���·� ����
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;
    }

    // KeyManager���� Play �׼��� ȣ��� �� ���� ����/�ݱ� ó��
    public void OnPlay()
    {
        if (playerInRange)
        {
            // ���ڸ� ó�� �� ���� ���͸��� �Ҹ�
            if (!hasUsedBattery)
            {
                // ���� ������ �������� ���͸�(ID = 1)���� Ȯ��
                var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
                if (selectedItem != null && selectedItem.id == "1")
                {
                    // ���ڸ� ���ų� ���� �� ���͸� ���� ����
                    ToggleChest();
                    ItemController.Instance.DecreaseItemQuantity("1");  // ���͸� ���� ����
                    hasUsedBattery = true;  // ���͸��� ��������� ���
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

    // ���ڸ� ���ų� �ݴ� �Լ�
    private void ToggleChest()
    {
        if (isOpen)
        {
            // ���ڰ� ���������� �ݱ� (���� ��ġ ���)
            lid.DOLocalMoveX(closedPosition.x, duration).SetEase(motionEase);
        }
        else
        {
            // ���ڰ� ���������� ���� (X������ �̵�)
            lid.DOLocalMoveX(openPositionX, duration).SetEase(motionEase);
        }

        isOpen = !isOpen;  // ���¸� ������Ŵ
    }

    // �÷��̾ Ʈ���� ���� ������ ������ �� ȣ��Ǵ� �Լ�
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // �÷��̾�� 'Player' �±װ� �ִ��� Ȯ��
        {
            playerInRange = true;  // �÷��̾ ���� �ȿ� ����
        }
    }

    // �÷��̾ Ʈ���� �������� ����� �� ȣ��Ǵ� �Լ�
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // �÷��̾ ������ ���
        {
            playerInRange = false;  // �÷��̾ ���� ������ ����
        }
    }
}
