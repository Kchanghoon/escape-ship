using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    // 스테이지 선택 버튼들
    public Button stageButton1;
    public Button stageButton2;
    public Button stageButton3;
    // 추가적으로 필요한 버튼들을 위와 같이 선언

    void Start()
    {
        // 각 버튼에 직접 할당된 스테이지 선택 이벤트 추가
        if (stageButton1 != null)
        {
            stageButton1.onClick.AddListener(() => SelectStage(0));  // 첫 번째 스테이지 선택
        }
        if (stageButton2 != null)
        {
            stageButton2.onClick.AddListener(() => SelectStage(1));  // 두 번째 스테이지 선택
        }
        if (stageButton3 != null)
        {
            stageButton3.onClick.AddListener(() => SelectStage(2));  // 세 번째 스테이지 선택
        }
    }

    public void SelectStage(int stageIndex)
    {
        Debug.Log($"스테이지 {stageIndex} 선택됨.");
        // StageManager 싱글톤을 통해 스테이지 활성화
        StageManager.Instance.ActivateStage(stageIndex);
    }
}
