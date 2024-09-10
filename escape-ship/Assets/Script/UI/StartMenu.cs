using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public GameObject StartMenuUI;  // StartMenuUI Canvas를 드래그하여 연결할 변수
    public GameObject confirmExitPanel;//게임 종료 확인창 UI
    public GameObject confirmOptionSlotPanel; //  옵션창 UI
    public GameObject confirmLoadSlotPanel; // 로드 UI
    private bool isPaused = false;  // 게임이 일시정지 상태인지 확인하는 변수
    public GameObject panel;
    public AudioSource continueAudioSource;
    public AudioSource[] allAudioSources; //오디오 사운드 관리

   
    public void Start()
    {
        if (StartMenuUI.activeSelf)
        {
            PauseAllSoundsExcept(continueAudioSource);
          
        }
        else
        {
            ResumeAllSounds();
        }
    }

    public void OnPanelClick()
    {
        panel.transform.SetAsLastSibling(); // 패널을 가장 앞으로 보냄
    }


    public void Resume()
    {
        StartMenuUI.SetActive(false);  // UI 숨기기
 
        confirmExitPanel.SetActive(false); //나가기 확인창 숨기기
        confirmOptionSlotPanel.SetActive(false); //세이브창 숨기기
        confirmLoadSlotPanel.SetActive(false); // 로드창 숨기기
        Time.timeScale = 1f;  // 게임 시간 재개
        isPaused = false;  // 일시정지 상태 해제
       Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
 


    public void showConfirmExitPanel()
    {
        confirmExitPanel.SetActive(true);
    }


    public void showLoadSlotPanel()
    {
        confirmLoadSlotPanel.SetActive(true);
    }

    public void showOptionPanel()
    {
        confirmOptionSlotPanel.SetActive(true);
    }

    public void CancelExit()
    {
        confirmExitPanel.SetActive(false); // 나가기 창 숨기기
        confirmLoadSlotPanel.SetActive(false);
        confirmOptionSlotPanel.SetActive(false);
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

    void PauseAllSoundsExcept(AudioSource continueAudioSource) //소리 일시정지
    {
        allAudioSources = FindObjectsOfType<AudioSource>(); // 모든 오디오 소스 가져오기
        foreach (AudioSource audioSource in allAudioSources)
        {
            //if (audioSource != exception)  // 특정 오디오 소스는 제외
            {
                audioSource.Pause();  // 나머지 오디오 소스는 일시 정지
            }
        }
    }
    void ResumeAllSounds() // 소리 재생
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.UnPause(); // 소리 재개
        }
    }
}
