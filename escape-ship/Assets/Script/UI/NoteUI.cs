using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    [SerializeField] private GameObject item9Panel;
    [SerializeField] private GameObject item10Panel;
    Dictionary<string, GameObject> itemPanelDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        itemPanelDic.Add("9", item9Panel);
        itemPanelDic.Add("10", item10Panel);
    }

    private void Start()
    {
        // Ű �̺�Ʈ�� �г� ��� �޼��带 ����
        KeyManager.Instance.keyDic[KeyAction.Panel] += TryTogglePanel;
    }

    private void OnDestroy()
    {
        if (KeyManager.Instance != null) KeyManager.Instance.keyDic[KeyAction.Panel] -= TryTogglePanel;
    }

    // ���õ� �������� �г��� �� �� �ִ��� Ȯ���ϴ� �޼���
    private void TryTogglePanel()
    {
        // �κ��丮���� ���õ� ������ ��������
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();
        if (selectedItem == null) return;
        if (!itemPanelDic.ContainsKey(selectedItem.id)) return; // id ���� ��� return

        // ���õ� �������� ������, �� �������� �г��� �� �� �ִ� ���������� Ȯ��
        TogglePanel(itemPanelDic[selectedItem.id]);
    }

    // �г��� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �޼���
    private void TogglePanel(GameObject panel)
    {
        if (panel == null) return;
        bool isActive = !panel.gameObject.activeInHierarchy;  // �г��� Ȱ��ȭ ���¸� ���
        MouseCam.Instance.SetCursorState(!isActive);  // MouseCam ��ũ��Ʈ�� ���� Ŀ�� ���� ����
        panel.SetActive(isActive);  // �г��� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
    }
}
