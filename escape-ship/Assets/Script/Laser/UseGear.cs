using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Linq;  // DoTween 네임스페이스 추가

public class UseGear : MonoBehaviour
{
    public GameObject[] objectsToMove;  // 이동시킬 게임 오브젝트 배열
    private bool isGearUsed = false;  // 기어(아이템 12번)가 사용되었는지 여부를 추적
    public float moveDistance = 5f;  // 오브젝트를 이동시킬 Y축 거리
    public float moveDuration = 1f;  // 이동 애니메이션의 지속 시간
    [SerializeField] AudioSource GearMove;  // 기어 움직이는 소리
    [SerializeField] Transform player;  // 플레이어의 Transform
    private bool isMouseOverItem = false;  // 마우스가 오브젝트 위에 있는지 여부를 저장
    [SerializeField] float interactDistance = 4f;  // 상호작용 가능 거리
    [SerializeField] TextMeshProUGUI Text;  // 상호작용 안내 텍스트

    void Start()
    {

        KeyManager.Instance.keyDic[KeyAction.Play] += CheckIfGearIsUsed;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (isMouseOverItem && distanceToPlayer <= interactDistance)
        {
            ShowText();  // 상호작용 안내 텍스트 활성화
        }
        else
        {
            HideText();  // 상호작용 안내 텍스트 비활성화
        }
    }
    public KeyCode GetKeyCode(KeyAction keyAction)
    {
        // KeyManager의 InputDownKeySets와 InputKeySets를 사용하여 키 찾기
        return KeyManager.Instance.InputDownKeySets.Concat(KeyManager.Instance.InputKeySets)
            .FirstOrDefault(keySet => keySet.keyAction == keyAction)?.keyCode ?? KeyCode.None;
    }
    private void ShowText()
    {        // KeyManager에서 현재 Play 키값 가져오기
        KeyCode playKey = KeyManager.Instance.GetKeyCode(KeyAction.Play);

        Text.gameObject.SetActive(true);  // 텍스트 활성화
        Text.text = $"{playKey}키를 눌러 기어장착 가능";  // 안내 메시지 설정
    }

    // 상호작용 안내 텍스트를 비활성화하는 메서드
    private void HideText()
    {
        Text.gameObject.SetActive(false);  // 텍스트 비활성화
    }

    private void OnMouseEnter()
    {
        isMouseOverItem = true;
    }

    private void OnMouseExit()
    {
        isMouseOverItem = false;
    }

    // 아이템 12번을 사용했는지 확인하는 메서드
    void CheckIfGearIsUsed()
    {
        if (!isGearUsed)  // 아직 기어가 사용되지 않았을 때만 확인
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (isMouseOverItem && distanceToPlayer <= interactDistance)
            {
                var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();  // 선택된 아이템을 가져옴
                if (selectedItem != null && selectedItem.id == "12")  // 아이템 ID가 12번인지 확인
                {
                    UseItem();  // 아이템 사용 로직 호출
                }
            }
        }
    }

    // 아이템이 사용되었을 때 호출되는 메서드
    void UseItem()
    {
        isGearUsed = true;  // 기어가 사용되었음을 기록
        GearMove.Play();  // 문 열림 소리 재생.Play;

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
