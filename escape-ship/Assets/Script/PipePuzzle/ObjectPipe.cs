using UnityEngine;

public class ObjectPipe : MonoBehaviour
{
    [SerializeField] private GameObject panel;  // ����� �г�
    [SerializeField] private Transform player;  // �÷��̾��� Transform
    [SerializeField] private float interactionDistance = 3f;  // �÷��̾�� ������Ʈ ���� ��ȣ�ۿ� �Ÿ�
    [SerializeField] private Canvas panelCanvas;  // �г��� Canvas (�켱���� ������ ���� �ʿ�)
    private int originalSortingOrder;  // ���� Canvas�� sortingOrder
    private bool isPanelActive = false;  // �г��� ���� Ȱ��ȭ�Ǿ� �ִ��� ����
    private bool isMouseOverObject = false;  // ���콺�� ������Ʈ ���� �ִ��� ����

    void Start()
    {
        // ���� �� �г��� ��Ȱ��ȭ
        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("�г��� �Ҵ���� �ʾҽ��ϴ�.");
        }

        if (panelCanvas != null)
        {
            originalSortingOrder = panelCanvas.sortingOrder;  // Canvas�� ���� �켱���� ����
        }
        else
        {
            Debug.LogWarning("panelCanvas�� �Ҵ���� �ʾҽ��ϴ�.");
        }

        KeyManager.Instance.keyDic[KeyAction.Play] += TryTogglePanel;
    }

    // ���콺�� ������Ʈ ���� ���� �� ȣ��Ǵ� �Լ�
    private void OnMouseEnter()
    {
        isMouseOverObject = true;
    }

    // ���콺�� ������Ʈ���� ����� �� ȣ��Ǵ� �Լ�
    private void OnMouseExit()
    {
        isMouseOverObject = false;
    }

    // �г��� ����� �� �ִ��� Ȯ���ϰ� �г��� ���
    private void TryTogglePanel()
    {
        if (IsPlayerInRange() && isMouseOverObject)
        {
            TogglePanel();
        }
        else
        {
            Debug.Log("�÷��̾ �ʹ� �ְų� ���콺�� ������Ʈ ���� ���� �ʽ��ϴ�.");
        }
    }

    // �г��� ����ϴ� �Լ�
    private void TogglePanel()
    {
        if (panel != null)
        {
            isPanelActive = !isPanelActive;  // �г��� Ȱ��ȭ ���¸� ����
            panel.SetActive(isPanelActive);  // �г��� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ

            if (isPanelActive)
            {
                // �г��� Ȱ��ȭ�� �� Canvas�� �켱������ ����
                if (panelCanvas != null)
                {
                    panelCanvas.sortingOrder = 999;  // �켱������ �ֻ����� ����
                }
            }
            else
            {
                // �г��� ��Ȱ��ȭ�� �� ���� �켱������ ����
                if (panelCanvas != null)
                {
                    panelCanvas.sortingOrder = originalSortingOrder;  // ���� ������ ����
                }
            }

            Debug.Log($"�г� ���°� ����Ǿ����ϴ�: {(isPanelActive ? "Ȱ��ȭ��" : "��Ȱ��ȭ��")}");
        }
        else
        {
            Debug.LogWarning("�г��� ����Ǿ� ���� �ʽ��ϴ�.");
        }
    }

    // �÷��̾ ���� �Ÿ� ���� �ִ��� Ȯ���ϴ� �Լ�
    private bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        return distanceToPlayer <= interactionDistance;
    }
}
