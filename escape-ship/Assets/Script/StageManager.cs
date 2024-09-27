using UnityEngine;

public class StageManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static StageManager Instance { get; private set; }

    // ���������� ������ �迭
    public GameObject[] stages;

    // �÷��̾ ������ ����
    public GameObject player;

    // �� ���������� �÷��̾� ���� ��ġ
    public Transform[] playerStartPoints;

    // Awake �޼��忡�� �̱��� �ν��Ͻ� ����
    private void Awake()
    {
        // �ν��Ͻ��� ��� ������ ���� ������Ʈ�� �ν��Ͻ��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �����ϰ� �ʹٸ� ���
        }
        else
        {
            // �̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ�� �ı�
            Destroy(gameObject);
        }
    }

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
                // �������� ���� ���� ��ġ�� Vector3�� ��������
                Vector3 startPosition = new Vector3(
                    playerStartPoints[stageIndex].position.x,
                    playerStartPoints[stageIndex].position.y,
                    playerStartPoints[stageIndex].position.z
                );

                // �÷��̾��� ��ġ�� ���� �������� ����
                player.transform.position = startPosition;
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
