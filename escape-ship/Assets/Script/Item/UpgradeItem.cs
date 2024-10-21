using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;  // DoTween ���ӽ����̽� �߰�

public class UpgradeItem : MonoBehaviour
{
    [SerializeField] private Transform player;  // �÷��̾��� Transform
    [SerializeField] private float interactDistance = 3f;  // ��ȣ�ۿ� ������ �Ÿ�
    private bool isMouseOverItem = false;  // ���콺�� ������Ʈ ���� �ִ��� ���θ� ����
    [SerializeField] private TextMeshProUGUI statusText;  // ���� ���¸� ǥ���ϴ� TextMeshPro �ؽ�Ʈ
    [SerializeField] private float fadeDuration = 1f;  // �ؽ�Ʈ�� ������ ������� �ð�

    void Start()
    {
        // ���� �ؽ�Ʈ ��Ȱ��ȭ
        statusText.gameObject.SetActive(false);

        KeyManager.Instance.keyDic[KeyAction.Play] += TryUpgrade;  // KeyManager���� Play Ű �̺�Ʈ ���
    }

    void Update()
    {
        // �÷��̾�� ������Ʈ ������ �Ÿ��� ���
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // �÷��̾ ��ȣ�ۿ� ������ �Ÿ� ���� �ְ�, ���콺�� ������ ���� �ִ� ���
        if (distanceToPlayer <= interactDistance && isMouseOverItem)
        {
            UpdateStatusText();  // ������ ���¸� Ȯ���Ͽ� �ؽ�Ʈ ������Ʈ
        }
        else
        {
            HideTextWithAnimation();  // �ؽ�Ʈ�� ������� ��
        }
    }

    // ���콺�� ������Ʈ ���� ���� �� ȣ��Ǵ� �޼���
    private void OnMouseEnter()
    {
        isMouseOverItem = true;  // ���콺�� ������Ʈ ���� ����
    }

    // ���콺�� ������Ʈ���� ��� �� ȣ��Ǵ� �޼���
    private void OnMouseExit()
    {
        isMouseOverItem = false;  // ���콺�� ������Ʈ ���� ����
    }

    // �ؽ�Ʈ�� ������ ��Ÿ���� �޼���
    private void ShowTextWithAnimation()
    {
        statusText.gameObject.SetActive(true);  // �ؽ�Ʈ Ȱ��ȭ
        statusText.DOFade(1, fadeDuration);  // ���� 1�� ������ ���� (�ִϸ��̼����� �ؽ�Ʈ ��Ÿ����)
    }

    // �ؽ�Ʈ�� ������ ������� �ϴ� �޼���
    private void HideTextWithAnimation()
    {
        statusText.DOFade(0, fadeDuration).OnComplete(() => statusText.gameObject.SetActive(false));  // ���� 0���� ������ ���� �� ��Ȱ��ȭ
    }

    // ������ ���¸� Ȯ���ϰ� �ؽ�Ʈ�� ������Ʈ�ϴ� �޼���
    private void UpdateStatusText()
    {
        // ItemController���� ���� ���� ���� ������ Ȯ��
        var itemController = ItemController.Instance;

        // �κ��丮���� 11���� 5�� �������� �ִ��� Ȯ��
        var item11 = itemController.curItemDatas.Find(x => x.id == "11");
        var item5 = itemController.curItemDatas.Find(x => x.id == "5");

        if (item11 != null && item5 != null)
        {
            // 11���� 5�� �������� ������ ���׷��̵� ���� �޽���
            statusText.text = "ī�� ���׷��̵尡 �����մϴ�.";
        }
        else
        {
            // �������� ������ ���׷��̵� �Ұ��� �޽���
            statusText.text = "1�� ����ī��� ��ũ�� �ʿ��մϴ�.";
        }

        // �ؽ�Ʈ�� ������ ��Ÿ����
        ShowTextWithAnimation();
    }

    // ���� TryUpgrade �޼��� �״�� ����
    private void TryUpgrade()
    {
        // �÷��̾�� ������Ʈ ������ �Ÿ��� ���
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // ���õ� �������� ������, ���� �Ÿ� ���� �ְ�, ���콺�� ������ ���� �ִ��� Ȯ��
        if (distanceToPlayer <= interactDistance && isMouseOverItem)
        {
            // ItemController���� ���� ���� ���� ������ Ȯ��
            var itemController = ItemController.Instance;

            // �κ��丮���� 11���� 5�� �������� �ִ��� Ȯ��
            var item11 = itemController.curItemDatas.Find(x => x.id == "11");
            var item5 = itemController.curItemDatas.Find(x => x.id == "5");

            if (item11 != null && item5 != null)
            {
                // 11���� 5�� �������� ������ 8�� �������� �߰��ϰ� ���� ������ ����
                itemController.RemoveItemById("11");  // 11�� ������ ����
                itemController.RemoveItemById("5");  // 5�� ������ ����
                itemController.AddItem("8");  // 8�� ������ �߰�
                statusText.text = "���� ī�� �߱޿Ϸ�.";
                ShowTextWithAnimation();
                Debug.Log("������ ���׷��̵� ����! 8�� �������� �߰��Ǿ����ϴ�.");
            }
            else
            {
                ShowTextWithAnimation();
                Debug.Log("���׷��̵忡 �ʿ��� �������� �����ϴ�. 11���� 5���� �ʿ��մϴ�.");
            }
        }
        else
        {
            Debug.Log("���׷��̵带 �� �� �����ϴ�. �÷��̾ ����� ������ ���� �ʰų� ���콺�� �����ۿ� �����ϴ�.");
        }
    }
}
