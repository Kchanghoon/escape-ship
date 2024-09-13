[System.Serializable]
public class InventorySlot
{
    public Item item; // ���Կ� ���� ������
    public int count; // �ش� �������� ����

    public InventorySlot(Item _item, int _count)
    {
        item = _item;
        count = _count;
    }

    // ������ ������� Ȯ���ϴ� �޼���
    public bool IsEmpty()
    {
        return item == null;
    }
}
