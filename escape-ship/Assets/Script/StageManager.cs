using UnityEngine;

public class StageManager : MonoBehaviour
{
    // 스테이지를 저장할 배열
    public GameObject[] stages;

    // 플레이어를 저장할 변수
    public GameObject player;

    // 각 스테이지별 플레이어 시작 위치
    public Transform[] playerStartPoints;

    // 특정 스테이지를 활성화하는 함수
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
            player.transform.position = playerStartPoints[stageIndex].position;
            player.transform.rotation = playerStartPoints[stageIndex].rotation;
        }
    }
}
