using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;      // �������� �̸�
    public string itemImagePath; // �̹��� ��� (JSON���� ������ �� ���)
    public Sprite itemImage;     // �κ��丮 UI���� ����� ������ (��Ÿ�� �� �ε�)

    // �������� ����� �� ȣ��Ǵ� �޼���
    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
        // ������ ��� �� �߻��� ���� ���� (��: ü�� ȸ��, ���� ���� ��)
    }

    // ������ �̹����� �ε��ϴ� �޼��� (JSON �ҷ����� �� ���)
    public void LoadImage()
    {
        if (!string.IsNullOrEmpty(itemImagePath))
        {
            itemImage = Resources.Load<Sprite>(itemImagePath);
        }
    }
}
