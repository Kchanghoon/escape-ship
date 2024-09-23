using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemDataExample itemData; // 아이템 데이터

    // 플레이어가 아이템 범위 안에 들어오면 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemController.Instance.SetCanPickUp(itemData); // 아이템을 받을 수 있는 상태로 전환
        }
    }

    // 플레이어가 아이템 범위에서 나가면 호출되는 메서드
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemController.Instance.ClearCanPickUp(); // 아이템을 받을 수 없는 상태로 전환
        }
    }
}
