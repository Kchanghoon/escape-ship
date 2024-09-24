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
    private bool canPickUp = false; // �������� ���� �� �ִ��� ���θ� �����ϴ� ����
    private ItemDataExample nearbyItem; // �÷��̾ ������ �ִ� �������� ����

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


    //���� �ڵ� 
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
            Debug.LogError($"ID {id}�� �ش��ϴ� ������ �����͸� ã�� �� �����ϴ�.");
            return null;
        }
        return new ItemDataExample(foundItem); // ���⿡�� ���纻�� ��ȯ
    }

    // ������ �Ⱦ�
    public void OnPickUp()
    {
        if (canPickUp && nearbyItem != null)
        {
            Debug.Log($"������ {nearbyItem.id}��(��) �Ⱦ��߽��ϴ�.");
            AddItem(nearbyItem.id); // �κ��丮�� ������ �߰�
            nearbyItem = null; // �������� �Ⱦ��� �� �ʱ�ȭ
            canPickUp = false; // �ٽ� �������� �Ⱦ��� �� ������ �ʱ�ȭ
        }
    }

    // �÷��̾ �����ۿ� ��������� ȣ��Ǵ� �޼���
    public void SetCanPickUp(ItemDataExample itemData)
    {
        nearbyItem = itemData;
        canPickUp = true; // �������� ���� �� �ִ� ���·� ����
    }

    // �÷��̾ �����ۿ��� �־����� ȣ��Ǵ� �޼���
    public void ClearCanPickUp()
    {
        nearbyItem = null;
        canPickUp = false; // �������� ���� �� ���� ���·� ����
    }
    // �������� ���� �� �ִ� �������� Ȯ���ϴ� �޼���
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
    //    OnAddItem?.Invoke(itemDataExample); //�κ��丮�� ������ �߰� �̺�Ʈ �߻�
    //}
    public void AddItem(string id)
    {
        var existingItem = curItemDatas.Find(x => x.id == id);

        if (existingItem != null)
        {
            // �̹� �κ��丮�� �ִ� �������� ��� ������ ����
            existingItem.quantity++;
            Debug.Log($"������ {existingItem.id}�� ������ {existingItem.quantity}�� �����߽��ϴ�.");
        }
        else
        {
            // ���ο� �������� �߰�
            var itemDataExample = GetItemDBData(id);
            if (itemDataExample == null) return;  // ������ �����Ͱ� ������ �߰����� ����
            itemDataExample.uniqueId = System.Guid.NewGuid().ToString();
            itemDataExample.quantity = 1;  // ���� �߰��� �������� ������ 1�� ����
            curItemDatas.Add(itemDataExample);

            OnAddItem?.Invoke(itemDataExample);  // �κ��丮 UI ���� �̺�Ʈ
            Debug.Log($"������ {itemDataExample.id}��(��) �κ��丮�� �߰��Ǿ����ϴ�.");
        }

        // UI ���� (�κ��丮 ���� ����)
        InventoryUIExmaple.Instance.UpdateInventoryUI();
    }
    //{
    //    var itemDataExample = GetItemDBData(id);
    //    if (itemDataExample == null) return; // �������� ������ �߰����� ����
    //    itemDataExample.uniqueId = Guid.NewGuid().ToString();
    //    curItemDatas.Add(itemDataExample);
    //    OnAddItem?.Invoke(itemDataExample); // �κ��丮�� ������ �߰� �̺�Ʈ �߻�
    //}

    // ������ ���� ���� �޼���
    public void DecreaseItemQuantity(string id)
    {
        var item = curItemDatas.Find(x => x.id == id);

        if (item != null)
        {
            item.quantity--;

            if (item.quantity <= 0)
            {
                // ������ 0�� �Ǹ� �������� �κ��丮���� ����
                RemoveItem(item.uniqueId);
            }
            else
            {
                // ������ �پ��� �� UI ������Ʈ
                InventoryUIExmaple.Instance.UpdateInventoryUI();
            }
        }
    }


}
