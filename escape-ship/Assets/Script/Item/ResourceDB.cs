using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDB : Singleton<ResourceDB>
{
    [SerializeField] List<ResourceData> itemResourceData;

    public ResourceData GetItemResource(string id)
    {
        return itemResourceData.Find(x => x.id == id);
    }
}
