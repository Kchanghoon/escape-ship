using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject BasepauseMenuUI;
    [SerializeField] private GameObject confirmMenuPanel;
    [SerializeField] private GameObject confirmExitPanel;
    [SerializeField] private GameObject confirmSaveSlotPanel;
    [SerializeField] private GameObject confirmLoadSlotPanel;
    [SerializeField] private GameObject MainMenuUI;

    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private TMP_InputField bgmVolumeText;
    [SerializeField] private TMP_InputField effectVolumeText;
    [SerializeField] private TMP_InputField masterVolumeText;

    [Header("Other Settings")]
    [SerializeField] private GameObject[] uiElements;

    private void Start()
    {
        KeyManager.Instance.keyDic[KeyAction.Setting] += OnSetting;

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);

        bgmVolumeText.onEndEdit.AddListener(delegate { OnBGMTextChange(bgmVolumeText.text); });
        effectVolumeText.onEndEdit.AddListener(delegate { OnEffectTextChange(effectVolumeText.text); });
        masterVolumeText.onEndEdit.AddListener(delegate { OnMasterTextChange(masterVolumeText.text); });

        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.75f);
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);

        UpdateTextFields();
    }

    private void UpdateTextFields()
    {
        bgmVolumeText.text = Mathf.Round(bgmSlider.value * 100).ToString();
        effectVolumeText.text = Mathf.Round(effectSlider.value * 100).ToString();
        masterVolumeText.text = Mathf.Round(masterSlider.value * 100).ToString();
    }

    public void SetBGMVolume(float value)
    {
        float volume = (value > 0.0001f) ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("BGMVolume", volume);
        PlayerPrefs.SetFloat("BGMVolume", value);
        UpdateTextFields();
    }

    public void SetEffectVolume(float value)
    {
        float volume = (value > 0.0001f) ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("EffectVolume", volume);
        PlayerPrefs.SetFloat("EffectVolume", value);
        UpdateTextFields();
    }

    public void SetMasterVolume(float value)
    {
        float volume = (value > 0.0001f) ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", value);
        UpdateTextFields();
    }

    private void OnBGMTextChange(string newValue)
    {
        if (float.TryParse(newValue, out float result))
        {
            result = Mathf.Clamp(result / 100f, 0.0001f, 1f);
            bgmSlider.value = result;
            SetBGMVolume(result);
        }
    }

    private void OnEffectTextChange(string newValue)
    {
        if (float.TryParse(newValue, out float result))
        {
            result = Mathf.Clamp(result / 100f, 0.0001f, 1f);
            effectSlider.value = result;
            SetEffectVolume(result);
        }
    }

    private void OnMasterTextChange(string newValue)
    {
        if (float.TryParse(newValue, out float result))
        {
            result = Mathf.Clamp(result / 100f, 0.0001f, 1f);
            masterSlider.value = result;
            SetMasterVolume(result);
        }
    }

    private void OnSetting()
    {
        bool newPauseState = !GameManager.Instance.isPause;
        if (AreAllUIElementsInactive())
        {
            GameManager.Instance.SetPause(newPauseState); // GameManager의 일시정지 상태를 설정
            GameManager.Instance.SetSetting(newPauseState); // 설정 메뉴 상태도 업데이트
            BasepauseMenuUI.SetActive(newPauseState); // UI 표시
        }
    }

    private bool AreAllUIElementsInactive()
    {
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement.activeSelf)
            {
                Debug.Log(uiElement.name + " is active.");
                return false;
            }
        }
        return true;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.SetPause(false);

        BasepauseMenuUI.SetActive(false);
        confirmMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmSaveSlotPanel.SetActive(false);
        confirmLoadSlotPanel.SetActive(false);
        MainMenuUI.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        // 세이브 로직 추가
    }

    public void ClickLoadButton(int loadIndex)
    {
        // 로드 로직 추가
    }
}
