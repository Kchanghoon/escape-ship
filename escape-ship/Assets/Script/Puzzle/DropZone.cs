using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string correctTag; // 올바른 퍼즐 조각의 태그

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그 중인 오브젝트를 가져옴
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (draggable != null && eventData.pointerDrag.CompareTag(correctTag))
        {
            // 퍼즐 조각이 올바른 위치에 놓이면 부모를 변경
            draggable.transform.SetParent(transform);
            draggable.transform.position = transform.position;

            Debug.Log("퍼즐 조각이 올바른 위치에 놓였습니다!");
        }
        else
        {
            Debug.Log("올바른 조각이 아닙니다.");
        }
    }
}
