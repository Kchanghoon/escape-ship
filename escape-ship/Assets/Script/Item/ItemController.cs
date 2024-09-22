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

    public string testAddId;

    [ContextMenu("Test Add Item")]
    public void TestAddItem()
    {
        AddItem(testAddId);
    }

    [ContextMenu("Test Remove Item")]
    public void TestRemoveItem()
    {
        RemoveItemById(testAddId);
    }

    public void AddItem(string id)
    {
        var itemDataExample= GetItemDBData(id);
        itemDataExample.uniqueId = Guid.NewGuid().ToString();
        curItemDatas.Add(itemDataExample);
        OnAddItem?.Invoke(itemDataExample);
    }

    public void RemoveItem(string uniqueId)
    {
        OnRemoveItem?.Invoke((curItemDatas.Find(x => x.uniqueId == uniqueId)));
        curItemDatas.RemoveAll(x => x.uniqueId == uniqueId);
    }

    public void RemoveItemById(string id)
    {
        OnRemoveItem?.Invoke((curItemDatas.Find(x => x.id == id)));
        curItemDatas.RemoveAll(x => x.id == id);
    }

    public ItemDataExample GetItemDBData(string id)
    {
        return new ItemDataExample( allItemDatas.Find(x => x.id == id));
    }
}
