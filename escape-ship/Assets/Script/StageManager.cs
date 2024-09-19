using UnityEngine;

public class StageManager : MonoBehaviour
{
    // ���������� ������ �迭
    public GameObject[] stages;

    // �÷��̾ ������ ����
    public GameObject player;

    // �� ���������� �÷��̾� ���� ��ġ
    public Transform[] playerStartPoints;

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

            // �÷��̾ �ش� ���������� ���� ��ġ�� �̵�
            player.transform.position = playerStartPoints[stageIndex].position;
            player.transform.rotation = playerStartPoints[stageIndex].rotation;
        }
    }
}
