using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;  // 현재 슬롯에 들어있는 아이템
    public Image icon; // UI에서 아이템의 아이콘

    void UpdateSlot()
    {
        if (item != null)
        {
            icon.sprite = item.itemImage; // 아이템이 있으면 아이콘을 설정
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;  // 아이템이 없으면 아이콘을 제거
            icon.enabled = false;
        }
    }
}
