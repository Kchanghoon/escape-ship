using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveMenuController : MonoBehaviour
{
    public GameObject saveSlotPanel;
    public GameObject pauseMenuUI;
    public Button slotButton1, slotButton2, slotButton3;
    public Button loadSlotButton1, loadSlotButton2, loadSlotButton3;

    void Start()
    {
        slotButton1.onClick.AddListener(() => SaveGame(1));
        slotButton2.onClick.AddListener(() => SaveGame(2));
        slotButton3.onClick.AddListener(() => SaveGame(3));

        loadSlotButton1.onClick.AddListener(() => LoadGame(1));
        loadSlotButton2.onClick.AddListener(() => LoadGame(2));
        loadSlotButton3.onClick.AddListener(() => LoadGame(3));
    }

    public void ShowSaveSlotPanel()
    {
        saveSlotPanel.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void SaveGame(int slotNumber)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            GameState gameState = new GameState
            {
                playerPositionX = player.transform.position.x,
                playerPositionY = player.transform.position.y,
                playerPositionZ = player.transform.position.z
            };
            string json = JsonUtility.ToJson(gameState);

            string path = Application.persistentDataPath + "/saveSlot" + slotNumber + ".json";
            File.WriteAllText(path, json);

            Debug.Log("���� ���� ��Ȳ�� ���� " + slotNumber + "�� ����Ǿ����ϴ�: " + path);
            saveSlotPanel.SetActive(false);
            pauseMenuUI.SetActive(true);
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
            PlayerController player = FindObjectOfType<PlayerController>();

            if (player != null)
            {
                Vector3 newPosition = new Vector3(gameState.playerPositionX, gameState.playerPositionY, gameState.playerPositionZ);
                player.SetPlayerPosition(newPosition);
                Debug.Log("������ ���� " + slotNumber + "���� �ε�Ǿ����ϴ�.");
                Debug.Log("�ε� �� �÷��̾� ��ġ: " + player.transform.position);
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
}