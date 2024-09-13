using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots; // �κ��丮 ���� ���
    public int maxSlots; // �κ��丮 �ִ� ���� ����

    // �κ��丮 �ʱ�ȭ
    void Start()
    {
        slots = new List<InventorySlot>();
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot(null, 0)); // �� ���� �߰�
        }
    }

    // �������� �κ��丮�� �߰��ϴ� �޼���
    public bool AddItem(Item item, int count = 1)
    {
        // ��ø ������ �������� ���
        if (item.isStackable)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == item) // �̹� �ش� �������� ������
                {
                    slot.count += count; // ������ ���� ����
                    return true;
                }
            }
        }

        // �� ������ ã�Ƽ� �߰�
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.item = item;
                slot.count = count;
                return true;
            }
        }

        // �κ��丮�� ���� ���� �߰� �Ұ�
        return false;
    }

    // �������� �κ��丮���� �����ϴ� �޼���
    public bool RemoveItem(Item item, int count = 1)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item)
            {
                slot.count -= count;
                if (slot.count <= 0)
                {
                    slot.item = null;
                    slot.count = 0;
                }
                return true;
            }
        }
        return false;
    }
}
