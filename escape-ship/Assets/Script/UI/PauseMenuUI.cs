using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;  // PauseMenu�� Canvas�� �巡���Ͽ� ������ ����
    [SerializeField] GameObject confirmMenuPanel; //�޴� Ȯ��â UI 
    [SerializeField] GameObject confirmExitPanel;//���� ���� Ȯ��â UI
    [SerializeField] GameObject confirmSaveSlotPanel;
    [SerializeField] GameObject confirmLoadSlotPanel;
    //[SerializeField] AudioSource[] allAudioSources;
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] private AudioMixer audioMixer; // AudioMixer ����
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider masterSlider;  // Master Volume �����̴�
    private bool isPaused = false;  // ������ �Ͻ����� �������� Ȯ���ϴ� ����

    private void Start()
    {
        KeyManager.Instance.keyDic[KeyAction.Setting] += OnSetting;

        // �����̴��� ���� ����� ������ ȣ��� �޼��� �߰�
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);

        // ���� �� �����̴� ���� �ʱ�ȭ (����� ������ �ҷ����� �Ǵ� �⺻�� ����)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f); // �⺻�� 0.75
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.75f); // �⺻�� 0.75
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);

    }

    // BGM ������ �����ϴ� �Լ�
    public void SetBGMVolume(float value)
    {
        // AudioMixer�� ������ ���ú�(dB)�� ó���ϹǷ�, 0~1 ���� -80dB���� 0dB�� ��ȯ
        float volume = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("BGMVolume", volume);

        // ������ ����
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    // ȿ���� ������ �����ϴ� �Լ�
    public void SetEffectVolume(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("EffectVolume", volume);

        // ������ ����
        PlayerPrefs.SetFloat("EffectVolume", value);
    }

    // ȿ���� ������ �����ϴ� �Լ�
    public void SetMasterVolume(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("MasterVolume", volume);

        // ������ ����
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    private void OnSetting()
    {
        Pause(!isPaused);
        //if (isPaused) Resume();  // �̹� �Ͻ����� ���¶�� �簳
        //else Pause(!isPaused);  // �Ͻ����� ���°� �ƴ϶�� �Ͻ�����
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    //    SoundManager.instance.ResumeAllSounds();
    }

    void Pause(bool isPuase)
    {
        pauseMenuUI.SetActive(isPuase);  // UI ǥ��
        Time.timeScale = isPuase? 0f : 1;  // ���� �ð� ����
        isPaused = isPuase;  // �Ͻ����� ���·� ����
        Cursor.visible = isPuase;
        Cursor.lockState = isPuase? CursorLockMode.None : CursorLockMode.Locked;

      /*  if(isPuase) SoundManager.instance.StopAllSounds();
        else
        {
            confirmMenuPanel.SetActive(false); //�޴� Ȯ��â �����
            confirmExitPanel.SetActive(false); //������ Ȯ��â �����
            confirmSaveSlotPanel.SetActive(false); //���̺�â �����
            confirmLoadSlotPanel.SetActive(false); // �ε�â �����
        }
      */
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


    public void ClickSaveButton(int saveIndex)
    {

    }

    public void ClickLoadButton(int loadIndex)
    {

    }
}