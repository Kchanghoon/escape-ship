using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;      // 아이템의 이름
    public string itemImagePath; // 이미지 경로 (JSON으로 저장할 때 사용)
    public Sprite itemImage;     // 인벤토리 UI에서 사용할 아이콘 (런타임 시 로드)

    // 아이템을 사용할 때 호출되는 메서드
    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
        // 아이템 사용 시 발생할 동작 구현 (예: 체력 회복, 무기 장착 등)
    }

    // 아이템 이미지를 로드하는 메서드 (JSON 불러오기 후 사용)
    public void LoadImage()
    {
        if (!string.IsNullOrEmpty(itemImagePath))
        {
            itemImage = Resources.Load<Sprite>(itemImagePath);
        }
    }
}
