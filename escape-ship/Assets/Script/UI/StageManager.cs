using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    // ���������� ������ �迭
    public GameObject[] stages;

    // �÷��̾ ������ ����
    public GameObject player;

    // �� ���������� �÷��̾� ���� ��ġ
    public Transform[] playerStartPoints;

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

            // ��� �������� ��Ȱ��ȭ
            foreach (GameObject stage in stages)
            {
                stage.SetActive(false);
            }

            if (stageIndex >= 0 && stageIndex < stages.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                stages[stageIndex].SetActive(true);

                // �÷��̾ �ش� ���������� ���� ��ġ�� �̵�
                if (player != null)
                {
                    // �÷��̾� ��ġ�� ȸ���� �������� ���� ��ġ�� ����
                    player.transform.position = playerStartPoints[stageIndex].position;
                    player.transform.rotation = playerStartPoints[stageIndex].rotation;

                    Debug.Log($"�÷��̾ {stageIndex} ���������� ���� ��ġ�� �̵��߽��ϴ�. ���� ��ġ: {player.transform.position}");
                }
                else
                {
                    Debug.LogWarning("Player ������Ʈ�� �������� �ʽ��ϴ�.");
                }
            }
            else
            {
                Debug.LogError("�߸��� �������� �ε����Դϴ�.");
            }
        }
    }
}
