using UnityEngine;

public class UseGear : MonoBehaviour
{
    public GameObject[] objectsToDisable;  // 비활성화할 게임 오브젝트 배열
    private bool isGearUsed = false;  // 기어(아이템 12번)가 사용되었는지 여부를 추적

    void Start()
    {
        // 게임 시작 시 오브젝트를 모두 비활성화
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        KeyManager.Instance.keyDic[KeyAction.Play] += CheckIfGearIsUsed;
    }

    void Update()
    {
        // 아이템 12번이 인벤토리에서 사용되었는지 확인
        CheckIfGearIsUsed();
    }

    // 아이템 12번을 사용했는지 확인하는 메서드
    void CheckIfGearIsUsed()
    {
        if (!isGearUsed)  // 아직 기어가 사용되지 않았을 때만 확인
        {
            var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();  // 선택된 아이템을 가져옴
            if (selectedItem != null && selectedItem.id == "12")  // 아이템 ID가 12번인지 확인
            {
                UseItem();  // 아이템 사용 로직 호출
            }
        }
    }

    // 아이템이 사용되었을 때 호출되는 메서드
    void UseItem()
    {
        isGearUsed = true;  // 기어가 사용되었음을 기록

        // 비활성화된 오브젝트들을 다시 활성화
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }

        Debug.Log("아이템 12번이 사용되었으며, 게임 오브젝트들이 활성화되었습니다.");
    }
}
