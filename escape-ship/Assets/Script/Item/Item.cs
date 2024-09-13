using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName; // ������ �̸�
    public string description; // ������ ����
    public Sprite icon; // ������ ������ (UI���� ������ �̹���)
    public int id; // ������ ID
    public bool isStackable; // ������ ��ø ���� ����
}
