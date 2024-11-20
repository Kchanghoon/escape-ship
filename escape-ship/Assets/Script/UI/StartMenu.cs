using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class StartMenu : MonoBehaviour
{
    // Main UI 패널들
    [Header("Main UI Panels")]
    [SerializeField] private GameObject StartMainUI;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject confirmExitPanel;
    [SerializeField] private GameObject confirmOptionSlotPanel;
    [SerializeField] private GameObject confirmSelectSlotPanel;
    [SerializeField] private GameObject confrimKeyBindingPanel;

    // 플레이어 설정
    [Header("Player Settings")]
    [SerializeField] private PlayerState playerState;

    //// 오디오 설정
    [Header("Audio Settings")]
    [SerializeField] AudioSource BTNClick;  // 문이 열릴 때 재생할 소리

    public GameObject panel;


    public void ShowMainUI()
    {
        StartMainUI.SetActive(true);
        GameManager.Instance.ShowMouse();
        BTNClick.Play();
    }

    public void HideMainUI()
    {
        StartMainUI.SetActive(false);
        GameManager.Instance.HideMouse();
        BTNClick.Play();
    }

    // 스테이지 선택 후 StageManager에 연결하여 활성화
    public void SelectStage(int stageIndex)
    {
        // StartMenu 및 기타 UI 비활성화
        StartMainUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmOptionSlotPanel.SetActive(false);
        confirmSelectSlotPanel.SetActive(false);
        confrimKeyBindingPanel.SetActive(false);
        BTNClick.Play();
        // 플레이어 상태 초기화
        if (playerState != null)
        {
            playerState.IncreaseOxygen(playerState.MaxOxygen - playerState.Oxygen);
            playerState.DecreaseStress(playerState.Stress);
        }

        GameManager.Instance.HideMouse();

        // 스테이지 활성화
        StageManager.Instance.ActivateStage(stageIndex);
    }

    public void OnPanelClick()
    {
        panel.transform.SetAsLastSibling(); // 패널을 가장 앞으로 보냄
    }

    public void Resume()
    {
        HideMainUI();
        PauseMenuUI.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmOptionSlotPanel.SetActive(false);
        confirmSelectSlotPanel.SetActive(false);
        confrimKeyBindingPanel.SetActive(false);
        BTNClick.Play();
        if (playerState != null)
        {
            playerState.IncreaseOxygen(playerState.MaxOxygen - playerState.Oxygen);
            playerState.DecreaseStress(playerState.Stress);
        }

        GameManager.Instance.HideMouse();
    }

    public void showConfirmExitPanel() { confirmExitPanel.SetActive(true); BTNClick.Play(); }
    public void showOptionPanel() { confirmOptionSlotPanel.SetActive(true); BTNClick.Play(); }
    public void showStageSelectPanel() { confirmSelectSlotPanel.SetActive(true); BTNClick.Play(); }
    public void CancelExit() 
         {   
            confirmExitPanel.SetActive(false);  
            confirmOptionSlotPanel.SetActive(false); 
            confirmSelectSlotPanel.SetActive(false);
            BTNClick.Play();
          }

    public void QuitGame()
    {
        // 게임 종료 기능
        Application.Quit();
        BTNClick.Play();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
