using TMPro;
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

    public TextMeshProUGUI statusText;  // ���¸� ǥ���� TMP �ؽ�Ʈ

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
        // ���� �� �ؽ�Ʈ �����
        statusText.gameObject.SetActive(false);  // TMP �ؽ�Ʈ ����

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
            var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
            if (selectedItem != null && selectedItem.id == "3")
            {
                TogglePanel();
            }
            else
            {
                statusText.gameObject.SetActive(true);
                statusText.text = "���갡 �ʿ��մϴ�.";  // ��� ī�尡 ���� ��
            }
        }
        else
        {
            Debug.Log("�÷��̾ �ʹ� �ְų� ���콺�� ������Ʈ ���� ���� �ʽ��ϴ�.");

            statusText.gameObject.SetActive(false);
        }
    }

    //// ���� �ؽ�Ʈ�� ������Ʈ�ϴ� �Լ�
    //private void UpdateStatusText()
    //{
    //    var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
    //    if (selectedItem == null || selectedItem.id != "3")
    //    {
    //        statusText.text = "���갡 �ʿ��մϴ�.";  // ��� ī�尡 ���� ��
    //    }
    //    else
    //    {
    //        statusText.text = "Ȱ��ȭ �غ� �Ϸ�.";  // ��� ī�尡 ���� ��
    //    }
    //    statusText.gameObject.SetActive(true);  // �ؽ�Ʈ ǥ��
    //}



    // �г��� ����ϴ� �Լ�
    private void TogglePanel()
    {
        if (panel != null)
        {
            isPanelActive = !isPanelActive;  // �г��� Ȱ��ȭ ���¸� ����
            panel.SetActive(isPanelActive);  // �г��� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
            PipeManager.Instance.RandomPipe();

            if (isPanelActive)
            {
                // �г��� Ȱ��ȭ�� �� Canvas�� �켱������ ����
                if (panelCanvas != null)
                {
                    panelCanvas.sortingOrder = 999;  // �켱������ �ֻ����� ����
                    MouseCam mouseCam = FindObjectOfType<MouseCam>();
                    if (mouseCam != null)
                    {
                        mouseCam.SetCursorState(false);  // Ŀ�� ��� ����
                    }
                }
            }
            else
            {
                // �г��� ��Ȱ��ȭ�� �� ���� �켱������ ����
                if (panelCanvas != null)
                {
                    panelCanvas.sortingOrder = originalSortingOrder;  // ���� ������ ����
                    MouseCam mouseCam = FindObjectOfType<MouseCam>();
                    if (mouseCam != null)
                    {
                        mouseCam.SetCursorState(true);  // Ŀ�� ���
                    }
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
