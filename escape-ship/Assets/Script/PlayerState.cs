using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerState : MonoBehaviour
{
    // �÷��̾� ���� ���� ����
    [SerializeField] float stress;  // ��Ʈ���� ��ġ
    [SerializeField] float oxygen;  // ��� ��ġ

    // �ִ밪, �ּҰ��� ������ �� �� �ִ�.
    [SerializeField] float maxStress = 100f;
    [SerializeField] float maxOxygen = 100f;

    // Start�� ���� ���� �� ȣ��
    private void Start()
    {
        // �ʱ� �� ���� (���ϴ� ������ �ʱ�ȭ�� �� �ִ�)
        stress = 0f;
        oxygen = maxOxygen;
        KeyManager.Instance.keyDic[KeyAction.Jump] += OnJump;
        KeyManager.Instance.keyDic[KeyAction.Run] += OnRun;
    }

    private void OnJump()
    {
        DecreaseOxygen(5f);  // ��Ҹ� 5��ŭ ����

    }

    private void OnRun()
    {
        DecreaseOxygen(Time.deltaTime * 2f); // ��� 2�ʸ��� ����.
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
