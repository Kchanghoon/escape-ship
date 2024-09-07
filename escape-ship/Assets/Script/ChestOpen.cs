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

    void Start()
    {
        lid.localRotation = Quaternion.Euler(closedRotation);  // ó���� ���� ���·� ����
    }

    void Update()
    {
        // E Ű�� ������ �� ���ڸ� ���ݴ� ���
        if (Input.GetKeyDown(KeyCode.E))
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
}
