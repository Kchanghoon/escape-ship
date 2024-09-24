using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ItemController : Singleton<ItemController>
{
    public delegate void ItemEvent(ItemDataExample itemData);
    public event ItemEvent OnAddItem;
    public event ItemEvent OnRemoveItem;

    [SerializeField] List<ItemDataExample> allItemDatas;
    [SerializeField] List<ItemDataExample> curItemDatas;
    private bool canPickUp = false; // 아이템을 받을 수 있는지 여부를 저장하는 변수
    private ItemDataExample nearbyItem; // 플레이어가 가까이 있는 아이템을 저장

    private void Start()
    {
        KeyManager.Instance.keyDic[KeyAction.PickUp] += OnPickUp;

    }
    public string testAddId;

    //[ContextMenu("Test Add Item")]
    //public void TestAddItem()
    //{
    //    AddItem(testAddId);
    //}

    //[ContextMenu("Test Remove Item")]
    //public void TestRemoveItem()
    //{
    //    RemoveItemById(testAddId);
    //}


    //기존 코드 
    //public void RemoveItem(string uniqueId)
    //{
    //    OnRemoveItem?.Invoke((curItemDatas.Find(x => x.uniqueId == uniqueId)));
    //    curItemDatas.RemoveAll(x => x.uniqueId == uniqueId);
    //}

    //public void RemoveItemById(string id)
    //{
    //    OnRemoveItem?.Invoke((curItemDatas.Find(x => x.id == id)));
    //    curItemDatas.RemoveAll(x => x.id == id);
    //}

    public void RemoveItem(string uniqueId)
    {
        var item = curItemDatas.Find(x => x.uniqueId == uniqueId);
        if (item != null)
        {
            OnRemoveItem?.Invoke(item);
            curItemDatas.Remove(item);
        }
    }

    public void RemoveItemById(string id)
    {
        var item = curItemDatas.Find(x => x.id == id);
        if (item != null)
        {
            OnRemoveItem?.Invoke(item);
            curItemDatas.Remove(item);
        }
    }

    //public ItemDataExample GetItemDBData(string id)
    //{
    //    return new ItemDataExample( allItemDatas.Find(x => x.id == id));
    //}

    public ItemDataExample GetItemDBData(string id)
    {
        var foundItem = allItemDatas.Find(x => x.id == id);
        if (foundItem == null)
        {
            Debug.LogError($"ID {id}에 해당하는 아이템 데이터를 찾을 수 없습니다.");
            return null;
        }
        return new ItemDataExample(foundItem); // 여기에서 복사본을 반환
    }

    // 아이템 픽업
    public void OnPickUp()
    {
        if (canPickUp && nearbyItem != null)
        {
            Debug.Log($"아이템 {nearbyItem.id}을(를) 픽업했습니다.");
            AddItem(nearbyItem.id); // 인벤토리에 아이템 추가
            nearbyItem = null; // 아이템을 픽업한 후 초기화
            canPickUp = false; // 다시 아이템을 픽업할 수 없도록 초기화
        }
    }

    // 플레이어가 아이템에 가까워지면 호출되는 메서드
    public void SetCanPickUp(ItemDataExample itemData)
    {
        nearbyItem = itemData;
        canPickUp = true; // 아이템을 받을 수 있는 상태로 설정
    }

    // 플레이어가 아이템에서 멀어지면 호출되는 메서드
    public void ClearCanPickUp()
    {
        nearbyItem = null;
        canPickUp = false; // 아이템을 받을 수 없는 상태로 설정
    }
    // 아이템을 받을 수 있는 상태인지 확인하는 메서드
    public bool CanPickUp()
    {
        return canPickUp;
    }

    //public ItemDataExample GetItemDBData(string id)
    //{
    //    return new ItemDataExample(allItemDatas.Find(x => x.id == id));
    //}

    //public void AddItem(string id)
    //{
    //    var itemDataExample = GetItemDBData(id);
    //    itemDataExample.uniqueId = Guid.NewGuid().ToString();
    //    curItemDatas.Add(itemDataExample);
    //    OnAddItem?.Invoke(itemDataExample); //인벤토리에 아이템 추가 이벤트 발생
    //}
    public void AddItem(string id)
    {
        var existingItem = curItemDatas.Find(x => x.id == id);

        if (existingItem != null)
        {
            // 이미 인벤토리에 있는 아이템일 경우 수량만 증가
            existingItem.quantity++;
            Debug.Log($"아이템 {existingItem.id}의 수량이 {existingItem.quantity}로 증가했습니다.");
        }
        else
        {
            // 새로운 아이템을 추가
            var itemDataExample = GetItemDBData(id);
            if (itemDataExample == null) return;  // 아이템 데이터가 없으면 추가하지 않음
            itemDataExample.uniqueId = System.Guid.NewGuid().ToString();
            itemDataExample.quantity = 1;  // 새로 추가된 아이템의 수량은 1로 설정
            curItemDatas.Add(itemDataExample);

            OnAddItem?.Invoke(itemDataExample);  // 인벤토리 UI 갱신 이벤트
            Debug.Log($"아이템 {itemDataExample.id}이(가) 인벤토리에 추가되었습니다.");
        }

        // UI 갱신 (인벤토리 슬롯 갱신)
        InventoryUIExmaple.Instance.UpdateInventoryUI();
    }
    //{
    //    var itemDataExample = GetItemDBData(id);
    //    if (itemDataExample == null) return; // 아이템이 없으면 추가하지 않음
    //    itemDataExample.uniqueId = Guid.NewGuid().ToString();
    //    curItemDatas.Add(itemDataExample);
    //    OnAddItem?.Invoke(itemDataExample); // 인벤토리에 아이템 추가 이벤트 발생
    //}

    // 아이템 수량 감소 메서드
    public void DecreaseItemQuantity(string id)
    {
        var item = curItemDatas.Find(x => x.id == id);

        if (item != null)
        {
            item.quantity--;

            if (item.quantity <= 0)
            {
                // 수량이 0이 되면 아이템을 인벤토리에서 제거
                RemoveItem(item.uniqueId);
            }
            else
            {
                // 수량이 줄었을 때 UI 업데이트
                InventoryUIExmaple.Instance.UpdateInventoryUI();
            }
        }
    }


}
