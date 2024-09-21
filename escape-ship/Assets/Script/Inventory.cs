using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] items;  // 아이템을 배열로 선언

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();

        // 슬롯 배열의 크기를 slotParent의 자식 개수로 설정
        items = new Item[slots.Length];
    }
#endif

    void Awake()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        // 슬롯을 업데이트하는 부분
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
    }

    public void AddItem(Item _item)
    {
        // 빈 슬롯을 찾은 후 아이템을 추가
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = _item;
                FreshSlot();
                return; // 아이템 추가 후 종료
            }
        }

        // 모든 슬롯이 가득 찬 경우
        print("슬롯이 가득 차 있습니다.");
    }
}
