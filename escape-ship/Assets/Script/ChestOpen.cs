using UnityEngine;
using DG.Tweening;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private Transform lid;  // ������ �Ѳ� (ȸ���� �κ�)
    [SerializeField] private Vector3 closedRotation;  // �������� ���� ȸ���� (��: (0, 0, 0))
    [SerializeField] private Vector3 openRotation;    // ������ ���� ȸ���� (��: (-90, 0, 0))
    [SerializeField] private float duration = 1f;  // ������ �ӵ�
    [SerializeField] private Ease motionEase = Ease.InOutQuad;  // �ִϸ��̼� Ease
    private bool isOpen = false;  // ���ڰ� ���ȴ��� ���θ� ���
    private bool playerInRange = false;  // �÷��̾ ������ �ִ��� Ȯ���ϴ� �÷���

    void Start()
    {
        lid.localRotation = Quaternion.Euler(closedRotation);  // ó���� ���� ���·� ����
    }

    void Update()
    {
        // �÷��̾ ���� �ȿ� ���� ���� E Ű�� ���ڸ� ������
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleChest();  // ���ڸ� ���ų� ����
        }
    }

    // ���ڸ� ���ų� �ݴ� �Լ�
    private void ToggleChest()
    {
        if (isOpen)
        {
            // ���ڰ� ���������� �ݱ�
            lid.DORotate(closedRotation, duration).SetEase(motionEase);
        }
        else
        {
            // ���ڰ� ���������� ����
            lid.DORotate(openRotation, duration).SetEase(motionEase);
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
