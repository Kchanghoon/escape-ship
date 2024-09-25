using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] ItemDataExample itemData;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] UnityEngine.UI.Image itemIcon;
    [SerializeField] UnityEngine.UI.Image itembackground;
    [SerializeField] Text itemQuantityText; // 아이템 수량을 표시하는 UI 텍스트

    public ItemDataExample ItemData { get => itemData; }

    public void Init(ItemDataExample itemData)
    {
        this.itemData = itemData;
        bool isActive = itemData != null && !string.IsNullOrEmpty(itemData.id);
        SetActive(isActive);

        if (!isActive) return;
        itemIcon.sprite = ResourceDB.Instance.GetItemResource(itemData.id).sprite;
        UpdateQuantityUI();
    }

    public void SetActive(bool isActive)
    {
        canvasGroup.alpha = isActive ? 1 : 0;
        canvasGroup.interactable = isActive;
        canvasGroup.blocksRaycasts = isActive;
    }

    public void UpdateQuantityUI()
    {
        if (itemQuantityText == null)
        {
            Debug.LogError("itemQuantityText가 할당되지 않았습니다.");
            return;
        }

        if (itemData != null && itemData.quantity > 1)
        {
            itemQuantityText.text = itemData.quantity.ToString();
            itemQuantityText.gameObject.SetActive(true); // 수량이 1 이상일 때만 활성화
        }
        else
        {
            itemQuantityText.gameObject.SetActive(false); // 수량이 1 이하일 때 숨기기
        }
    }

    /// <summary>
    /// 아이템에 마우스가 올라갔을 때 툴팁 표시
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemData != null)
        {
            TooltipUI.Instance.ShowTooltip(itemData.descriptions);
            TooltipUI.Instance.SetTooltipPosition(Input.mousePosition); // 툴팁 위치 설정
        }
    }


    /// <summary>
    /// 아이템에서 마우스가 나갔을 때 툴팁 숨김
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI.Instance.HideTooltip();
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
