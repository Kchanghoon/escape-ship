using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // �÷��̾� ���� ���� ����
    public float stress;  // ��Ʈ���� ��ġ
    public float oxygen;  // ��� ��ġ

    // �ִ밪, �ּҰ��� ������ �� �� �ִ�.
    public float maxStress = 100f;
    public float maxOxygen = 100f;

    // Start�� ���� ���� �� ȣ��
    void Start()
    {
        // �ʱ� �� ���� (���ϴ� ������ �ʱ�ȭ�� �� �ִ�)
        stress = 0f;
        oxygen = maxOxygen;
    }

    // Update�� �� �����Ӹ��� ȣ��
    void Update()
    {
        // ��Ʈ���� �Ǵ� ��Ұ� ���ϴ� ������ ���⿡ ���� ����
        UpdateStress();
        UpdateOxygen();
    }

    // ��Ʈ������ ������Ʈ�ϴ� �޼���
    void UpdateStress()
    {
        // ���Ƿ� ��Ʈ������ �����ϴ� ���� (����)
        if (stress < maxStress)
        {
            stress += Time.deltaTime * 2f;  // 2f�� ���� �ӵ�
        }
        else
        {
            stress = maxStress;  // �ִ� ��Ʈ���� ����
        }
    }

    // ��Ҹ� ������Ʈ�ϴ� �޼���
    void UpdateOxygen()
    {
        // ���Ƿ� ��Ұ� �����ϴ� ���� (����)
        if (oxygen > 0)
        {
            oxygen -= Time.deltaTime * 1f;  // 1f�� ���� �ӵ�
        }
        else
        {
            oxygen = 0;  // ��Ұ� 0 ���Ϸ� �������� �ʵ��� ����
        }
    }

    // �ܺο��� ��Ʈ������ �����ϴ� �޼���
    public void IncreaseStress(float amount)
    {
        stress = Mathf.Clamp(stress + amount, 0, maxStress);
    }

    public void DecreaseStress(float amount)
    {
        stress = Mathf.Clamp(stress - amount, 0, maxStress);
    }

    // �ܺο��� ��ҿ� �����ϴ� �޼���
    public void IncreaseOxygen(float amount)
    {
        oxygen = Mathf.Clamp(oxygen + amount, 0, maxOxygen);
    }

    public void DecreaseOxygen(float amount)
    {
        oxygen = Mathf.Clamp(oxygen - amount, 0, maxOxygen);
    }
}
