using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static KeyManager;

public enum KeyAction
{
    Jump,
    Run,
    Setting,
    Inventory,
    PickUp
}

[Serializable]
public class KeySet
{
    public KeyAction keyAction;
    public KeyCode keyCode;

    public KeySet(KeyAction keyAction, KeyCode keyCode)
    {
        this.keyAction = keyAction;
        this.keyCode = keyCode;
    }
}

public class KeyManager : Singleton<KeyManager>
{
    [SerializeField] List<KeySet> inputDownKeySets;
    [SerializeField] List<KeySet> inputKeySets;

    List<KeySet> allKeySets
    {
        get => inputDownKeySets.Concat(inputKeySets).ToList();
    }
     
    public delegate void KeyEvent();
    private event KeyEvent OnJump;
    private event KeyEvent OnRun;
    private event KeyEvent OnSetting;
    private event KeyEvent OnInventory;
    private event KeyEvent OnPickUp;

    public Dictionary<KeyAction, KeyEvent> keyDic = new Dictionary<KeyAction, KeyEvent>();

    private void Awake()
    {
        keyDic.Add(KeyAction.Jump, OnJump);
        keyDic.Add(KeyAction.Run, OnRun);
        keyDic.Add(KeyAction.Setting, OnSetting);
        keyDic.Add(KeyAction.Inventory, OnInventory);
        keyDic.Add(KeyAction.PickUp, OnPickUp);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (var key in allKeySets)
            {
                if (Input.GetKeyDown(key.keyCode))
                {
                    keyDic[key.keyAction]?.Invoke();
                }
            }
        }

        // Key Up Ã³¸®
        foreach (var key in inputKeySets)
        {
            if (Input.GetKeyUp(key.keyCode))
            {
                keyDic[key.keyAction]?.Invoke();
            }
        }
    }
}
