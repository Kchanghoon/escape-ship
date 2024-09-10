using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public GameObject StartMenuUI;  // StartMenuUI Canvas�� �巡���Ͽ� ������ ����
    public GameObject confirmExitPanel;//���� ���� Ȯ��â UI
    public GameObject confirmOptionSlotPanel; //  �ɼ�â UI
    public GameObject confirmLoadSlotPanel; // �ε� UI
    private bool isPaused = false;  // ������ �Ͻ����� �������� Ȯ���ϴ� ����
    public GameObject panel;
    public AudioSource continueAudioSource;
    public AudioSource[] allAudioSources; //����� ���� ����

   
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
        panel.transform.SetAsLastSibling(); // �г��� ���� ������ ����
    }


    public void Resume()
    {
        StartMenuUI.SetActive(false);  // UI �����
 
        confirmExitPanel.SetActive(false); //������ Ȯ��â �����
        confirmOptionSlotPanel.SetActive(false); //���̺�â �����
        confirmLoadSlotPanel.SetActive(false); // �ε�â �����
        Time.timeScale = 1f;  // ���� �ð� �簳
        isPaused = false;  // �Ͻ����� ���� ����
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
        confirmExitPanel.SetActive(false); // ������ â �����
        confirmLoadSlotPanel.SetActive(false);
        confirmOptionSlotPanel.SetActive(false);
    }
    public void QuitGame()
    {
        // ���� ���� ���
        Application.Quit();
        // ������ ��忡�� �׽�Ʈ ���� ���
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void PauseAllSoundsExcept(AudioSource continueAudioSource) //�Ҹ� �Ͻ�����
    {
        allAudioSources = FindObjectsOfType<AudioSource>(); // ��� ����� �ҽ� ��������
        foreach (AudioSource audioSource in allAudioSources)
        {
            //if (audioSource != exception)  // Ư�� ����� �ҽ��� ����
            {
                audioSource.Pause();  // ������ ����� �ҽ��� �Ͻ� ����
            }
        }
    }
    void ResumeAllSounds() // �Ҹ� ���
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.UnPause(); // �Ҹ� �簳
        }
    }
}
