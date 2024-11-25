using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    // 스테이지를 저장할 배열
    public GameObject[] stages;

    // 플레이어를 저장할 변수
    public GameObject player;

    // 각 스테이지별 플레이어 시작 위치
    public Transform[] playerStartPoints;

    // 현재 활성화된 스테이지 인덱스 저장 변수
    private int currentStageIndex = -1;

    public PlayerState playerstate;
    public InventoryUIExmaple inventoryuiexample;

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

            // 현재 활성화된 스테이지 인덱스 업데이트
            currentStageIndex = stageIndex;

            // 플레이어를 해당 스테이지의 시작 위치로 이동
            if (player != null)
            {
                CharacterController characterController = player.GetComponent<CharacterController>();

                if (characterController != null)
                {
                    // CharacterController가 있는 경우, Transform을 통해 위치 이동
                    characterController.enabled = false;  // 이동하기 전에 CharacterController를 비활성화
                    player.transform.position = playerStartPoints[stageIndex].position;
                    player.transform.rotation = playerStartPoints[stageIndex].rotation;
                    characterController.enabled = true;  // 위치 설정 후 다시 CharacterController 활성화
                }
                else
                {
                    // CharacterController가 없을 때는 Transform을 통해 이동
                    player.transform.position = playerStartPoints[stageIndex].position;
                    player.transform.rotation = playerStartPoints[stageIndex].rotation;
                }
                playerstate.ResetState();
                inventoryuiexample.ResetInventory();
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

    // 현재 활성화된 스테이지 인덱스를 반환하는 메서드
    public int GetCurrentStageIndex()
    {
        return currentStageIndex;
    }
}
