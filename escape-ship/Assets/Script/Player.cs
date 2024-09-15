using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    public List<Item> inventory = new List<Item>(); // �κ��丮 ����Ʈ

    public void PickUpItem(Item item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item); // �κ��丮�� ������ �߰�
            Debug.Log("Picked up: " + item.itemName);
        }
    }
}
