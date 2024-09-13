using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; // �κ��丮 ����
    public List<InventorySlotUI> slotUIs; // UI ���� ���

    // UI�� ������Ʈ�ϴ� �޼���
    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            InventorySlot slot = inventory.slots[i];
            slotUIs[i].UpdateSlotUI(slot.item, slot.count);
        }
    }

    void Update()
    {
        UpdateInventoryUI(); // �� �����Ӹ��� UI ������Ʈ
    }
}
