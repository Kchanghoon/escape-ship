using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots; // 인벤토리 슬롯 목록
    public int maxSlots; // 인벤토리 최대 슬롯 개수

    // 인벤토리 초기화
    void Start()
    {
        slots = new List<InventorySlot>();
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot(null, 0)); // 빈 슬롯 추가
        }
    }

    // 아이템을 인벤토리에 추가하는 메서드
    public bool AddItem(Item item, int count = 1)
    {
        // 중첩 가능한 아이템인 경우
        if (item.isStackable)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == item) // 이미 해당 아이템이 있으면
                {
                    slot.count += count; // 아이템 개수 증가
                    return true;
                }
            }
        }

        // 빈 슬롯을 찾아서 추가
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.item = item;
                slot.count = count;
                return true;
            }
        }

        // 인벤토리가 가득 차면 추가 불가
        return false;
    }

    // 아이템을 인벤토리에서 제거하는 메서드
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
