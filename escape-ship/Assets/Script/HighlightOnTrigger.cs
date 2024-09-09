using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnTrigger : MonoBehaviour
{
    public GameObject targetObject;  // 아웃라인 효과를 적용할 대상 오브젝트
    private Outline outline;  // 아웃라인 컴포넌트

    void Start()
    {
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

    // 플레이어가 트리거 안에 들어왔을 때
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.enabled = true;  // 아웃라인 활성화
        }
    }

    // 플레이어가 트리거를 벗어났을 때
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.enabled = false;  // 아웃라인 비활성화
        }
    }
}
