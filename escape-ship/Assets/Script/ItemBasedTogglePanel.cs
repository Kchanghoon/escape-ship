using UnityEngine;

public class ItemBasedTogglePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;  // �г� ������Ʈ
    [SerializeField] private string requiredItemId = "specialItem";  // �� ������Ʈ�� �ʿ��� ������ ID
    [SerializeField] private Canvas ToggleCanvas;  // �г��� Canvas
    private bool isPanelActive = false;  // �г��� ���� �ִ� �������� ���
    private int originalSortingOrder;  // Canvas�� ���� sortingOrder ����
    [SerializeField] int sortingCanvas;

    private void Start()
    {
        // Canvas�� ���� sortingOrder ����
        originalSortingOrder = ToggleCanvas.sortingOrder;

        // Ű �̺�Ʈ�� �г� ��� ����
        KeyManager.Instance.keyDic[KeyAction.Panel] += TryTogglePanel;

        // ���� �� �г��� ��Ȱ��ȭ
        if (panel != null)
        {
            panel.SetActive(false);
            Debug.Log("�г� �ʱ� ����: ��Ȱ��ȭ");
        }
        else
        {
            Debug.LogWarning("�г��� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    // ���õ� �������� �ش� ������Ʈ�� �г��� �� �� �ִ��� Ȯ���ϴ� �Լ�
    private void TryTogglePanel()
    {
        // �κ��丮���� ���õ� ������ ��������
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();

        // ���õ� �������� �����ϰ�, �ش� ������Ʈ�� �䱸�ϴ� ���������� Ȯ��
        if (selectedItem != null && selectedItem.id == requiredItemId)
        {
            TogglePanel();  // �г� ���
        }
        else
        {
            Debug.Log($"�ʿ��� �������� ���õ��� �ʾҽ��ϴ�. �ʿ��� ������ ID: {requiredItemId}");
        }
    }

    // �г��� ����ϴ� �Լ�
    private void TogglePanel()
    {
        if (panel != null)
        {
            isPanelActive = !isPanelActive;  // �г� ���¸� ����

            if (isPanelActive)
            {
                ToggleCanvas.sortingOrder = sortingCanvas;  // �ֻ��� �켱������ ����
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                ToggleCanvas.sortingOrder = originalSortingOrder;  // ���� �켱������ ����
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            panel.SetActive(isPanelActive);  // �г��� Ȱ��ȭ/��Ȱ��ȭ
            Debug.Log($"�г� ���°� ����Ǿ����ϴ�. ���� ����: {(isPanelActive ? "Ȱ��ȭ" : "��Ȱ��ȭ")}");
        }
        else
        {
            Debug.LogWarning("�г��� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}
