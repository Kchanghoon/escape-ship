using UnityEngine;
using DG.Tweening;  // DoTween ���ӽ����̽� �߰�

public class UseGear : MonoBehaviour
{
    public GameObject[] objectsToMove;  // �̵���ų ���� ������Ʈ �迭
    private bool isGearUsed = false;  // ���(������ 12��)�� ���Ǿ����� ���θ� ����
    public float moveDistance = 5f;  // ������Ʈ�� �̵���ų Y�� �Ÿ�
    public float moveDuration = 1f;  // �̵� �ִϸ��̼��� ���� �ð�

    void Start()
    {
        // ���� ���� �� ������Ʈ���� �ʱ� ��ġ�� ���� (�ʿ��� ��� �ʱ� ��ġ ����)
        foreach (GameObject obj in objectsToMove)
        {
            // obj.transform.position = new Vector3(obj.transform.position.x, originalYPosition, obj.transform.position.z);
        }

        KeyManager.Instance.keyDic[KeyAction.Play] += CheckIfGearIsUsed;
    }

    // ������ 12���� ����ߴ��� Ȯ���ϴ� �޼���
    void CheckIfGearIsUsed()
    {
        if (!isGearUsed)  // ���� �� ������ �ʾ��� ���� Ȯ��
        {
            var selectedItem = InventoryUIExmaple.Instance.GetSelectedItem();  // ���õ� �������� ������
            if (selectedItem != null && selectedItem.id == "12")  // ������ ID�� 12������ Ȯ��
            {
                UseItem();  // ������ ��� ���� ȣ��
            }
        }
    }

    // �������� ���Ǿ��� �� ȣ��Ǵ� �޼���
    void UseItem()
    {
        isGearUsed = true;  // �� ���Ǿ����� ���

        ItemController.Instance.DeleteItemQuantity("12");
        // ������Ʈ���� Y������ moveDistance��ŭ �ε巴�� �̵�
        foreach (GameObject obj in objectsToMove)
        {
            obj.transform.DOMoveY(obj.transform.position.y + moveDistance, moveDuration)
                .SetEase(Ease.OutQuad);  // Ease �ɼ����� �ε巴�� ������
        }

        Debug.Log("������ 12���� ���Ǿ�����, ���� ������Ʈ���� Y������ �̵��Ǿ����ϴ�.");
    }
}
