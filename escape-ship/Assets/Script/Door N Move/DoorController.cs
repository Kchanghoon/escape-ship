using UnityEngine;
using TMPro;  // TextMeshPro ���ӽ����̽� �߰�
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    public Transform doorLeft;
    public Transform doorRight;
    public Transform player;  // �÷��̾��� Transform�� �߰��Ͽ� �Ÿ� ���

    public float leftStartPosZ;
    public float rightStartPosZ;
    public float endPosZ = 3f;  // ���� ���� �� �̵��� �Ÿ�
    public float duration = 1f;  // ���� ������ �� �ɸ��� �ð�
    public float interactionDistance = 5f;  // �÷��̾�� �� ������ ��ȣ�ۿ� �Ÿ�
    public Ease motionEase = Ease.OutQuad;

    public TextMeshProUGUI statusText;  // ���¸� ǥ���� TMP �ؽ�Ʈ
    private bool isDoorOpen = false;  // ���� ���� �ִ��� ���θ� �����ϴ� ����
    private bool isAnimating = false;  // �ִϸ��̼��� ���� ������ Ȯ���ϴ� �÷���
    private bool isMouseOverDoor = false;  // ���콺�� �� ���� �ִ��� ����

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
        // �÷��̾�� ���� �Ÿ��� ��ȣ�ۿ� �Ÿ� ���� �ִ��� ���
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // �÷��̾ ��ȣ�ۿ� �Ÿ� �ȿ� �ְ�, ���콺�� �� ���� ���� ���� ���� ������ ǥ��
        if (distanceToPlayer <= interactionDistance && isMouseOverDoor)
        {
            UpdateStatusText();
        }
        else
        {
            statusText.gameObject.SetActive(false);  // ���� �����
        }
    }

    // KeyManager���� Play �׼��� ȣ��� �� ���� ����/�ݱ� ó��
    public void OnPlay()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= interactionDistance && isMouseOverDoor && !isAnimating)
        {
            // ��� ī��Ű(ID = 2)�� ��� �־�߸� ���� �� �� ����
            var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
            if (selectedItem != null && (selectedItem.id == "5" || selectedItem.id == "6" || selectedItem.id == "7" || selectedItem.id == "8"))
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
                statusText.text = "���� ī�尡 �ʿ��մϴ�.";
            }
        }
    }

    // ���� �ؽ�Ʈ�� ������Ʈ�ϴ� �Լ�
    private void UpdateStatusText()
    {
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
        if (selectedItem == null || (selectedItem.id != "5" && selectedItem.id != "6" && selectedItem.id != "7" && selectedItem.id != "8"))
        {
            statusText.text = "�ּ� 3�� ���� ī�尡 �ʿ��մϴ�.";  // ���� ī�尡 ���� ��
        }
        else
        {
            statusText.text = "EŰ�� ���� ���� ���ݾ��ּ���.";  // ���� ī�尡 ���� ��
        }
        statusText.gameObject.SetActive(true);  // �ؽ�Ʈ ǥ��
    }

    // ���콺�� �� ���� ���� �� ȣ��
    private void OnMouseEnter()
    {
        isMouseOverDoor = true;
    }

    // ���콺�� ������ ����� �� ȣ��
    private void OnMouseExit()
    {
        isMouseOverDoor = false;
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
