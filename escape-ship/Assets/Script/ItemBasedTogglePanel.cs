using UnityEngine;

public class ItemBasedTogglePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;  // �г� ������Ʈ
    [SerializeField] private string requiredItemId = "specialItem";  // �г� ����� ���� �ʿ��� ������ ID
    [SerializeField] private ItemController itemController;  // ������ ���� ��Ʈ�ѷ�
    [SerializeField] private Canvas ToggleCanvas;  // �г��� Canvas
    private bool isPanelActive = false;  // �г��� ���� �ִ� �������� ���
    private int originalSortingOrder;  // Canvas�� ���� sortingOrder ����
    [SerializeField] int sortingCanvas;

    private void Start()
    {
        // Canvas�� ���� sortingOrder ����
        originalSortingOrder = ToggleCanvas.sortingOrder;

        KeyManager.Instance.keyDic[KeyAction.Panel] += TogglePanel;

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

        if (itemController == null)
        {
            Debug.LogWarning("ItemController�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    // �г��� ����ϴ� �Լ�
    private void TogglePanel()
    {
        // �������� �ִ��� Ȯ��
        if (HasRequiredItem())
        {
            if (panel != null)
            {
                isPanelActive = !isPanelActive;  // �г� ���¸� ����

                // �г��� Ȱ��ȭ�Ǹ� Canvas�� �켱������ ����
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
        else
        {
            Debug.Log("�ʿ��� �������� ���� �г��� �� �� �����ϴ�.");
        }
    }

    // �÷��̾� �κ��丮�� �ش� �������� �ִ��� Ȯ���ϴ� �Լ�
    private bool HasRequiredItem()
    {
        if (itemController != null)
        {
            foreach (var item in itemController.curItemDatas)
            {
                Debug.Log($"�κ��丮 ������ Ȯ�� ��: {item.id}");
                if (item.id == requiredItemId)
                {
                    Debug.Log("�ʿ��� �������� �κ��丮�� �ֽ��ϴ�.");
                    return true;
                }
            }
        }
        else
        {
            Debug.LogWarning("ItemController�� �Ҵ���� �ʾҽ��ϴ�.");
        }
        Debug.Log("�ʿ��� �������� �κ��丮�� �����ϴ�.");
        return false;
    }
}
