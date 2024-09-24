using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemDataExample
{
    public string id;
    public string uniqueId;
    public int quantity;     // 아이템의 수량

    public ItemDataExample (ItemDataExample itemDataExample)
    {
        this.id = itemDataExample.id;
        this.uniqueId = itemDataExample.uniqueId;
        this.quantity = itemDataExample.quantity;
    }

    public ItemDataExample (string id, string uniqueId)
    {
        this.id = id;
        this.uniqueId = uniqueId;
        this.quantity = quantity;
    }

    public ItemDataExample() { }
}

public class ItemExample : MonoBehaviour
{
    [SerializeField] ItemDataExample itemDataExample;
}
