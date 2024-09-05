using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // PauseMenu�� Canvas�� �巡���Ͽ� ������ ����
    public GameObject confirmMenuPanel; //�޴� Ȯ��â UI 
    public GameObject confirmExitPanel;//���� ���� Ȯ��â UI
    public GameObject confirmSaveSlotPanel;
    public GameObject confirmLoadSlotPanel;
    public AudioSource[] allAudioSources;
    public GameObject MainMenuUI;
    private bool isPaused = false;  // ������ �Ͻ����� �������� Ȯ���ϴ� ����

    void Update()
    {
        // Esc Ű �Է� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();  // �̹� �Ͻ����� ���¶�� �簳
                ResumeAllSounds(); // �Ҹ� ���
            }
            else
            {
                Pause();  // �Ͻ����� ���°� �ƴ϶�� �Ͻ�����
                PauseAllSounds(); //�Ҹ��� ����
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // UI �����
        confirmMenuPanel.SetActive(false); //�޴� Ȯ��â �����
        confirmExitPanel.SetActive(false); //������ Ȯ��â �����
        confirmSaveSlotPanel.SetActive(false); //���̺�â �����
        confirmLoadSlotPanel.SetActive(false); // �ε�â �����
        Time.timeScale = 1f;  // ���� �ð� �簳
        isPaused = false;  // �Ͻ����� ���� ����
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);  // UI ǥ��
        Time.timeScale = 0f;  // ���� �ð� ����
        isPaused = true;  // �Ͻ����� ���·� ����
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
        pauseMenuUI.SetActive(false);  // UI �����
        confirmMenuPanel.SetActive(false); //�޴� Ȯ��â �����
        confirmExitPanel.SetActive(false); //������ Ȯ��â �����
        confirmSaveSlotPanel.SetActive(false); //���̺�â �����
        confirmLoadSlotPanel.SetActive(false); // �ε�â �����
        MainMenuUI.SetActive(true);

    }

    public void CancelExit()
    {
        confirmMenuPanel.SetActive(false);  // Ȯ�� â �����
        confirmExitPanel.SetActive(false); // ������ â �����
        confirmLoadSlotPanel.SetActive(false);
        confirmSaveSlotPanel.SetActive(false);
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

    void PauseAllSounds() //�Ҹ� �Ͻ�����
    {
        allAudioSources = FindObjectsOfType<AudioSource>(); // ��� ����� �ҽ� ��������
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Pause(); // �Ҹ� �Ͻ� ����
        }
    }
    void ResumeAllSounds() // thfl worp 
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.UnPause(); // �Ҹ� �簳
        }
    }
}