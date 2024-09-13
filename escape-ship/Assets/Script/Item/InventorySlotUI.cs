using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon; // ������ ������
    public Text countText; // ������ ����

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
