using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform originalParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 원래 위치 저장
        startPosition = transform.position;
        originalParent = transform.parent;

        // 드래그 중에는 부모를 Canvas로 변경
        transform.SetParent(GameObject.Find("Canvas").transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 마우스 위치를 따라 이동
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드랍 영역 밖에 드랍하면 원래 위치로 돌아감
        if (transform.parent == GameObject.Find("Canvas").transform)
        {
            transform.position = startPosition;
            transform.SetParent(originalParent);
        }
    }
}
