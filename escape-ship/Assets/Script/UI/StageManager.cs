using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    // 스테이지를 저장할 배열
    public GameObject[] stages;

    // 플레이어를 저장할 변수
    public GameObject player;

    // 각 스테이지별 플레이어 시작 위치
    public Transform[] playerStartPoints;

    public void ActivateStage(int stageIndex)
    {
        // 모든 스테이지 비활성화
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].SetActive(false);
        }

        // 선택된 스테이지만 활성화
        if (stageIndex >= 0 && stageIndex < stages.Length)
        {
            stages[stageIndex].SetActive(true);

            // 모든 스테이지 비활성화
            foreach (GameObject stage in stages)
            {
                stage.SetActive(false);
            }

            if (stageIndex >= 0 && stageIndex < stages.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                stages[stageIndex].SetActive(true);

                // 플레이어를 해당 스테이지의 시작 위치로 이동
                if (player != null)
                {
                    // 플레이어 위치와 회전을 스테이지 시작 위치로 설정
                    player.transform.position = playerStartPoints[stageIndex].position;
                    player.transform.rotation = playerStartPoints[stageIndex].rotation;

                    Debug.Log($"플레이어가 {stageIndex} 스테이지의 시작 위치로 이동했습니다. 현재 위치: {player.transform.position}");
                }
                else
                {
                    Debug.LogWarning("Player 오브젝트가 존재하지 않습니다.");
                }
            }
            else
            {
                Debug.LogError("잘못된 스테이지 인덱스입니다.");
            }
        }
    }
}
