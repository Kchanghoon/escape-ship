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
        KeyManager.Instance.keyDic[KeyAction.Drop] += OnDrop;

    }
    public string testAddId;

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
            Debug.Log($"������ {nearbyItem.id}��(��) �Ⱦ��߽��ϴ� OnPickUP.");
            AddItem(nearbyItem.id); // �κ��丮�� ������ �߰�
            nearbyItem = null; // �������� �Ⱦ��� �� �ʱ�ȭ
            canPickUp = false; // �ٽ� �������� �Ⱦ��� �� ������ �ʱ�ȭ
                               // UI ������Ʈ ȣ��
            InventoryUIExmaple.Instance.UpdateInventoryUI();
        }
    }

    public void OnDrop()
    {
        // �κ��丮���� ���õ� ������ ��������
        var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();

        if (selectedItem != null)
        {
            // ���õ� �������� �÷��̾��� �տ� ���
            DropItem(selectedItem.id);
        }
        else
        {
            Debug.Log("���õ� �������� �����ϴ�.");
        }
    }


    public void DropItem(string id)
    {
        // �÷��̾� ��ġ �� ���� ����
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //Vector3 dropPosition = playerTransform.position + playerTransform.forward * 1.5f;  // �÷��̾� ���ʿ� ���
        Vector3 dropPosition = playerTransform.position;  // �÷��̾� ��ġ�� ���

        // ����� ������ �������� ������ ����
        GameObject itemPrefab = GetItemPrefab(id);  // �������� �̸� ����س��Ҵٰ� ����
        if (itemPrefab != null)
        {
            GameObject droppedItem = Instantiate(itemPrefab, dropPosition, Quaternion.identity);
            droppedItem.GetComponent<Item>().itemData = curItemDatas.Find(x => x.id == id);  // ������ ������ ����
            Debug.Log($"������ {id}��(��) ���������ϴ�.");

            // �κ��丮���� ������ ����
            RemoveItemById(id);
            InventoryUIExmaple.Instance.UpdateInventoryUI();  // �κ��丮 ����
        }
        else
        {
            Debug.LogError("������ �������� �������� �ʾҽ��ϴ�.");
        }
    
}

    // ������ �������� ã�� �޼��� (�������� ���ҽ��� ����Ǿ� �ִٰ� ����)
    private GameObject GetItemPrefab(string id)
    {
        // ResourceDB���� id�� �ش��ϴ� �������� ã�� ����
        var itemResource = ResourceDB.Instance.GetItemResource(id);
        if (itemResource != null)
        {
            return itemResource.object3D;  // 3D ������Ʈ�� ���
        }
        return null;
    }

    // �÷��̾ �����ۿ� ��������� ȣ��Ǵ� �޼���
    public void SetCanPickUp(ItemDataExample itemData)
    {
        nearbyItem = itemData;
        canPickUp = true; // �������� ���� �� �ִ� ���·� ����
        Debug.Log($"������ {nearbyItem.id}��(��) �Ⱦ� ������ �����Դϴ�.");
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


    // ������ ���� ���� �޼���
    public void DecreaseItemQuantity(string id)
    {
        var item = curItemDatas.Find(x => x.id == id);

        // �Ҹ���� �ʴ� �������� ���⼭ �ٷ� ����
        if (id == "2")
        {
            Debug.Log($"������ {id}��(��) �Ҹ���� �ʽ��ϴ�.");
            return;
        }

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
