using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnTrigger : MonoBehaviour
{
    public GameObject targetObject;  // �ƿ����� ȿ���� ������ ��� ������Ʈ
    private Outline outline;  // �ƿ����� ������Ʈ

    void Start()
    {
        // ��� ������Ʈ�� �ƿ����� ������Ʈ�� �������ų� �߰�
        outline = targetObject.GetComponent<Outline>();
        if (outline == null)
        {
            outline = targetObject.AddComponent<Outline>();
            outline.OutlineWidth = 5f;  // �׵θ� �β� ����
            outline.OutlineColor = Color.yellow;  // �׵θ� ���� ����
        }
        outline.enabled = false;  // ������ �� �ƿ����� ��Ȱ��ȭ
    }

    // �÷��̾ Ʈ���� �ȿ� ������ ��
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.enabled = true;  // �ƿ����� Ȱ��ȭ
        }
    }

    // �÷��̾ Ʈ���Ÿ� ����� ��
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.enabled = false;  // �ƿ����� ��Ȱ��ȭ
        }
    }
}
