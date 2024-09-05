using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class SaveMenuController : MonoBehaviour
{
    public GameObject saveSlotPanel;  // SaveSlotPanel UI �г�
    public GameObject pauseMenuUI;    // �Ͻ����� �޴� UI
    public Button slotButton1, slotButton2, slotButton3;  // �� ���� ���� ��ư
    public Button loadSlotButton1, loadSlotButton2, loadSlotButton3;  // �� �ε� ���� ��ư

    void Start()
    {
        // �� ���� ��ư�� Ŭ�� �̺�Ʈ �߰�
        slotButton1.onClick.AddListener(() => SaveGame(1));
        slotButton2.onClick.AddListener(() => SaveGame(2));
        slotButton3.onClick.AddListener(() => SaveGame(3));

        loadSlotButton1.onClick.AddListener(() => LoadGame(1));
        loadSlotButton2.onClick.AddListener(() => LoadGame(2));
        loadSlotButton3.onClick.AddListener(() => LoadGame(3));
    }

    public void ShowSaveSlotPanel()
    {
        saveSlotPanel.SetActive(true);  // ���� ���� UI ǥ��
        pauseMenuUI.SetActive(false);   // �Ͻ����� �޴� UI �����
    }

    public void SaveGame(int slotNumber)
    {
        Player player = FindObjectOfType<Player>(); // Player ������Ʈ ã��
        if (player != null)
        {
            GameState gameState = player.GetGameState(); // �÷��̾��� ���� ���� ��������
            string json = JsonUtility.ToJson(gameState); // ��ü�� JSON �������� ����ȭ

            // JSON�� ���Ϸ� ����
            string path = Application.persistentDataPath + "/saveSlot" + slotNumber + ".json";
            File.WriteAllText(path, json);

            Debug.Log("���� ���� ��Ȳ�� ���� " + slotNumber + "�� ����Ǿ����ϴ�: " + path);
            saveSlotPanel.SetActive(false);  // ���� ���� UI �����
            pauseMenuUI.SetActive(true);     // �Ͻ����� �޴� UI ǥ��
        }
        else
        {
            Debug.LogWarning("Player ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    public void LoadGame(int slotNumber)
    {
        string path = Application.persistentDataPath + "/saveSlot" + slotNumber + ".json";
        Debug.Log("���� ���: " + path);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("���� JSON ������: " + json);

            GameState gameState = JsonUtility.FromJson<GameState>(json);
            Player player = FindObjectOfType<Player>();

            if (player != null)
            {
                player.SetGameState(gameState);
                Debug.Log("������ ���� " + slotNumber + "���� �ε�Ǿ����ϴ�:");
                Debug.Log("�÷��̾� ����: " + gameState.playerScore);
                Debug.Log("�÷��̾� ��ġ: (" + gameState.playerPositionX + ", " + gameState.playerPositionY + ", " + gameState.playerPositionZ + ")");
                Debug.Log("�ε� �� �÷��̾� ��ġ: " + player.transform.position); // ����� �α� �߰�
            }
            else
            {
                Debug.LogWarning("Player ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("���� " + slotNumber + "�� ����� ������ �����ϴ�.");
        }
    }
    public void SetGameState(GameState gameState)
    {
      //  score = gameState.playerScore;
        transform.position = new Vector3(gameState.playerPositionX, gameState.playerPositionY, gameState.playerPositionZ);
    }
}
