using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;  // ���� ���Կ� ����ִ� ������
    public Image icon; // UI���� �������� ������

    void UpdateSlot()
    {
        if (item != null)
        {
            icon.sprite = item.itemImage; // �������� ������ �������� ����
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;  // �������� ������ �������� ����
            icon.enabled = false;
        }
    }
}
