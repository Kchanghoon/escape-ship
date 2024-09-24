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

    public TextMeshProUGUI OpenText;  // "EŰ�� ���� ���� ���ÿ�" TMP �ؽ�Ʈ�� ���� ����
    public TextMeshProUGUI CloseText;  // "EŰ�� ���� ���� ���ÿ�" TMP �ؽ�Ʈ�� ���� ����
    private bool isDoorOpen = false;  // ���� ���� �ִ��� ���θ� �����ϴ� ����
    private bool isAnimating = false;  // �ִϸ��̼��� ���� ������ Ȯ���ϴ� �÷���
    private bool playerInRange = false;  // �÷��̾ ���� �ȿ� �ִ��� Ȯ���ϴ� �÷���

    void Start()
    {
        // ���� ��ġ ����
        leftStartPosZ = doorLeft.localPosition.z;
        rightStartPosZ = doorRight.localPosition.z;

        // ���� �� �ؽ�Ʈ �����
        OpenText.gameObject.SetActive(false);  // TMP �ؽ�Ʈ ����
        CloseText.gameObject.SetActive(false);
        // KeyManager���� Play �׼��� ���
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;
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
            }
            else
            {
                Debug.Log("��� ī��Ű�� ��� �־�� ���� �� �� �ֽ��ϴ�.");
            }
        }
    }

    // �÷��̾ Ʈ���� ���� �ȿ� ������ ��
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // �÷��̾�� 'Player' �±װ� �پ� �ִٰ� ����
        {
            playerInRange = true;  // �÷��̾ ���� �ȿ� ������ ���
            if (isDoorOpen == false)
            {
                OpenText.gameObject.SetActive(true);  // TMP �ؽ�Ʈ ǥ��
                CloseText.gameObject .SetActive(false);
            }
            else
            {
                CloseText.gameObject.SetActive(true);
                OpenText.gameObject.SetActive(false);
            }
        }
    }

    // �÷��̾ Ʈ���� ������ ����� ��
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;  // �÷��̾ ������ ������� ���
            OpenText.gameObject.SetActive(false);  // TMP �ؽ�Ʈ ����
            CloseText.gameObject.SetActive(false);
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
