using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform originalParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�� ���� �� ���� ��ġ ����
        startPosition = transform.position;
        originalParent = transform.parent;

        // �巡�� �߿��� �θ� Canvas�� ����
        transform.SetParent(GameObject.Find("Canvas").transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ���콺 ��ġ�� ���� �̵�
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ��� ���� �ۿ� ����ϸ� ���� ��ġ�� ���ư�
        if (transform.parent == GameObject.Find("Canvas").transform)
        {
            transform.position = startPosition;
            transform.SetParent(originalParent);
        }
    }
}
