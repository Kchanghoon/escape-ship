using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public Inventory inventory;

    // �������� ����ϴ� �޼���
    public void UseItem(Item item)
    {
        // ������ ��� ���� ����
        Debug.Log(item.itemName + " ���");

        inventory.RemoveItem(item); // ��� �� ������ ����
    }
}
