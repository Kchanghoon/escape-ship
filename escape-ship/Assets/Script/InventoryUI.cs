using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Player player;             // �÷��̾��� �κ��丮�� ����
    public GameObject inventoryPanel; // �κ��丮 �г�
    public GameObject slotPrefab;     // �κ��丮 ���� ������

    private bool isInventoryVisible = false;  // �κ��丮 �г��� ���� ����

    void Update()
    {
        // 'Tab' Ű �Է� ����
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInventoryVisible = !isInventoryVisible;  // ���� ����
            inventoryPanel.SetActive(isInventoryVisible);  // �г� Ȱ��ȭ/��Ȱ��ȭ ��ȯ

            if (isInventoryVisible)
            {
                UpdateInventoryUI();  // �г��� Ȱ��ȭ�� ���� �κ��丮 UI ������Ʈ
            }
        }
    }

    void UpdateInventoryUI()
    {
        if (inventoryPanel == null || slotPrefab == null || player == null || player.inventory == null)
        {
            Debug.LogError("Inventory UI is not properly set up.");
            return;
        }

        // ���� �κ��丮 �г��� ��� ���� ����
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // �κ��丮�� �ִ� ��� �����ۿ� ���� ���� ����
        foreach (Item item in player.inventory)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryPanel.transform);

            Transform iconTransform = slot.transform.Find("Icon");
            if (iconTransform == null)
            {
                Debug.LogError("Icon object not found in slot prefab!");
                continue;
            }

            Image icon = iconTransform.GetComponent<Image>();
            if (icon == null)
            {
                Debug.LogError("Image component not found on Icon object!");
                continue;
            }

            icon.sprite = item.icon;  // ������ ������ ����
        }
    }
}
