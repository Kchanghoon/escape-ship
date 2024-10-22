using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro; // TextMeshPro�� ����ϱ� ���� �߰�

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] GameObject BasepauseMenuUI;  // PauseMenu�� Canvas�� �巡���Ͽ� ������ ����
    [SerializeField] GameObject confirmMenuPanel; //�޴� Ȯ��â UI 
    [SerializeField] GameObject confirmExitPanel;//���� ���� Ȯ��â UI
    [SerializeField] GameObject confirmSaveSlotPanel;
    [SerializeField] GameObject confirmLoadSlotPanel;
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] private AudioMixer audioMixer; // AudioMixer ����
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider masterSlider;  // Master Volume �����̴�

    [SerializeField] private TMP_InputField bgmVolumeText; // BGM ���� �ؽ�Ʈ �ʵ�
    [SerializeField] private TMP_InputField effectVolumeText; // ȿ���� ���� �ؽ�Ʈ �ʵ�
    [SerializeField] private TMP_InputField masterVolumeText; // Master Volume �ؽ�Ʈ �ʵ�

    private bool isPaused = false;  // ������ �Ͻ����� �������� Ȯ���ϴ� ����
    [SerializeField] private GameObject[] uiElements; // Ȯ���� UI ��ҵ��� �迭�� ����

    private void Start()
    {
        KeyManager.Instance.keyDic[KeyAction.Setting] += OnSetting;

        // �����̴��� ���� ����� ������ ȣ��� �޼��� �߰�
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);

        // �ؽ�Ʈ �ʵ� ���� ����� ������ ȣ��� �޼��� �߰�
        bgmVolumeText.onEndEdit.AddListener(delegate { OnBGMTextChange(bgmVolumeText.text); });
        effectVolumeText.onEndEdit.AddListener(delegate { OnEffectTextChange(effectVolumeText.text); });
        masterVolumeText.onEndEdit.AddListener(delegate { OnMasterTextChange(masterVolumeText.text); });

        // ���� �� �����̴� �� �ʱ�ȭ (����� ������ �ҷ����� �Ǵ� �⺻�� ����)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f); // �⺻�� 0.75
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.75f); // �⺻�� 0.75
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);

        // �ؽ�Ʈ �ʵ嵵 �ʱ�ȭ
        UpdateTextFields();
    }

    // �����̴� ���� �� �ؽ�Ʈ �ʵ� ������Ʈ
    private void UpdateTextFields()
    {
        bgmVolumeText.text = Mathf.Round(bgmSlider.value * 100).ToString(); // �����̴� �� �ۼ�Ʈ�� ��ȯ
        effectVolumeText.text = Mathf.Round(effectSlider.value * 100).ToString();
        masterVolumeText.text = Mathf.Round(masterSlider.value * 100).ToString();
    }

    // BGM ������ �����ϴ� �Լ�
    public void SetBGMVolume(float value)
    {
        float volume = (value > 0.0001f) ? Mathf.Log10(value) * 20 : -80f; // 0�̸� -80dB�� ����
        audioMixer.SetFloat("BGMVolume", volume);
        PlayerPrefs.SetFloat("BGMVolume", value);
        UpdateTextFields(); // �����̴� ���� �� �ؽ�Ʈ �ʵ嵵 ������Ʈ
    }

    // ȿ���� ������ �����ϴ� �Լ�
    public void SetEffectVolume(float value)
    {
        float volume = (value > 0.0001f) ? Mathf.Log10(value) * 20 : -80f; // 0�̸� -80dB�� ����
        audioMixer.SetFloat("EffectVolume", volume);
        PlayerPrefs.SetFloat("EffectVolume", value);
        UpdateTextFields(); // �����̴� ���� �� �ؽ�Ʈ �ʵ嵵 ������Ʈ
    }

    // Master ������ �����ϴ� �Լ�
    public void SetMasterVolume(float value)
    {
        float volume = (value > 0.0001f) ? Mathf.Log10(value) * 20 : -80f; // 0�̸� -80dB�� ����
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", value);
        UpdateTextFields(); // �����̴� ���� �� �ؽ�Ʈ �ʵ嵵 ������Ʈ
    }


    // �ؽ�Ʈ �Է����� BGM ���� ����
    private void OnBGMTextChange(string newValue)
    {
        if (float.TryParse(newValue, out float result))
        {
            result = Mathf.Clamp(result / 100f, 0.0001f, 1f); // 0 ~ 100 ������ 0 ~ 1�� ��ȯ
            bgmSlider.value = result;  // �����̴� ����
            SetBGMVolume(result);  // ����� ���� ����
        }
    }

    // �ؽ�Ʈ �Է����� ȿ���� ���� ����
    private void OnEffectTextChange(string newValue)
    {
        if (float.TryParse(newValue, out float result))
        {
            result = Mathf.Clamp(result / 100f, 0.0001f, 1f); // 0 ~ 100 ������ 0 ~ 1�� ��ȯ
            effectSlider.value = result;
            SetEffectVolume(result);
        }
    }

    // �ؽ�Ʈ �Է����� Master ���� ����
    private void OnMasterTextChange(string newValue)
    {
        if (float.TryParse(newValue, out float result))
        {
            result = Mathf.Clamp(result / 100f, 0.0001f, 1f); // 0 ~ 100 ������ 0 ~ 1�� ��ȯ
            masterSlider.value = result;
            SetMasterVolume(result);
        }
    }

    private void OnSetting()
    {
        if (AreAllUIElementsInactive()) Pause(!isPaused);
    }

    private bool AreAllUIElementsInactive()
    {
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement.activeSelf) // �ϳ��� Ȱ��ȭ�Ǿ� ������ false ��ȯ
            {
                Debug.Log(uiElement.name + " is active.");
                return false;
            }
        }
        return true; // ��� UI�� ��Ȱ��ȭ�Ǿ� ������ true ��ȯ
    }

    void Pause(bool isPause)
    {
        BasepauseMenuUI.SetActive(isPause);  // UI ǥ��
        Time.timeScale = isPause ? 0f : 1;  // ���� �ð� ����
        isPaused = isPause;  // �Ͻ����� ���·� ����
        Cursor.visible = isPause;
        Cursor.lockState = isPause ? CursorLockMode.None : CursorLockMode.Locked;
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

    public void showLoadSlotPanel()
    {
        confirmLoadSlotPanel.SetActive(true);
    }

    public void Menu()
    {
        BasepauseMenuUI.SetActive(false);
        confirmMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmSaveSlotPanel.SetActive(false);
        confirmLoadSlotPanel.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void CancelExit()
    {
        confirmMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmLoadSlotPanel.SetActive(false);
        confirmSaveSlotPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ClickSaveButton(int saveIndex)
    {
        // ���̺� ���� �߰�
    }

    public void ClickLoadButton(int loadIndex)
    {
        // �ε� ���� �߰�
    }
}
