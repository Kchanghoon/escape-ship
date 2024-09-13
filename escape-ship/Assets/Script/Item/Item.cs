using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName; // 아이템 이름
    public string description; // 아이템 설명
    public Sprite icon; // 아이템 아이콘 (UI에서 보여줄 이미지)
    public int id; // 아이템 ID
    public bool isStackable; // 아이템 중첩 가능 여부
}
