using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnHover : MonoBehaviour
{
    [SerializeField] GameObject targetObject;  // 아웃라인 효과를 적용할 대상 오브젝트
    [SerializeField] float interactionDistance = 3f;  // 상호작용 가능한 거리
    private Outline outline;  // 아웃라인 컴포넌트
    private bool isMouseOver = false;  // 마우스가 오브젝트 위에 있는지 여부
    Transform player { get => PlayerController.Instance.transform; }  // 플레이어의 Transform

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("targetObject가 할당되지 않았습니다.");
            return;
        }

        if (player == null)
        {
            Debug.LogError("player가 할당되지 않았습니다.");
            return;
        }

        // 대상 오브젝트에 아웃라인 컴포넌트를 가져오거나 추가
        outline = targetObject.GetComponent<Outline>();
        if (outline == null)
        {
            outline = targetObject.AddComponent<Outline>();
            outline.OutlineWidth = 5f;  // 테두리 두께 설정
            outline.OutlineColor = Color.yellow;  // 테두리 색상 설정
        }
        outline.enabled = false;  // 시작할 때 아웃라인 비활성화
    }

    private void Update()
    {
        // 플레이어와 오브젝트 간의 거리 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 플레이어가 상호작용 가능한 거리 내에 있고, 마우스가 오브젝트 위에 있을 때만 아웃라인 활성화
        if (isMouseOver && distanceToPlayer <= interactionDistance)
        {
            outline.enabled = true;  // 아웃라인 활성화
        }
        else
        {
            outline.enabled = false;  // 아웃라인 비활성화
        }
    }

    // 마우스가 오브젝트 위에 있을 때
    private void OnMouseEnter()
    {
        isMouseOver = true;  // 마우스가 오브젝트 위에 있음
    }

    // 마우스가 오브젝트에서 벗어났을 때
    private void OnMouseExit()
    {
        isMouseOver = false;  // 마우스가 오브젝트에서 벗어남
        outline.enabled = false;  // 아웃라인 비활성화
    }
}
