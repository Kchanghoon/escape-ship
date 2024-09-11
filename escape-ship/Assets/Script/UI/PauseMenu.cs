using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // PauseMenu의 Canvas를 드래그하여 연결할 변수
    public GameObject confirmMenuPanel; //메뉴 확인창 UI 
    public GameObject confirmExitPanel;//게임 종료 확인창 UI
    public GameObject confirmSaveSlotPanel;
    public GameObject confirmLoadSlotPanel;
    public AudioSource[] allAudioSources;
    public GameObject MainMenuUI;
    private bool isPaused = false;  // 게임이 일시정지 상태인지 확인하는 변수

    void Update()
    {
        // Esc 키 입력 감지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();  // 이미 일시정지 상태라면 재개
                ResumeAllSounds(); // 소리 재게
            }
            else
            {
                Pause();  // 일시정지 상태가 아니라면 일시정지
                
                PauseAllSounds(); //소리도 정지
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // UI 숨기기
        confirmMenuPanel.SetActive(false); //메뉴 확인창 숨기기
        confirmExitPanel.SetActive(false); //나가기 확인창 숨기기
        confirmSaveSlotPanel.SetActive(false); //세이브창 숨기기
        confirmLoadSlotPanel.SetActive(false); // 로드창 숨기기
        Time.timeScale = 1f;  // 게임 시간 재개
        isPaused = false;  // 일시정지 상태 해제
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);  // UI 표시
        Time.timeScale = 0f;  // 게임 시간 정지
        isPaused = true;  // 일시정지 상태로 설정
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        pauseMenuUI.SetActive(false);  // UI 숨기기
        confirmMenuPanel.SetActive(false); //메뉴 확인창 숨기기
        confirmExitPanel.SetActive(false); //나가기 확인창 숨기기
        confirmSaveSlotPanel.SetActive(false); //세이브창 숨기기
        confirmLoadSlotPanel.SetActive(false); // 로드창 숨기기
        MainMenuUI.SetActive(true);

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

    void PauseAllSounds() //소리 일시정지
    {
        allAudioSources = FindObjectsOfType<AudioSource>(); // 모든 오디오 소스 가져오기
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Pause(); // 소리 일시 정지
        }
    }
    void ResumeAllSounds() // thfl worp 
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.UnPause(); // 소리 재개
        }
    }
}