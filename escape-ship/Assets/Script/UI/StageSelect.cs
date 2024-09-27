using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    // �������� ���� ��ư��
    public Button stageButton1;
    public Button stageButton2;
    public Button stageButton3;
    // �߰������� �ʿ��� ��ư���� ���� ���� ����

    void Start()
    {
        // �� ��ư�� ���� �Ҵ�� �������� ���� �̺�Ʈ �߰�
        if (stageButton1 != null)
        {
            stageButton1.onClick.AddListener(() => SelectStage(0));  // ù ��° �������� ����
        }
        if (stageButton2 != null)
        {
            stageButton2.onClick.AddListener(() => SelectStage(1));  // �� ��° �������� ����
        }
        if (stageButton3 != null)
        {
            stageButton3.onClick.AddListener(() => SelectStage(2));  // �� ��° �������� ����
        }
    }

    public void SelectStage(int stageIndex)
    {
        Debug.Log($"�������� {stageIndex} ���õ�.");
        // StageManager �̱����� ���� �������� Ȱ��ȭ
        StageManager.Instance.ActivateStage(stageIndex);
    }
}
