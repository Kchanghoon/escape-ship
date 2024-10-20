using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour  where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null) // 1
            {
                instance = (T)FindObjectOfType(typeof(T)); // 2

                //if(instance == null) // 3
                //{
                //    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                //    instance = obj.GetComponent<T>();
                //}
            }
            return instance;
        }
    }
}
