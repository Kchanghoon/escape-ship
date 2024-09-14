using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventorySaveLoad : MonoBehaviour
{
    public Inventory inventory;

    public void SaveInventory()
    {
        InventoryData data = new InventoryData(inventory);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/inventory.json", json);
        Debug.Log("인벤토리 저장 완료");
    }

    public void LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);
            foreach (ItemData itemData in data.items)
            {
                Item item = GetItemByName(itemData.itemName); // 아이템을 이름으로 찾기
                inventory.AddItem(item, itemData.count);
            }
            Debug.Log("인벤토리 로드 완료");
        }
    }

    private Item GetItemByName(string itemName)
    {
        // 아이템 목록에서 이름으로 아이템을 찾아 반환하는 메서드
        return allItems.Find(item => item.itemName == itemName);
    }


}

[System.Serializable]
public class InventoryData
{
    public List<ItemData> items = new List<ItemData>();

    public InventoryData(Inventory inventory)
    {
        foreach (InventorySlot slot in inventory.slots)
        {
            if (!slot.IsEmpty())
            {
                items.Add(new ItemData(slot.item.itemName, slot.count));
            }
        }
    }
}

[System.Serializable]
public class ItemData
{
    public string itemName;
    public int count;

    public ItemData(string _itemName, int _count)
    {
        itemName = _itemName;
        count = _count;
    }
}
