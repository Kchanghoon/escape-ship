using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public InventorySlotUI slotUI;
    private Transform originalParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root); // �巡���� �� ������ UI�� �ֻ������
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // �巡�� �� ���콺 ��ġ ����ٴ�
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent); // �巡�� ������ ���� ��ġ�� ���ư�
    }

    public void OnDrop(PointerEventData eventData)
    {
        SlotDragDrop droppedSlot = eventData.pointerDrag.GetComponent<SlotDragDrop>();
        if (droppedSlot != null)
        {
            // ���� �� ������ ��ȯ ���� ����
            SwapItems(slotUI, droppedSlot.slotUI);
        }
    }

    private void SwapItems(InventorySlotUI slotA, InventorySlotUI slotB)
    {
        // �� ������ �������� ��ȯ�ϴ� ����
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
