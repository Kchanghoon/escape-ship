using System;
using UnityEngine;
using DG.Tweening;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject StartMainUI;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject confirmExitPanel; // 종료 확인창 UI
    [SerializeField] private GameObject confirmOptionSlotPanel; // 옵션창 UI
    [SerializeField] private GameObject confirmLoadSlotPanel; // 로드 UI
    [SerializeField] private GameObject confirmSelectSlotPanel; // 스테이지 선택 UI
    private bool isPaused = false;
    public GameObject panel;

    private void Start()
    {
        MouseCam.Instance.SetCursorState(false); // 게임 시작 시 마우스 잠금
    }
    // 스테이지 선택 후 StageManager에 연결하여 활성화
    public void SelectStage(int stageIndex)
    {
        Resume(); // 선택 후 메뉴를 닫음
        // StageManager에서 해당 스테이지를 활성화
        StageManager.Instance.ActivateStage(stageIndex); // StageManager의 메서드를 호출
       
    }

    public void Update()
    {
        if (StartMainUI.activeSelf || PauseMenuUI.activeSelf)
        {
            // 메뉴 BGM 등을 처리하는 코드
        }
    }

    public void OnPanelClick()
    {
        panel.transform.SetAsLastSibling(); // 패널을 가장 앞으로 보냄
    }

    public void Resume()
    {
        StartMainUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmOptionSlotPanel.SetActive(false);
        confirmLoadSlotPanel.SetActive(false);
        confirmSelectSlotPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
        MouseCam.Instance.SetCursorState(true);  // 마우스 커서 잠금
    }

    public void showConfirmExitPanel() { confirmExitPanel.SetActive(true); }
    public void showLoadSlotPanel() { confirmLoadSlotPanel.SetActive(true); }
    public void showOptionPanel() { confirmOptionSlotPanel.SetActive(true); }
    public void showStageSelectPanel() { confirmSelectSlotPanel.SetActive(true); }
    public void CancelExit() { confirmExitPanel.SetActive(false); confirmLoadSlotPanel.SetActive(false); confirmOptionSlotPanel.SetActive(false); confirmSelectSlotPanel.SetActive(false); }
    public void QuitGame()
    {
        // 게임 종료 기능
        Application.Quit();
        // 에디터 모드에서 테스트 중인 경우
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
