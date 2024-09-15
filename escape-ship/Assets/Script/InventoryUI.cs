using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Player player;             // 플레이어의 인벤토리를 참조
    public GameObject inventoryPanel; // 인벤토리 패널
    public GameObject slotPrefab;     // 인벤토리 슬롯 프리팹

    private bool isInventoryVisible = false;  // 인벤토리 패널의 현재 상태

    void Update()
    {
        // 'Tab' 키 입력 감지
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInventoryVisible = !isInventoryVisible;  // 상태 반전
            inventoryPanel.SetActive(isInventoryVisible);  // 패널 활성화/비활성화 전환

            if (isInventoryVisible)
            {
                UpdateInventoryUI();  // 패널이 활성화될 때만 인벤토리 UI 업데이트
            }
        }
    }

    void UpdateInventoryUI()
    {
        if (inventoryPanel == null || slotPrefab == null || player == null || player.inventory == null)
        {
            Debug.LogError("Inventory UI is not properly set up.");
            return;
        }

        // 현재 인벤토리 패널의 모든 슬롯 삭제
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // 인벤토리에 있는 모든 아이템에 대해 슬롯 생성
        foreach (Item item in player.inventory)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryPanel.transform);

            Transform iconTransform = slot.transform.Find("Icon");
            if (iconTransform == null)
            {
                Debug.LogError("Icon object not found in slot prefab!");
                continue;
            }

            Image icon = iconTransform.GetComponent<Image>();
            if (icon == null)
            {
                Debug.LogError("Image component not found on Icon object!");
                continue;
            }

            icon.sprite = item.icon;  // 아이템 아이콘 설정
        }
    }
}
