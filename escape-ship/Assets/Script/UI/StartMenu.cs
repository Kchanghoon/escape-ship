using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject StartMenuUI;  // StartMenuUI Canvas�� �巡���Ͽ� ������ ����
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject confirmExitPanel;//���� ���� Ȯ��â UI
    [SerializeField] GameObject confirmOptionSlotPanel; //  �ɼ�â UI
    [SerializeField] GameObject confirmLoadSlotPanel; // �ε� UI
    private bool isPaused = false;  // ������ �Ͻ����� �������� Ȯ���ϴ� ����
    public GameObject panel;
    //public string continueSoundName;


    public void Update()
    {
        if (StartMenuUI.activeSelf || PauseMenuUI.activeSelf)
        {
            //SoundManager.instance.PlaySound2D("MenuBGM", isLoop: true, type: SoundType.BGM);  // �޴� BGM ���
            SoundManager.instance.StopAllSounds();  // Ư�� �Ҹ� �����ϰ� ��� �Ͻ�����
        }
        else
        {
            SoundManager.instance.ResumeAllSounds();  // ��� �Ҹ� �簳
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
        Cursor.visible = false;     //���콺 Ŀ�� �Ⱥ��̰� 
        Cursor.lockState = CursorLockMode.Locked;  //���콺 Ŀ�� �߾ӿ� ����.
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

    //void PauseAllSoundsExcept(AudioSource continueAudioSource) //�Ҹ� �Ͻ�����
    //{
    //    allAudioSources = FindObjectsOfType<AudioSource>(); // ��� ����� �ҽ� ��������
    //    foreach (AudioSource audioSource in allAudioSources)
    //    {
    //        //if (audioSource != exception)  // Ư�� ����� �ҽ��� ����
    //        {
    //            audioSource.Pause();  // ������ ����� �ҽ��� �Ͻ� ����
    //        }
    //    }
    //}
    //void ResumeAllSounds() // �Ҹ� ���
    //{
    //    foreach (AudioSource audioSource in allAudioSources)
    //    {
    //        audioSource.UnPause(); // �Ҹ� �簳
    //    }
    //}
}
