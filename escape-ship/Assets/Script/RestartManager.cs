using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : Singleton<RestartManager>
{
    [Header("Panel Settings")]
    [SerializeField] private GameObject DeadPanel;

    [Header("Audio Settings")]
    [SerializeField] AudioSource BTNClick;

    [Header("Player Settings")]
    [SerializeField] private PlayerState playerState;

    // 사망한 스테이지 인덱스를 저장할 변수 추가
    private int lastStageIndex = 0;

    public void ShowDeadPanel()
    {
        DeadPanel.SetActive(true);
    }

    // 사망 시 현재 스테이지를 저장하는 메서드
    public void SaveCurrentStage()
    {
        lastStageIndex = StageManager.Instance.GetCurrentStageIndex();
        Debug.Log($"Current stage saved as {lastStageIndex}");
    }

    public void SelectStage(int stageIndex = -1)
    {
        // stageIndex가 -1이면 마지막 사망한 스테이지 인덱스를 사용
        if (stageIndex == -1)
        {
            stageIndex = lastStageIndex;
        }

        Debug.Log($"Restarting at stage index: {stageIndex}");

        DeadPanel.SetActive(false);
        BTNClick.Play();

        if (playerState != null)
        {
            playerState.IncreaseOxygen(playerState.MaxOxygen - playerState.Oxygen);
            playerState.DecreaseStress(playerState.Stress);
        }

        GameManager.Instance.HideMouse();
        // 마지막 사망한 스테이지 또는 선택한 스테이지로 활성화
        StageManager.Instance.ActivateStage(stageIndex);
    }

    public void SendRestart()
    {
        SelectStage();
    }

    public void MainMenu()
    {
        GameManager.Instance.ShowMouse();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
