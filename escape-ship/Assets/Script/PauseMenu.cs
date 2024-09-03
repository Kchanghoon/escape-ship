using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // PauseMenu의 Canvas를 드래그하여 연결할 변수
    public GameObject confirmMenuPanel; //메뉴 확인창 UI 
    public GameObject confirmExitPanel;//게임 종료 확인창 UI
    public GameObject confirmSaveSlotPanel;
    public GameObject confirmLoadSlotPanel;
    private bool isPaused = false;  // 게임이 일시정지 상태인지 확인하는 변수

    void Update()
    {
        // Esc 키 입력 감지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();  // 이미 일시정지 상태라면 재개
            }
            else
            {
                Pause();  // 일시정지 상태가 아니라면 일시정지
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // UI 숨기기
        confirmMenuPanel.SetActive(false); //메뉴 확인창 숨기기
        confirmExitPanel.SetActive(false); //나가기 확인창 숨기기
        Time.timeScale = 1f;  // 게임 시간 재개
        isPaused = false;  // 일시정지 상태 해제
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);  // UI 표시
        Time.timeScale = 0f;  // 게임 시간 정지
        isPaused = true;  // 일시정지 상태로 설정
    }

    public void showConfirmMenuPanel()
    {
        confirmMenuPanel.SetActive(true);
    }
    public void showConfirmExitPanel()
    {
        confirmExitPanel.SetActive(true);
    }
    public void showSaveSlotPanel()
    {
        confirmSaveSlotPanel.SetActive(true);
    }
    public void showLoadSlotPanel() {
        confirmLoadSlotPanel.SetActive(true);
        }
    public void Menu()
    {
        Time.timeScale = 1f; //게임 시간 돌리기
        SceneManager.LoadScene("Main"); // 메인 신 로드
    }

    public void CancelExit()
    {
        confirmMenuPanel.SetActive(false);  // 확인 창 숨기기
        confirmExitPanel.SetActive(false); // 나가기 창 숨기기
        confirmLoadSlotPanel.SetActive(false);
        confirmSaveSlotPanel.SetActive(false);
    }
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