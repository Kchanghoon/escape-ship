using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;  // DoTween 네임스페이스 추가

public class UpgradeItem : MonoBehaviour
{
    [SerializeField] private Transform player;  // 플레이어의 Transform
    [SerializeField] private float interactDistance = 3f;  // 상호작용 가능한 거리
    private bool isMouseOverItem = false;  // 마우스가 오브젝트 위에 있는지 여부를 저장
    [SerializeField] private TextMeshProUGUI statusText;  // 상자 상태를 표시하는 TextMeshPro 텍스트
    [SerializeField] private float fadeDuration = 1f;  // 텍스트가 서서히 사라지는 시간

    void Start()
    {
        // 상태 텍스트 비활성화
        statusText.gameObject.SetActive(false);

        KeyManager.Instance.keyDic[KeyAction.Play] += TryUpgrade;  // KeyManager에서 Play 키 이벤트 등록
    }

    void Update()
    {
        // 플레이어와 오브젝트 사이의 거리를 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 플레이어가 상호작용 가능한 거리 내에 있고, 마우스가 아이템 위에 있는 경우
        if (distanceToPlayer <= interactDistance && isMouseOverItem)
        {
            UpdateStatusText();  // 아이템 상태를 확인하여 텍스트 업데이트
        }
        else
        {
            HideTextWithAnimation();  // 텍스트가 사라지게 함
        }
    }

    // 마우스가 오브젝트 위에 있을 때 호출되는 메서드
    private void OnMouseEnter()
    {
        isMouseOverItem = true;  // 마우스가 오브젝트 위에 있음
    }

    // 마우스가 오브젝트에서 벗어날 때 호출되는 메서드
    private void OnMouseExit()
    {
        isMouseOverItem = false;  // 마우스가 오브젝트 위에 없음
    }

    // 텍스트를 서서히 나타내는 메서드
    private void ShowTextWithAnimation()
    {
        statusText.gameObject.SetActive(true);  // 텍스트 활성화
        statusText.DOFade(1, fadeDuration);  // 투명도 1로 서서히 증가 (애니메이션으로 텍스트 나타내기)
    }

    // 텍스트를 서서히 사라지게 하는 메서드
    private void HideTextWithAnimation()
    {
        statusText.DOFade(0, fadeDuration).OnComplete(() => statusText.gameObject.SetActive(false));  // 투명도 0으로 서서히 감소 후 비활성화
    }

    // 아이템 상태를 확인하고 텍스트를 업데이트하는 메서드
    private void UpdateStatusText()
    {
        // ItemController에서 현재 보유 중인 아이템 확인
        var itemController = ItemController.Instance;

        // 인벤토리에서 11번과 5번 아이템이 있는지 확인
        var item11 = itemController.curItemDatas.Find(x => x.id == "11");
        var item5 = itemController.curItemDatas.Find(x => x.id == "5");

        if (item11 != null && item5 != null)
        {
            // 11번과 5번 아이템이 있으면 업그레이드 가능 메시지
            statusText.text = "카드 업그레이드가 가능합니다.";
        }
        else
        {
            // 아이템이 없으면 업그레이드 불가능 메시지
            statusText.text = "1급 보안카드와 디스크가 필요합니다.";
        }

        // 텍스트를 서서히 나타내기
        ShowTextWithAnimation();
    }

    // 기존 TryUpgrade 메서드 그대로 유지
    private void TryUpgrade()
    {
        // 플레이어와 오브젝트 사이의 거리를 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 선택된 아이템이 있으며, 일정 거리 내에 있고, 마우스가 아이템 위에 있는지 확인
        if (distanceToPlayer <= interactDistance && isMouseOverItem)
        {
            // ItemController에서 현재 보유 중인 아이템 확인
            var itemController = ItemController.Instance;

            // 인벤토리에서 11번과 5번 아이템이 있는지 확인
            var item11 = itemController.curItemDatas.Find(x => x.id == "11");
            var item5 = itemController.curItemDatas.Find(x => x.id == "5");

            if (item11 != null && item5 != null)
            {
                // 11번과 5번 아이템이 있으면 8번 아이템을 추가하고 기존 아이템 제거
                itemController.RemoveItemById("11");  // 11번 아이템 제거
                itemController.RemoveItemById("5");  // 5번 아이템 제거
                itemController.AddItem("8");  // 8번 아이템 추가
                statusText.text = "함장 카드 발급완료.";
                ShowTextWithAnimation();
                Debug.Log("아이템 업그레이드 성공! 8번 아이템이 추가되었습니다.");
            }
            else
            {
                ShowTextWithAnimation();
                Debug.Log("업그레이드에 필요한 아이템이 없습니다. 11번과 5번이 필요합니다.");
            }
        }
        else
        {
            Debug.Log("업그레이드를 할 수 없습니다. 플레이어가 충분히 가까이 있지 않거나 마우스가 아이템에 없습니다.");
        }
    }
}
