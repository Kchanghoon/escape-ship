using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // 아이템의 이름
    public Sprite icon;     // 인벤토리 UI에서 사용할 아이콘

    // 가상 메서드 정의 (필요 시 서브클래스에서 구현)
    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
        // 아이템 사용 시 발생할 동작 구현 (예: 체력 회복, 무기 장착 등)
    }
}
