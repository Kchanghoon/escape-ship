using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] items;  // �������� �迭�� ����

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();

        // ���� �迭�� ũ�⸦ slotParent�� �ڽ� ������ ����
        items = new Item[slots.Length];
    }
#endif

    void Awake()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        // ������ ������Ʈ�ϴ� �κ�
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
    }

    public void AddItem(Item _item)
    {
        // �� ������ ã�� �� �������� �߰�
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = _item;
                FreshSlot();
                return; // ������ �߰� �� ����
            }
        }

        // ��� ������ ���� �� ���
        print("������ ���� �� �ֽ��ϴ�.");
    }
}
