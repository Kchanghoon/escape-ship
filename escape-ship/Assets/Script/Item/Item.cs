using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro ��� ��

public class Item : MonoBehaviour
{
    public ItemDataExample itemData;  // ������ ������
    public float pickUpDistance = 2f;  // �������� ������ �� �ִ� �Ÿ�
    private Transform playerTransform;  // �÷��̾��� Transform
    private bool isMouseOverItem = false;  // ������ �����ۿ� ���������� ����
    public TextMeshProUGUI pickUpText;  // ȭ�鿡 ����� �ؽ�Ʈ (TextMeshPro ��� ��)

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // �÷��̾��� Transform�� ������
        pickUpText.gameObject.SetActive(false);  // ó���� �ؽ�Ʈ�� ��Ȱ��ȭ
    }

    // �÷��̾ ���콺�� �����ۿ� �÷��� �� (������ �������� ��)
    private void OnMouseEnter()
    {
        isMouseOverItem = true;  // ������ �����ۿ� ������
        HighlightItem(true);  // ������ ���̶���Ʈ Ȱ��ȭ
    }

    // �÷��̾ ���콺�� �����ۿ��� ���� �� (������ ����� ��)
    private void OnMouseExit()
    {
        isMouseOverItem = false;  // ������ �����ۿ��� ���
        HighlightItem(false);  // ������ ���̶���Ʈ ��Ȱ��ȭ
        pickUpText.gameObject.SetActive(false);  // ������ ����� �ؽ�Ʈ ��Ȱ��ȭ
    }

    void Update()
    {
        if (isMouseOverItem)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);

            // �÷��̾ ������ �Ÿ� �ȿ� �ִ� ���
            if (distance <= pickUpDistance)
            {
                pickUpText.gameObject.SetActive(true);  // �ؽ�Ʈ Ȱ��ȭ
                pickUpText.text = "FŰ�� ���� �������� ��������";  // ���� ����

                // F Ű�� ������ �������� ������
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PickUpItem();  // ������ ȹ��
                }
            }
            else
            {
                pickUpText.gameObject.SetActive(false);  // �Ÿ��� �־����� �ؽ�Ʈ ��Ȱ��ȭ
            }
        }
    }

    // �������� ȹ���ϴ� �Լ�
    private void PickUpItem()
    {
        ItemController.Instance.SetCanPickUp(itemData);  // �������� �κ��丮�� �߰�
        Destroy(gameObject);  // �������� ȹ���� �� �ı�
        pickUpText.gameObject.SetActive(false);  // �������� ������ �ؽ�Ʈ ��Ȱ��ȭ
        Debug.Log($"������ {itemData.id}��(��) ȹ���߽��ϴ�.");
    }

    // ������ ���̶���Ʈ�� �����ϴ� �Լ�
    private void HighlightItem(bool highlight)
    {
        var outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = highlight;
        }
    }
}
