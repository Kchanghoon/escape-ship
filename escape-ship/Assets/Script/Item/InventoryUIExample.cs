using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIExmaple : Singleton<InventoryUIExmaple>
{
    [SerializeField] ItemSlotUI[] itemSlots = new ItemSlotUI[10]; // 인벤토리 슬롯 10개 제한
    ItemDataExample[] itemDatas // 저장할 아이템 데이터
    {
        get => Array.ConvertAll(itemSlots, x => x.ItemData);
    }
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image dragItemImage;

    ItemSlotUI dragItemSlotUI;
    ItemSlotUI targetItemSlotUI;
    bool isDrag;
    public bool IsDrag { get => isDrag; }

    private void Start()
    {
        ItemController.Instance.OnAddItem += OnAddItem;
        ItemController.Instance.OnRemoveItem += OnRemoveItem;
        ResetInventory(); // 테스트용
    }

    /// <summary>
    /// 인벤토리 열기
    /// </summary>
    public void OpenInventory()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    [ContextMenu("Reset")]
    public void ResetInventory()
    {
        dragItemImage.gameObject.SetActive(false);
        SetInventory(new ItemDataExample[10]); //새로운 값 입력
    }

    /// <summary>
    /// 입력한 아이템 데이터에 따라 Slot에 데이터를 넣는다.
    /// </summary>
    /// <param name="SetItemDatas">저장한 데이터 or reset한 데이터</param>
    private void SetInventory(ItemDataExample[] SetItemDatas)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].Init(SetItemDatas[i]);
        }
    }

    /// <summary>
    /// 아이템을 얻는 경우 아이템 슬롯에 넣는다.
    /// </summary>
    /// <param name="itemData">얻은 아이템 data</param>
    public void OnAddItem(ItemDataExample itemData)
    {
        ItemSlotUI itemSlot = GetEmptySlot(); // 비어있는칸 확인
        if (itemSlot == null) return; // 빈 칸 없음

        itemSlot.Init(itemData);
    }

    public void OnRemoveItem(ItemDataExample itemData)
    {
        ItemSlotUI itemSlot = GetItemSlot(itemData.uniqueId);
        if (itemSlot == null) return;
        itemSlot.Init(new ItemDataExample());
    }

    /// <summary>
    /// 인벤토리 빈칸 찾기
    /// </summary>
    /// <returns></returns>
    private ItemSlotUI GetEmptySlot()
    {
        return Array.Find(itemSlots, x => string.IsNullOrEmpty(x.ItemData?.id)); // id가 비어있는 칸
    }

    private ItemSlotUI GetItemSlot(string uniqueId)
    {
        return Array.Find(itemSlots, x => x.ItemData.uniqueId == uniqueId);
    }

    /// <summary>
    /// 아이템 위치 변경
    /// </summary>
    /// <param name="fromItemSlot">이동한 아이템 슬롯</param>
    /// <param name="targetItemSlot">이동 목적지 슬롯</param>
    public void ChangeItemSlot(ItemSlotUI fromItemSlot, ItemSlotUI targetItemSlot)
    {
        var tempFromItemData = fromItemSlot?.ItemData;
        var tempTargetItemData = targetItemSlot?.ItemData;

        // 바뀐 값 넣어주기
        fromItemSlot?.Init(tempTargetItemData);
        targetItemSlot?.Init(tempFromItemData);
    }

    /// <summary>
    /// 아이템 드래그 시작
    /// </summary>
    /// <param name="dragItemSlotUI">드래그하는 아이템 정보</param>
    public void SetDragItem(ItemSlotUI dragItemSlotUI)
    {
        dragItemImage.sprite = ResourceDB.Instance.GetItemResource(dragItemSlotUI.ItemData.id).sprite;
        dragItemImage.transform.position = dragItemSlotUI.transform.position;
        dragItemImage.gameObject.SetActive(true);
        this.dragItemSlotUI = dragItemSlotUI;

        dragItemSlotUI.ItemIconFade(0.5f); // 기존 아이템 반투명하게 하기
        isDrag = true;

        StartCoroutine(DragItem()); // 아이템 드래그 시작
    }

    private IEnumerator DragItem()
    {
        while (isDrag)
        {
            Vector3 pos = Input.mousePosition;
            Vector3 dragPos = pos;

            var eventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            List<RaycastResult> hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, hits);

            // Raycast 결과 처리
            foreach (var result in hits)
            {
                // 클릭한 UI 요소가 ItemSlotUI 스크립트를 가지고 있는지 확인
                targetItemSlotUI = result.gameObject.GetComponent<ItemSlotUI>();
                if (targetItemSlotUI != null)
                {
                    dragPos = targetItemSlotUI.transform.position;
                    break;
                }
            }

            if (hits.Count == 0) targetItemSlotUI = null;

            Vector3 finalPos = Vector3.Lerp(dragItemImage.transform.position, dragPos, 0.5f);
            dragItemImage.transform.position = finalPos; // 마우스 위치 따라 움직임

            yield return null;
        }
    }

    /// <summary>
    /// 아이템 드래그 끝
    /// </summary>
    public void OnDropIcon()
    {
        isDrag = false;
        dragItemImage.gameObject.SetActive(false); // 드래그 이미지 비활성화
        
        // targetItemSlot이 있는경우 변경
        if (targetItemSlotUI == null) dragItemSlotUI.ItemIconFade(1);
        else ChangeItemSlot(dragItemSlotUI, targetItemSlotUI);
    }
}
