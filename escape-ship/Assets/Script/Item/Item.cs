using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro 사용 시

public class Item : MonoBehaviour
{
    public ItemDataExample itemData;  // 아이템 데이터
    public float pickUpDistance = 2f;  // 아이템을 가져갈 수 있는 거리
    private Transform playerTransform;  // 플레이어의 Transform
    private bool isMouseOverItem = false;  // 에임이 아이템에 맞춰졌는지 여부
    public TextMeshProUGUI pickUpText;  // 화면에 출력할 텍스트 (TextMeshPro 사용 시)

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // 플레이어의 Transform을 가져옴
        pickUpText.gameObject.SetActive(false);  // 처음엔 텍스트를 비활성화
    }

    // 플레이어가 마우스를 아이템에 올렸을 때 (에임이 맞춰졌을 때)
    private void OnMouseEnter()
    {
        isMouseOverItem = true;  // 에임이 아이템에 맞춰짐
        HighlightItem(true);  // 아이템 하이라이트 활성화
    }

    // 플레이어가 마우스를 아이템에서 뗐을 때 (에임이 벗어났을 때)
    private void OnMouseExit()
    {
        isMouseOverItem = false;  // 에임이 아이템에서 벗어남
        HighlightItem(false);  // 아이템 하이라이트 비활성화
        pickUpText.gameObject.SetActive(false);  // 에임이 벗어나면 텍스트 비활성화
    }

    void Update()
    {
        if (isMouseOverItem)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);

            // 플레이어가 지정한 거리 안에 있는 경우
            if (distance <= pickUpDistance)
            {
                pickUpText.gameObject.SetActive(true);  // 텍스트 활성화
                pickUpText.text = "F키를 눌러 아이템을 집으세요";  // 문구 설정

                // F 키를 누르면 아이템을 가져옴
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PickUpItem();  // 아이템 획득
                }
            }
            else
            {
                pickUpText.gameObject.SetActive(false);  // 거리가 멀어지면 텍스트 비활성화
            }
        }
    }

    // 아이템을 획득하는 함수
    private void PickUpItem()
    {
        ItemController.Instance.SetCanPickUp(itemData);  // 아이템을 인벤토리에 추가
        Destroy(gameObject);  // 아이템을 획득한 후 파괴
        pickUpText.gameObject.SetActive(false);  // 아이템을 집으면 텍스트 비활성화
        Debug.Log($"아이템 {itemData.id}을(를) 획득했습니다.");
    }

    // 아이템 하이라이트를 제어하는 함수
    private void HighlightItem(bool highlight)
    {
        var outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = highlight;
        }
    }
}
