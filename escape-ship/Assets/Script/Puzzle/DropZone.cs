using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string correctTag; // �ùٸ� ���� ������ �±�

    public void OnDrop(PointerEventData eventData)
    {
        // �巡�� ���� ������Ʈ�� ������
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (draggable != null && eventData.pointerDrag.CompareTag(correctTag))
        {
            // ���� ������ �ùٸ� ��ġ�� ���̸� �θ� ����
            draggable.transform.SetParent(transform);
            draggable.transform.position = transform.position;

            Debug.Log("���� ������ �ùٸ� ��ġ�� �������ϴ�!");
        }
        else
        {
            Debug.Log("�ùٸ� ������ �ƴմϴ�.");
        }
    }
}
