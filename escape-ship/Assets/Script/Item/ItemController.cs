using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ItemController : Singleton<ItemController>
{
    // 아이템 이벤트 정의 (아이템이 추가되거나 삭제될 때 사용)
    public delegate void ItemEvent(ItemDataExample itemData);
    public event ItemEvent OnAddItem;  // 아이템 추가 시 발생하는 이벤트
    public event ItemEvent OnRemoveItem;  // 아이템 삭제 시 발생하는 이벤트

    [SerializeField] List<ItemDataExample> allItemDatas;  // 모든 아이템 데이터를 저장하는 리스트
    public List<ItemDataExample> curItemDatas;  // 현재 보유하고 있는 아이템 데이터를 저장하는 리스트
    private bool canPickUp = false;  // 아이템을 획득할 수 있는지 여부를 저장하는 플래그
    private ItemDataExample nearbyItem;  // 플레이어가 가까이에 있는 아이템 정보

    private void Start()
    {
        // PickUp 및 Drop 키 이벤트 연결
        //KeyManager.Instance.keyDic[KeyAction.PickUp] += OnPickUp;
        KeyManager.Instance.keyDic[KeyAction.Drop] += OnDrop;
    }

    public string testAddId;  // 테스트용 아이템 ID

    // 아이템을 고유 ID로 삭제하는 메서드
    public void RemoveItem(string uniqueId)
    {
        var item = curItemDatas.Find(x => x.uniqueId == uniqueId);  // 고유 ID로 아이템 찾기
        if (item != null)
        {
            OnRemoveItem?.Invoke(item);  // 아이템 삭제 이벤트 호출
            curItemDatas.Remove(item);  // 현재 아이템 리스트에서 삭제
        }
    }

    // 아이템을 일반 ID로 삭제하는 메서드
    public void RemoveItemById(string id)
    {
        var item = curItemDatas.Find(x => x.id == id);  // ID로 아이템 찾기
        if (item != null)
        {
            OnRemoveItem?.Invoke(item);  // 아이템 삭제 이벤트 호출
            curItemDatas.Remove(item);  // 현재 아이템 리스트에서 삭제
        }
    }

    // 아이템 데이터를 데이터베이스에서 가져오는 메서드
    public ItemDataExample GetItemDBData(string id)
    {
        var foundItem = allItemDatas.Find(x => x.id == id);  // ID로 아이템 데이터베이스에서 아이템 찾기
        if (foundItem == null)
        {
            Debug.LogError($"ID {id}에 해당하는 아이템 데이터를 찾을 수 없습니다.");  // 아이템이 없을 때 오류 출력
            return null;
        }
        return new ItemDataExample(foundItem);  // 아이템 데이터를 복사하여 반환
    }

    // 아이템을 획득하는 메서드
    //public void OnPickUp()
    //{
    //    if (canPickUp && nearbyItem != null)  // 획득 가능한 상태인지 확인
    //    {
    //        Debug.Log($"아이템 {nearbyItem.id}을(를) 획득했습니다 OnPickUP.");
    //        AddItem(nearbyItem.id);  // 인벤토리에 아이템 추가
    //        nearbyItem = null;  // 가까운 아이템 정보 초기화
    //        canPickUp = false;  // 아이템 획득 가능 상태 초기화

    //        // UI 업데이트 호출
    //        InventoryUIExmaple.Instance.UpdateInventoryUI();
    //    }
    //}

    // 아이템을 버리는 메서드
    public void OnDrop()
    {
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();  // 선택된 아이템 가져오기

        if (selectedItem != null)
        {
            DropItem(selectedItem.id);  // 선택된 아이템을 드랍
        }
        else
        {
            Debug.Log("선택된 아이템이 없습니다.");  // 선택된 아이템이 없을 때 메시지 출력
        }
    }

    // 아이템을 드랍하는 메서드
    public void DropItem(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        // 플레이어의 위치에서 아이템 드랍
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 dropPosition = playerTransform.position;  // 플레이어의 현재 위치에서 드랍

        // 드랍할 아이템의 프리팹을 가져옴
        var item = ResourceDB.Instance.GetItemResource(id).itemObject;
        if (item != null)
        {
            Item droppedItem = Instantiate(item.gameObject, dropPosition, Quaternion.identity).GetComponent<Item>();  // 아이템 생성
            droppedItem.itemData = curItemDatas.Find(x => x.id == id);  // 드랍된 아이템에 데이터 할당
            Debug.Log($"아이템 {id}을(를) 드랍했습니다.");

            RemoveItemById(id);  // 인벤토리에서 아이템 제거
            InventoryUIExmaple.Instance.UpdateInventoryUI();  // 인벤토리 UI 업데이트
        }
        else
        {
            Debug.LogError("아이템 프리팹을 찾을 수 없습니다.");  // 아이템 프리팹이 없을 때 오류 출력
        }
    }

    // 아이템 프리팹을 찾는 메서드
    //private GameObject GetItemPrefab(string id)
    //{
    //    // ResourceDB에서 아이템의 리소스를 가져옴
    //    var itemResource = ResourceDB.Instance.GetItemResource(id);
    //    if (itemResource != null)
    //    {
    //        return itemResource.object3D;  // 3D 오브젝트 반환
    //    }
    //    return null;
    //}

    // 플레이어가 아이템과 가까워졌을 때 호출되는 메서드
    public void SetCanPickUp(ItemDataExample itemData)
    {
        nearbyItem = itemData;  // 가까운 아이템 정보 저장
        canPickUp = true;  // 아이템 획득 가능 상태로 전환
        Debug.Log($"아이템 {nearbyItem.id}을(를) 획득할 수 있습니다.");
    }

    // 플레이어가 아이템에서 멀어졌을 때 호출되는 메서드
    public void ClearCanPickUp()
    {
        nearbyItem = null;  // 가까운 아이템 정보 초기화
        canPickUp = false;  // 아이템 획득 가능 상태 초기화
    }

    // 아이템을 획득할 수 있는지 여부를 반환하는 메서드
    public bool CanPickUp()
    {
        return canPickUp;
    }

    // 인벤토리에 아이템을 추가하는 메서드
    public void AddItem(string id)
    {
        var existingItem = curItemDatas.Find(x => x.id == id);  // 이미 존재하는 아이템인지 확인

        if (existingItem != null)
        {
            existingItem.quantity++;  // 이미 존재하면 수량 증가
            Debug.Log($"아이템 {existingItem.id}의 수량이 {existingItem.quantity}로 증가했습니다.");
        }
        else
        {
            var itemDataExample = GetItemDBData(id);  // 새로운 아이템 추가
            if (itemDataExample == null) return;  // 아이템 데이터가 없을 경우 반환
            itemDataExample.uniqueId = System.Guid.NewGuid().ToString();  // 고유 ID 부여
            itemDataExample.quantity = 1;  // 수량을 1로 설정
            curItemDatas.Add(itemDataExample);  // 인벤토리에 추가

            OnAddItem?.Invoke(itemDataExample);  // 아이템 추가 이벤트 호출
            Debug.Log($"아이템 {itemDataExample.id}이(가) 인벤토리에 추가되었습니다.");
        }

        // 인벤토리 UI 업데이트
        InventoryUIExmaple.Instance.UpdateInventoryUI();
    }

    // 아이템의 수량을 감소시키는 메서드
    public void DecreaseItemQuantity(string id)
    {
        var item = curItemDatas.Find(x => x.id == id);  // 아이템 찾기

        // ID가 "1", "2", "3"일 때만 감소 처리
        if (id != "1" && id != "2" && id != "3")
        {
            Debug.Log($"아이템 {id}은(는) 수량 감소 대상이 아닙니다.");
            return;
        }

        if (item != null)
        {
            item.quantity--;  // 수량 감소

            if (item.quantity <= 0)
            {
                RemoveItem(item.uniqueId);  // 수량이 0이 되면 아이템 삭제
            }
            else
            {
                InventoryUIExmaple.Instance.UpdateInventoryUI();  // UI 업데이트
            }

            Debug.Log($"아이템 {id}의 수량이 감소했습니다. 남은 수량: {item.quantity}");
        }
    }public void DeleteItemQuantity(string id)
    {
        var item = curItemDatas.Find(x => x.id == id);  // 아이템 찾기

        // ID가 "1", "2", "3"일 때만 감소 처리
        if (id != "5" && id != "6" && id != "7")
        {
            Debug.Log($"아이템 {id}은(는) 수량 감소 대상이 아닙니다.");
            return;
        }

        if (item != null)
        {
            item.quantity--;  // 수량 감소

            if (item.quantity <= 0)
            {
                RemoveItem(item.uniqueId);  // 수량이 0이 되면 아이템 삭제
            }
            else
            {
                InventoryUIExmaple.Instance.UpdateInventoryUI();  // UI 업데이트
            }

            Debug.Log($"아이템 {id}의 수량이 감소했습니다. 남은 수량: {item.quantity}");
        }
    }
}
