using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public InventorySlotUI slotUI;
    private Transform originalParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root); // 드래그할 때 슬롯을 UI의 최상단으로
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // 드래그 시 마우스 위치 따라다님
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent); // 드래그 끝나면 원래 위치로 돌아감
    }

    public void OnDrop(PointerEventData eventData)
    {
        SlotDragDrop droppedSlot = eventData.pointerDrag.GetComponent<SlotDragDrop>();
        if (droppedSlot != null)
        {
            // 슬롯 간 아이템 교환 로직 구현
            SwapItems(slotUI, droppedSlot.slotUI);
        }
    }

    private void SwapItems(InventorySlotUI slotA, InventorySlotUI slotB)
    {
        // 두 슬롯의 아이템을 교환하는 로직
        Item tempItem = slotA.item;
        int tempCount = slotA.count;

        slotA.item = slotB.item;
        slotA.count = slotB.count;

        slotB.item = tempItem;
        slotB.count = tempCount;

        slotA.UpdateSlotUI(slotA.item, slotA.count);
        slotB.UpdateSlotUI(slotB.item, slotB.count);
    }
}
