using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // �� GameObject�� ������ �ִ� Item ������

    void OnTriggerEnter(Collider other)
    {
        // ���� �浹�� ��ü�� Player���
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && item != null)
            {
                player.PickUpItem(item); // �÷��̾ �������� ���� ���
                Destroy(gameObject); // ������ GameObject ����
            }
        }
    }
}
