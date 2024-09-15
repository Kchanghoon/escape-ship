using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // 이 GameObject가 가지고 있는 Item 데이터

    void OnTriggerEnter(Collider other)
    {
        // 만약 충돌한 객체가 Player라면
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && item != null)
            {
                player.PickUpItem(item); // 플레이어가 아이템을 집어 들기
                Destroy(gameObject); // 아이템 GameObject 삭제
            }
        }
    }
}
