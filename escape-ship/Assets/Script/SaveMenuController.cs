using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class SaveMenuController : MonoBehaviour
{
    public GameObject saveSlotPanel;  // SaveSlotPanel UI 패널
    public GameObject pauseMenuUI;    // 일시정지 메뉴 UI
    public Button slotButton1, slotButton2, slotButton3;  // 각 저장 슬롯 버튼
    public Button loadSlotButton1, loadSlotButton2, loadSlotButton3;  // 각 로드 슬롯 버튼

    void Start()
    {
        // 각 슬롯 버튼에 클릭 이벤트 추가
        slotButton1.onClick.AddListener(() => SaveGame(1));
        slotButton2.onClick.AddListener(() => SaveGame(2));
        slotButton3.onClick.AddListener(() => SaveGame(3));

        loadSlotButton1.onClick.AddListener(() => LoadGame(1));
        loadSlotButton2.onClick.AddListener(() => LoadGame(2));
        loadSlotButton3.onClick.AddListener(() => LoadGame(3));
    }

    public void ShowSaveSlotPanel()
    {
        saveSlotPanel.SetActive(true);  // 저장 슬롯 UI 표시
        pauseMenuUI.SetActive(false);   // 일시정지 메뉴 UI 숨기기
    }

    public void SaveGame(int slotNumber)
    {
        Player player = FindObjectOfType<Player>(); // Player 오브젝트 찾기
        if (player != null)
        {
            GameState gameState = player.GetGameState(); // 플레이어의 현재 상태 가져오기
            string json = JsonUtility.ToJson(gameState); // 객체를 JSON 형식으로 직렬화

            // JSON을 파일로 저장
            string path = Application.persistentDataPath + "/saveSlot" + slotNumber + ".json";
            File.WriteAllText(path, json);

            Debug.Log("게임 진행 상황이 슬롯 " + slotNumber + "에 저장되었습니다: " + path);
            saveSlotPanel.SetActive(false);  // 저장 슬롯 UI 숨기기
            pauseMenuUI.SetActive(true);     // 일시정지 메뉴 UI 표시
        }
        else
        {
            Debug.LogWarning("Player 오브젝트를 찾을 수 없습니다.");
        }
    }

    public void LoadGame(int slotNumber)
    {
        string path = Application.persistentDataPath + "/saveSlot" + slotNumber + ".json";
        Debug.Log("파일 경로: " + path);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("읽은 JSON 데이터: " + json);

            GameState gameState = JsonUtility.FromJson<GameState>(json);
            Player player = FindObjectOfType<Player>();

            if (player != null)
            {
                player.SetGameState(gameState);
                Debug.Log("게임이 슬롯 " + slotNumber + "에서 로드되었습니다:");
                Debug.Log("플레이어 점수: " + gameState.playerScore);
                Debug.Log("플레이어 위치: (" + gameState.playerPositionX + ", " + gameState.playerPositionY + ", " + gameState.playerPositionZ + ")");
                Debug.Log("로드 후 플레이어 위치: " + player.transform.position); // 디버깅 로그 추가
            }
            else
            {
                Debug.LogWarning("Player 오브젝트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("슬롯 " + slotNumber + "에 저장된 게임이 없습니다.");
        }
    }
    public void SetGameState(GameState gameState)
    {
      //  score = gameState.playerScore;
        transform.position = new Vector3(gameState.playerPositionX, gameState.playerPositionY, gameState.playerPositionZ);
    }
}
