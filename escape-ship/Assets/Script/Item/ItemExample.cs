using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemDataExample
{
    public string id;
    public string uniqueId;

    public ItemDataExample (ItemDataExample itemDataExample)
    {
        this.id = itemDataExample.id;
        this.uniqueId = itemDataExample.uniqueId;
    }

    public ItemDataExample (string id, string uniqueId)
    {
        this.id = id;
        this.uniqueId = uniqueId;
    }

    public ItemDataExample() { }
}

public class ItemExample : MonoBehaviour
{
    [SerializeField] ItemDataExample itemDataExample;
}
