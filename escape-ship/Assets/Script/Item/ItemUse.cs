using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public Inventory inventory;

    // �������� ����ϴ� �޼���
    public void UseItem(Item item)
    {
        switch (item.itemName)
        {
            case "1Floor Card":
                inventory.RemoveItem(item); // ��� �� ������ ����
                break;

            default:
                Debug.Log(item.itemName + " ��� �Ұ�");
                break;
        }
    }

    private void DoorCard(Item item)
    {
        
        Debug.Log("1�� ī�� ���: " + item.itemName);
    }
}
