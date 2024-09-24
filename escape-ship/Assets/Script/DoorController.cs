using UnityEngine;
using TMPro;  // TextMeshPro ���ӽ����̽� �߰�
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    public Transform doorLeft;
    public Transform doorRight;

    public float leftStartPosZ;
    public float rightStartPosZ;
    public float endPosZ = 3f;  // ���� ���� �� �̵��� �Ÿ�
    public float duration = 1f;  // ���� ������ �� �ɸ��� �ð�
    public Ease motionEase = Ease.OutQuad;

    public TextMeshProUGUI statusText;  // ���¸� ǥ���� TMP �ؽ�Ʈ
    private bool isDoorOpen = false;  // ���� ���� �ִ��� ���θ� �����ϴ� ����
    private bool isAnimating = false;  // �ִϸ��̼��� ���� ������ Ȯ���ϴ� �÷���
    private bool playerInRange = false;  // �÷��̾ ���� �ȿ� �ִ��� Ȯ���ϴ� �÷���

    void Start()
    {
        // ���� ��ġ ����
        leftStartPosZ = doorLeft.localPosition.z;
        rightStartPosZ = doorRight.localPosition.z;

        // ���� �� �ؽ�Ʈ �����
        statusText.gameObject.SetActive(false);  // TMP �ؽ�Ʈ ����

        // KeyManager���� Play �׼��� ���
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;
    }

    void Update()
    {
        // �÷��̾ ���� �ȿ� �ִ� ���� ������ ���¿� ���� ������ ������Ʈ
        if (playerInRange)
        {
            UpdateStatusText();
        }
    }

    // KeyManager���� Play �׼��� ȣ��� �� ���� ����/�ݱ� ó��
    public void OnPlay()
    {
        if (playerInRange && !isAnimating)
        {
            // ��� ī��Ű(ID = 2)�� ��� �־�߸� ���� �� �� ����
            var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
            if (selectedItem != null && selectedItem.id == "2")
            {
                if (isDoorOpen)
                {
                    CloseDoor();  // ���� ���� ������ ����
                }
                else
                {
                    OpenDoor();  // ���� ���� ������ ��
                }
                statusText.text = "EŰ�� ���� ���� ���ݾ��ּ���.";  // �� ���¿� ���� �ؽ�Ʈ ����
            }
            else
            {
                Debug.Log("��� ī��Ű�� ��� �־�� ���� �� �� �ֽ��ϴ�.");
                statusText.text = "��� ī�尡 �ʿ��մϴ�.";
            }
        }
    }

    // ���� �ؽ�Ʈ�� ������Ʈ�ϴ� �Լ�
    private void UpdateStatusText()
    {
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
        if (selectedItem == null || selectedItem.id != "2")
        {
            statusText.text = "��� ī�尡 �ʿ��մϴ�.";  // ��� ī�尡 ���� ��
        }
        else
        {
            statusText.text = "EŰ�� ���� ���� ���ݾ��ּ���.";  // ��� ī�尡 ���� ��
        }
        statusText.gameObject.SetActive(true);  // �ؽ�Ʈ ǥ��
    }

    // �÷��̾ Ʈ���� ���� �ȿ� ������ ��
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // �÷��̾�� 'Player' �±װ� �پ� �ִٰ� ����
        {
            playerInRange = true;  // �÷��̾ ���� �ȿ� ������ ���
            UpdateStatusText();  // ������ �����ڸ��� ���� ���� ������Ʈ
        }
    }

    // �÷��̾ Ʈ���� ������ ����� ��
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;  // �÷��̾ ������ ������� ���
            statusText.gameObject.SetActive(false);  // TMP �ؽ�Ʈ ����
        }
    }

    // ���� ���� �Լ�
    void OpenDoor()
    {
        isAnimating = true;  // �ִϸ��̼��� ���� ������ ���
        DOTween.Sequence()
            .Append(doorLeft.DOLocalMoveZ(leftStartPosZ + endPosZ, duration))
            .Join(doorRight.DOLocalMoveZ(rightStartPosZ - endPosZ, duration))
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // �ִϸ��̼��� �������� ���
                isDoorOpen = true;  // ���� ���� ���·� ���
            });
    }

    // ���� �ݴ� �Լ�
    void CloseDoor()
    {
        isAnimating = true;  // �ִϸ��̼��� ���� ������ ���
        DOTween.Sequence()
            .Append(doorLeft.DOLocalMoveZ(leftStartPosZ, duration))
            .Join(doorRight.DOLocalMoveZ(rightStartPosZ, duration))
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // �ִϸ��̼��� �������� ���
                isDoorOpen = false;  // ���� ���� ���·� ���
            });
    }
}
