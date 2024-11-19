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
    Use,
    Drop,
    Panel,
    Sit,
    Wheel
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
    [SerializeField] private List<KeySet> inputDownKeySets; // KeyDown 이벤트 처리용
    [SerializeField] private List<KeySet> inputKeySets;     // KeyUp 이벤트 처리용

    // 읽기 전용 프로퍼티 추가
    public IReadOnlyList<KeySet> InputDownKeySets => inputDownKeySets;
    public IReadOnlyList<KeySet> InputKeySets => inputKeySets;
    // 모든 KeySet 가져오기 (읽기 전용)
    private List<KeySet> AllKeySets => inputDownKeySets.Concat(inputKeySets).ToList();

    public delegate void KeyEvent();
    private event KeyEvent OnJump;
    private event KeyEvent OnRun;
    private event KeyEvent OnSetting;
    private event KeyEvent OnInventory;
    private event KeyEvent OnPickUp;
    private event KeyEvent OnPlay;
    private event KeyEvent OnSit;
    private event KeyEvent OnPanel;

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
    private event KeyEvent OnDrop;

    public Dictionary<KeyAction, KeyEvent> keyDic = new Dictionary<KeyAction, KeyEvent>();

    private void Awake()
    {
        InitializeKeyDictionary();
    }

    private void InitializeKeyDictionary()
    {
        keyDic.Add(KeyAction.Jump, OnJump);
        keyDic.Add(KeyAction.Run, OnRun);
        keyDic.Add(KeyAction.Setting, OnSetting);
        keyDic.Add(KeyAction.Inventory, OnInventory);
        keyDic.Add(KeyAction.PickUp, OnPickUp);
        keyDic.Add(KeyAction.Play, OnPlay);
        keyDic.Add(KeyAction.Use, OnUse);
        keyDic.Add(KeyAction.Drop, OnDrop);
        keyDic.Add(KeyAction.Sit, OnSit);
        keyDic.Add(KeyAction.Panel, OnPanel);

        keyDic[KeyAction.SelectItem1] = () => InventoryUIExmaple.Instance.SelectItem(0);
        keyDic[KeyAction.SelectItem2] = () => InventoryUIExmaple.Instance.SelectItem(1);
        keyDic[KeyAction.SelectItem3] = () => InventoryUIExmaple.Instance.SelectItem(2);
        keyDic[KeyAction.SelectItem4] = () => InventoryUIExmaple.Instance.SelectItem(3);
        keyDic[KeyAction.SelectItem5] = () => InventoryUIExmaple.Instance.SelectItem(4);
        keyDic[KeyAction.SelectItem6] = () => InventoryUIExmaple.Instance.SelectItem(5);
        keyDic[KeyAction.SelectItem7] = () => InventoryUIExmaple.Instance.SelectItem(6);
        keyDic[KeyAction.SelectItem8] = () => InventoryUIExmaple.Instance.SelectItem(7);
        keyDic[KeyAction.SelectItem9] = () => InventoryUIExmaple.Instance.SelectItem(8);
        keyDic[KeyAction.SelectItem10] = () => InventoryUIExmaple.Instance.SelectItem(9);
    }

    // 키 입력 처리
    private void InputKey(KeyAction keyAction)
    {
        if (GameManager.Instance.isPause && GameManager.Instance.isSetting)
        {
            if (keyAction != KeyAction.Setting) return;
        }

        keyDic[keyAction]?.Invoke();
    }

    // Update 메서드 (기존 키 입력 처리 유지)
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (var key in AllKeySets)
            {
                if (Input.GetKeyDown(key.keyCode))
                {
                    InputKey(key.keyAction);
                }
            }
        }

        // Key Up 처리
        foreach (var key in inputKeySets)
        {
            if (Input.GetKeyUp(key.keyCode))
            {
                InputKey(key.keyAction);
            }
        }
    }

    // **추가**: 키 변경 요청 처리 메서드
    public void UpdateKeyBinding(KeyAction keyAction, KeyCode newKeyCode)
    {
        // KeyAction에 해당하는 KeySet 검색
        var keySet = AllKeySets.FirstOrDefault(k => k.keyAction == keyAction);
        if (keySet != null)
        {
            keySet.keyCode = newKeyCode; // 새로운 키 값으로 설정
            Debug.Log($"Key for {keyAction} updated to {newKeyCode}");
        }
        else
        {
            Debug.LogError($"KeyAction {keyAction} not found!");
        }
    }

    // **추가**: 특정 KeyAction의 KeyCode 가져오기
    public KeyCode GetKeyCode(KeyAction keyAction)
    {
        return AllKeySets.FirstOrDefault(k => k.keyAction == keyAction)?.keyCode ?? KeyCode.None;
    }
}