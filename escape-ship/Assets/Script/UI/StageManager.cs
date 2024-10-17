using UnityEngine;

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

            // 플레이어를 해당 스테이지의 시작 위치로 이동
            if (player != null)
            {
                // 스테이지 시작 지점 위치를 Vector3로 가져오기
                Vector3 startPosition = new Vector3(
                    playerStartPoints[stageIndex].position.x,
                    playerStartPoints[stageIndex].position.y,
                    playerStartPoints[stageIndex].position.z
                );

                // 플레이어의 위치를 시작 지점으로 설정
                player.transform.position = startPosition;
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
