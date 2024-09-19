using UnityEngine;

public class StageManager : MonoBehaviour
{

    void Start()
    {
        ActivateStage(0);  // 기본으로 Stage1을 활성화
    }
    // 스테이지를 저장할 배열
    public GameObject[] stages;

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
        }
    }
}
