using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] ItemDataExample itemData;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] UnityEngine.UI.Image itemIcon;
    [SerializeField] UnityEngine.UI.Image itembackground;

    public ItemDataExample ItemData { get => itemData; }

    public void Init(ItemDataExample itemData)
    {
        this.itemData = itemData;
        bool isActive = itemData != null && !string.IsNullOrEmpty(itemData.id);
        SetActive(isActive);

        if (!isActive) return;
        itemIcon.sprite = ResourceDB.Instance.GetItemResource(itemData.id).sprite;
    }

    public void SetActive(bool isActive)
    {
        canvasGroup.alpha = isActive ? 1 : 0;
        canvasGroup.interactable = isActive;
        canvasGroup.blocksRaycasts = isActive;
    }

    /// <summary>
    /// item icon alpha값 변경
    /// </summary>
    /// <param name="alpha"></param>
    public void ItemIconFade(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    /// <summary>
    /// 아이템 드래그 시작 할 때
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDragItem()
    {
        if (itemData == null) return;
        if (InventoryUIExmaple.Instance.IsDrag) return;
        InventoryUIExmaple.Instance.SetDragItem(this);
    }

    /// <summary>
    /// 아이템 드래그 끝났을 때
    /// </summary>
    public void OnDropItem()
    {
        if (itemData == null) return;
        InventoryUIExmaple.Instance.OnDropIcon();
    }

    // 슬롯 선택 시 호출 (선택 시 배경 활성화)
    public void Select()
    {
        itembackground.gameObject.SetActive(true); // 선택 시 배경 활성화
    }

    // 슬롯 선택 해제 시 호출 (선택 해제 시 배경 비활성화)
    public void Deselect()
    {
        itembackground.gameObject.SetActive(false); // 선택 해제 시 배경 비활성화
    }
}
