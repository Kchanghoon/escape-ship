using UnityEngine;

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

            // �÷��̾ �ش� ���������� ���� ��ġ�� �̵�
            if (player != null)
            {
                CharacterController characterController = player.GetComponent<CharacterController>();

                if (characterController != null)
                {
                    // CharacterController�� �ִ� ���, Transform�� ���� ��ġ �̵�
                    characterController.enabled = false;  // �̵��ϱ� ���� CharacterController�� ��Ȱ��ȭ
                    player.transform.position = playerStartPoints[stageIndex].position;
                    player.transform.rotation = playerStartPoints[stageIndex].rotation;
                    characterController.enabled = true;  // ��ġ ���� �� �ٽ� CharacterController Ȱ��ȭ
                }
                else
                {
                    // CharacterController�� ���� ���� Transform�� ���� �̵�
                    player.transform.position = playerStartPoints[stageIndex].position;
                    player.transform.rotation = playerStartPoints[stageIndex].rotation;
                }

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
