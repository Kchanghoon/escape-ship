using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemDataExample itemData; // ������ ������

    // �÷��̾ ������ ���� �ȿ� ������ ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemController.Instance.SetCanPickUp(itemData); // �������� ���� �� �ִ� ���·� ��ȯ
        }
    }

    // �÷��̾ ������ �������� ������ ȣ��Ǵ� �޼���
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemController.Instance.ClearCanPickUp(); // �������� ���� �� ���� ���·� ��ȯ
        }
    }
}
