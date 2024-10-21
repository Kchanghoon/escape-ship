using UnityEngine;
using DG.Tweening;  // DoTween 네임스페이스 추가

public class UseGear : MonoBehaviour
{
    public GameObject[] objectsToMove;  // 이동시킬 게임 오브젝트 배열
    private bool isGearUsed = false;  // 기어(아이템 12번)가 사용되었는지 여부를 추적
    public float moveDistance = 5f;  // 오브젝트를 이동시킬 Y축 거리
    public float moveDuration = 1f;  // 이동 애니메이션의 지속 시간

    void Start()
    {
        // 게임 시작 시 오브젝트들을 초기 위치로 설정 (필요한 경우 초기 위치 설정)
        foreach (GameObject obj in objectsToMove)
        {
            // obj.transform.position = new Vector3(obj.transform.position.x, originalYPosition, obj.transform.position.z);
        }

        KeyManager.Instance.keyDic[KeyAction.Play] += CheckIfGearIsUsed;
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

        ItemController.Instance.DeleteItemQuantity("12");
        // 오브젝트들을 Y축으로 moveDistance만큼 부드럽게 이동
        foreach (GameObject obj in objectsToMove)
        {
            obj.transform.DOMoveY(obj.transform.position.y + moveDistance, moveDuration)
                .SetEase(Ease.OutQuad);  // Ease 옵션으로 부드럽게 움직임
        }

        Debug.Log("아이템 12번이 사용되었으며, 게임 오브젝트들이 Y축으로 이동되었습니다.");
    }
}
