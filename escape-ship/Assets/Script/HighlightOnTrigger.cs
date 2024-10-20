using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnHover : MonoBehaviour
{
    [SerializeField] GameObject targetObject;  // �ƿ����� ȿ���� ������ ��� ������Ʈ
    [SerializeField] float interactionDistance = 3f;  // ��ȣ�ۿ� ������ �Ÿ�
    private Outline outline;  // �ƿ����� ������Ʈ
    private bool isMouseOver = false;  // ���콺�� ������Ʈ ���� �ִ��� ����
    Transform player { get => PlayerController.Instance.transform; }  // �÷��̾��� Transform

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("targetObject�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        if (player == null)
        {
            Debug.LogError("player�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

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

    private void Update()
    {
        // �÷��̾�� ������Ʈ ���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // �÷��̾ ��ȣ�ۿ� ������ �Ÿ� ���� �ְ�, ���콺�� ������Ʈ ���� ���� ���� �ƿ����� Ȱ��ȭ
        if (isMouseOver && distanceToPlayer <= interactionDistance)
        {
            outline.enabled = true;  // �ƿ����� Ȱ��ȭ
        }
        else
        {
            outline.enabled = false;  // �ƿ����� ��Ȱ��ȭ
        }
    }

    // ���콺�� ������Ʈ ���� ���� ��
    private void OnMouseEnter()
    {
        isMouseOver = true;  // ���콺�� ������Ʈ ���� ����
    }

    // ���콺�� ������Ʈ���� ����� ��
    private void OnMouseExit()
    {
        isMouseOver = false;  // ���콺�� ������Ʈ���� ���
        outline.enabled = false;  // �ƿ����� ��Ȱ��ȭ
    }
}
