using UnityEngine;

public class StageManager : MonoBehaviour
{

    void Start()
    {
        ActivateStage(0);  // �⺻���� Stage1�� Ȱ��ȭ
    }
    // ���������� ������ �迭
    public GameObject[] stages;

    // Ư�� ���������� Ȱ��ȭ�ϴ� �Լ�
    public void ActivateStage(int stageIndex)
    {
        // ��� �������� ��Ȱ��ȭ
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].SetActive(false);
        }

        // ���õ� ���������� Ȱ��ȭ
        if (stageIndex >= 0 && stageIndex < stages.Length)
        {
            stages[stageIndex].SetActive(true);
        }
    }
}
