using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerState : MonoBehaviour
{
    // �÷��̾� ���� ���� ����
    [SerializeField] private float stress;  // ��Ʈ���� ��ġ
    [SerializeField] private float oxygen;  // ��� ��ġ

    // �ִ밪, �ּҰ��� ������ �� �� �ִ�.
    [SerializeField] private float maxStress = 100f;
    [SerializeField] private float maxOxygen = 100f;

    private bool isInRecoverZone = false;  // �÷��̾ ȸ���� �ȿ� �ִ��� ����
    private bool isOxygenDepleted = false;  // ��Ұ� �� ���������� ����

    // �ܺο��� ��ҿ� ��Ʈ���� ���� ������ �� �ֵ��� public ������Ƽ �߰�
    public float Stress => stress;  // ��Ʈ���� �� �б�
    public float Oxygen => oxygen;  // ��� �� �б�
    public float MaxStress => maxStress;  // �ִ� ��Ʈ���� �� �б�
    public float MaxOxygen => maxOxygen;  // �ִ� ��� �� �б�

    // Start�� ���� ���� �� ȣ��
    private void Start()
    {
        stress = 0f;
        oxygen = maxOxygen;

        KeyManager.Instance.keyDic[KeyAction.Jump] += OnJump;
        KeyManager.Instance.keyDic[KeyAction.Run] += OnRun;
        KeyManager.Instance.keyDic[KeyAction.Play] += OnAction;  // EŰ �Ҵ� (����: KeyAction.Action�� EŰ�� ������)
    }

    private void OnJump()
    {
        DecreaseOxygen(3f);  // ��Ҹ� 3��ŭ ����
    }

    private void OnRun()
    {
        DecreaseOxygen(Time.deltaTime * 2f); // ��� 2�ʸ��� ����.
    }

    private void OnAction()
    {
        IncreaseStress(5f);  // E Ű�� ������ ��Ʈ������ 5 ����
    }

    // Update�� �� �����Ӹ��� ȣ��
    void Update()
    {
        UpdateOxygen();
        UpdateStress();
    }

    // ��Ʈ������ ������Ʈ�ϴ� �޼���
    void UpdateStress()
    {
        if (isOxygenDepleted && !isInRecoverZone)
        {
            // ��Ұ� �� �������� ȸ������ ���� ������ �ʴ� ��Ʈ���� 5 ����
            IncreaseStress(5f * Time.deltaTime);
        }
    }

    // ��Ҹ� ������Ʈ�ϴ� �޼���
    void UpdateOxygen()
    {
        if (oxygen > 0)
        {
            // ��Ұ� ���������� �ڿ� ����
            oxygen -= Time.deltaTime * 1f;  // 1f�� ���� �ӵ�
            isOxygenDepleted = false;
        }
        else
        {
            // ��Ұ� 0 ���Ϸ� �������� �ʵ��� �����ϰ� ��Ұ� ������ ���·� ���
            oxygen = 0;
            isOxygenDepleted = true;
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

    // ȸ������ ���� �� ȣ��
    public void EnterRecoverZone()
    {
        isInRecoverZone = true;  // ȸ������ �ִ� ���·� ���
    }

    // ȸ�������� ������ �� ȣ��
    public void ExitRecoverZone()
    {
        isInRecoverZone = false;  // ȸ�������� ���� ���·� ���
    }
}
