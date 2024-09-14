using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public Inventory inventory;

    // 아이템을 사용하는 메서드
    public void UseItem(Item item)
    {
        switch (item.itemName)
        {
            case "1Floor Card":
                inventory.RemoveItem(item); // 사용 후 아이템 제거
                break;

            default:
                Debug.Log(item.itemName + " 사용 불가");
                break;
        }
    }

    private void DoorCard(Item item)
    {
        
        Debug.Log("1층 카드 사용: " + item.itemName);
    }
}
