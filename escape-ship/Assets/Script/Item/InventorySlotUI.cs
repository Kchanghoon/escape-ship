using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon; // 아이템 아이콘
    public Text countText; // 아이템 개수

    public void UpdateSlotUI(Item item, int count)
    {
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            countText.text = count.ToString();
            countText.enabled = true;
        }
        else
        {
            icon.enabled = false;
            countText.enabled = false;
        }
    }
}
