using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CheckChestOpen : MonoBehaviour
{
    [SerializeField] private Transform lid;  // ������ �Ѳ� (������ �κ�)
    [SerializeField] private Vector3 closedPosition = Vector3.zero;  // �������� ���� ���� ��ġ��
    [SerializeField] private float openPositionX = 1f;  // ������ ���� X�� ��ġ��
    [SerializeField] private float duration = 1f;  // ������ �ӵ�
    [SerializeField] private Ease motionEase = Ease.InOutQuad;  // �ִϸ��̼� Ease
    [SerializeField] private float autoCloseDelay = 5f;  // ���� �ð� �� �ڵ����� ������ �ð�

    [SerializeField] private TextMeshProUGUI statusText;  // ���� �޽����� ����� TextMeshPro ����
    [SerializeField] private float interactionDistance = 3f;  // ��ȣ�ۿ� ���� �Ÿ�

    private bool isOpen = false;  // ���ڰ� ���ȴ��� ���θ� ���
    private bool isMouseOverChest = false;  // ���콺�� ���� ���� �ִ��� ����
    private bool hasUsedBattery = false;  // ���͸��� ����Ͽ� ���ڸ� ó�� �������� Ȯ���ϴ� �÷���
    private Transform playerTransform;  // �÷��̾��� Transform�� ����
    private Coroutine autoCloseCoroutine;  // �ڵ����� ������ �ڷ�ƾ�� ����

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
            // ���� "7"�� ī�尡 �ִ��� Ȯ��
            var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
            if (selectedItem != null && selectedItem.id == "7")
            {
                UpdateStatusText();
                ToggleChest();
            }
            else
            {
                // "7"�� ī�尡 ���� ���
                Debug.Log("ī�尡 �ʿ��մϴ�.");
                statusText.text = "ī�尡 �ʿ��մϴ�. (ID = 7)";
            }
        }
    }

    // ���� �޽����� ������Ʈ�ϴ� �Լ�
    private void UpdateStatusText()
    {
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();

        if (selectedItem != null) // selectedItem�� null���� ���� Ȯ��
        {
            if (selectedItem.id == "7")
            {
                statusText.text = "���ڸ� �����ּ���.";
            }
            else
            {
                statusText.text = "3�� ����ī��Ű�� �ʿ��մϴ�.";
            }
        }
        else
        {
            // selectedItem�� null�� �� ó��
            statusText.text = "3�� ����ī��Ű�� �ʿ��մϴ�.";
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
            if (autoCloseCoroutine != null)
            {
                StopCoroutine(autoCloseCoroutine);  // �ڵ� ���� �ڷ�ƾ ���
            }
        }
        else
        {
            // ���ڰ� ���������� ���� (X������ �̵�)
            lid.DOLocalMoveX(openPositionX, duration).SetEase(motionEase);
            statusText.gameObject.SetActive(false);  // ���ڰ� ������ �ؽ�Ʈ ��Ȱ��ȭ

            // ���� �ð��� ������ ���ڸ� �ڵ����� ����
            if (autoCloseCoroutine != null)
            {
                StopCoroutine(autoCloseCoroutine);  // ���� �ڷ�ƾ ���
            }
            autoCloseCoroutine = StartCoroutine(AutoCloseChest());  // ���ο� �ڷ�ƾ ����
        }

        isOpen = !isOpen;  // ���¸� ������Ŵ
    }

    // ���� �ð��� ������ ���ڸ� �ݴ� �ڷ�ƾ
    private IEnumerator AutoCloseChest()
    {
        yield return new WaitForSeconds(autoCloseDelay);  // ������ �ð���ŭ ���
        if (isOpen)  // ���ڰ� ���� ���� ���� ����
        {
            ToggleChest();  // ���ڸ� ����
        }
    }
}
