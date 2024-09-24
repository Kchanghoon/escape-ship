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
    PickUp,
    Play,
    SelectItem1,
    SelectItem2,
    SelectItem3,
    SelectItem4,
    SelectItem5,
    SelectItem6,
    SelectItem7,
    SelectItem8,
    SelectItem9,
    SelectItem10,
    Use
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
    private event KeyEvent OnPlay;

    private event KeyEvent OnSelectItem1;
    private event KeyEvent OnSelectItem2;
    private event KeyEvent OnSelectItem3;
    private event KeyEvent OnSelectItem4;
    private event KeyEvent OnSelectItem5;
    private event KeyEvent OnSelectItem6;
    private event KeyEvent OnSelectItem7;
    private event KeyEvent OnSelectItem8;
    private event KeyEvent OnSelectItem9;
    private event KeyEvent OnSelectItem10;

    private event KeyEvent OnUse;

    public Dictionary<KeyAction, KeyEvent> keyDic = new Dictionary<KeyAction, KeyEvent>();

    private void Awake()
    {
        keyDic.Add(KeyAction.Jump, OnJump);
        keyDic.Add(KeyAction.Run, OnRun);
        keyDic.Add(KeyAction.Setting, OnSetting);
        keyDic.Add(KeyAction.Inventory, OnInventory);
        keyDic.Add(KeyAction.PickUp, OnPickUp);
        keyDic.Add(KeyAction.Play, OnPlay);
        keyDic.Add(KeyAction.Use, OnUse);

        keyDic[KeyAction.SelectItem1] = () => InventoryUIExmaple.Instance.selectItem(0);
        keyDic[KeyAction.SelectItem2] = () => InventoryUIExmaple.Instance.selectItem(1);
        keyDic[KeyAction.SelectItem3] = () => InventoryUIExmaple.Instance.selectItem(2);
        keyDic[KeyAction.SelectItem4] = () => InventoryUIExmaple.Instance.selectItem(3);
        keyDic[KeyAction.SelectItem5] = () => InventoryUIExmaple.Instance.selectItem(4);
        keyDic[KeyAction.SelectItem6] = () => InventoryUIExmaple.Instance.selectItem(5);
        keyDic[KeyAction.SelectItem7] = () => InventoryUIExmaple.Instance.selectItem(6);
        keyDic[KeyAction.SelectItem8] = () => InventoryUIExmaple.Instance.selectItem(7);
        keyDic[KeyAction.SelectItem9] = () => InventoryUIExmaple.Instance.selectItem(8);
        keyDic[KeyAction.SelectItem10] = () => InventoryUIExmaple.Instance.selectItem(9);


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
