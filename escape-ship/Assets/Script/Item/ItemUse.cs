using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public Inventory inventory;

    // 아이템을 사용하는 메서드
    public void UseItem(Item item)
    {
        // 아이템 사용 로직 구현
        Debug.Log(item.itemName + " 사용");

        inventory.RemoveItem(item); // 사용 후 아이템 제거
    }
}
