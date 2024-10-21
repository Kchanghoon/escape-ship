using UnityEngine;

public class UseGear : MonoBehaviour
{
    public GameObject[] objectsToDisable;  // ��Ȱ��ȭ�� ���� ������Ʈ �迭
    private bool isGearUsed = false;  // ���(������ 12��)�� ���Ǿ����� ���θ� ����

    void Start()
    {
        // ���� ���� �� ������Ʈ�� ��� ��Ȱ��ȭ
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        KeyManager.Instance.keyDic[KeyAction.Play] += CheckIfGearIsUsed;
    }

    void Update()
    {
        // ������ 12���� �κ��丮���� ���Ǿ����� Ȯ��
        CheckIfGearIsUsed();
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

        // ��Ȱ��ȭ�� ������Ʈ���� �ٽ� Ȱ��ȭ
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }

        Debug.Log("������ 12���� ���Ǿ�����, ���� ������Ʈ���� Ȱ��ȭ�Ǿ����ϴ�.");
    }
}
