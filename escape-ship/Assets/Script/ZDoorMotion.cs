using UnityEngine;
using TMPro;  // TextMeshPro ���ӽ����̽� �߰�
using DG.Tweening;

public class ZDoorMotion : MonoBehaviour
{
    public Transform door;  // ���Ϸ� �����̴� ��
    public float startPosY;  // �� ���� ��ġ Y
    public float endPosY = 3f;  // ���� ���� �� �̵��� �Ÿ�
    public float duration = 1f;  // ���� ������ �� �ɸ��� �ð�
    public Ease motionEase = Ease.OutQuad;  // �ִϸ��̼� �ӵ� ����

    public TextMeshProUGUI actionText;  // TMP �ؽ�Ʈ ǥ��
    private bool isDoorOpen = false;  // ���� ���� �ִ��� ���θ� �����ϴ� ����
    private bool isAnimating = false;  // �ִϸ��̼��� ���� ������ Ȯ���ϴ� �÷���
    private bool playerInRange = false;  // �÷��̾ ���� �ȿ� �ִ��� Ȯ���ϴ� �÷���

    void Start()
    {
        // ���� ��ġ ����
        startPosY = door.localPosition.y;

        // ���� �� �ؽ�Ʈ �����
        actionText.gameObject.SetActive(false);
    }

    void Update()
    {
        // EŰ�� ������ �� �ִϸ��̼� ���� (�÷��̾ ���� �ȿ� �ְ� �ִϸ��̼��� ���� ������ ���� ��쿡��)
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isAnimating)
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
    }

    // �÷��̾ Ʈ���� ���� �ȿ� ������ ��
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // �÷��̾�� 'Player' �±װ� �پ� �ִٰ� ����
        {
            playerInRange = true;  // �÷��̾ ���� �ȿ� ������ ���
            UpdateActionText();
            actionText.gameObject.SetActive(true);  // TMP �ؽ�Ʈ ǥ��
        }
    }

    // �÷��̾ Ʈ���� ������ ����� ��
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;  // �÷��̾ ������ ������� ���
            actionText.gameObject.SetActive(false);  // TMP �ؽ�Ʈ ����
        }
    }

    // ���� ���� �Լ�
    void OpenDoor()
    {
        isAnimating = true;  // �ִϸ��̼��� ���� ������ ���
        door.DOLocalMoveY(startPosY + endPosY, duration)  // ���� ���� �̵�
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // �ִϸ��̼��� �������� ���
                isDoorOpen = true;  // ���� ���� ���·� ���
                UpdateActionText();
            });
    }

    // ���� �ݴ� �Լ�
    void CloseDoor()
    {
        isAnimating = true;  // �ִϸ��̼��� ���� ������ ���
        door.DOLocalMoveY(startPosY, duration)  // ���� ���� ��ġ�� �̵�
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // �ִϸ��̼��� �������� ���
                isDoorOpen = false;  // ���� ���� ���·� ���
                UpdateActionText();
            });
    }

    // �ؽ�Ʈ ������Ʈ
    void UpdateActionText()
    {
        if (isDoorOpen)
        {
            actionText.text = "EŰ�� ���� ���� �����ÿ�";
        }
        else
        {
            actionText.text = "EŰ�� ���� ���� ���ÿ�";
        }
    }
}
