using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    public List<Item> inventory = new List<Item>(); // 인벤토리 리스트

    public void PickUpItem(Item item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item); // 인벤토리에 아이템 추가
            Debug.Log("Picked up: " + item.itemName);
        }
    }
}
