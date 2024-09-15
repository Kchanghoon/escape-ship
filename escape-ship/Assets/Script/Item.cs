using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // �������� �̸�
    public Sprite icon;     // �κ��丮 UI���� ����� ������

    // ���� �޼��� ���� (�ʿ� �� ����Ŭ�������� ����)
    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
        // ������ ��� �� �߻��� ���� ���� (��: ü�� ȸ��, ���� ���� ��)
    }
}
