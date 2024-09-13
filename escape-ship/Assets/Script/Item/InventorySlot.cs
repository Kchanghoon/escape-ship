[System.Serializable]
public class InventorySlot
{
    public Item item; // 슬롯에 들어가는 아이템
    public int count; // 해당 아이템의 개수

    public InventorySlot(Item _item, int _count)
    {
        item = _item;
        count = _count;
    }

    // 슬롯이 비었는지 확인하는 메서드
    public bool IsEmpty()
    {
        return item == null;
    }
}
