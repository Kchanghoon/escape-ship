using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; // 인벤토리 참조
    public List<InventorySlotUI> slotUIs; // UI 슬롯 목록

    // UI를 업데이트하는 메서드
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
        UpdateInventoryUI(); // 매 프레임마다 UI 업데이트
    }
}
