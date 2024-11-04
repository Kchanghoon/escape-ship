using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class PauseMenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject BasepauseMenuUI;
    [SerializeField] private GameObject confirmMenuPanel;
    [SerializeField] private GameObject confirmExitPanel;
    [SerializeField] private GameObject MainMenuUI;

    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private TMP_InputField bgmVolumeText;
    [SerializeField] private TMP_InputField effectVolumeText;
    [SerializeField] private TMP_InputField masterVolumeText;
    [SerializeField] AudioSource BTNClick;  // 문이 열릴 때 재생할 소리

    [Header("Other Settings")]
    [SerializeField] private GameObject[] uiElements;

    private bool isPause;

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
        // uiElements 배열 내에서 활성화된 UI 요소가 있는지 확인
        bool isAnyUIActive = uiElements.Any(x => x.activeInHierarchy);

        if (isPause == false) // 게임이 일시정지 상태가 아니고, 다른 UI가 활성화되지 않은 경우에만 실행
        {
            Show();
            GameManager.Instance.isSetting = true;
            BasepauseMenuUI.SetActive(true);
            isPause = true;
        }
        else // 게임이 일시정지 상태이며, 다른 UI가 활성화된 경우에만 실행
        {
            if (!isAnyUIActive)
            {
               Hide();
            }
            GameManager.Instance.isSetting = false;
            BasepauseMenuUI.SetActive(false);
            isPause = false;
        }
    }

    private void Show()
    {
        GameManager.Instance.ShowMouse();
    }

    private void Hide()
    {
        GameManager.Instance.HideMouse();
    }



    public void showConfirmMenuPanel()
    {
        confirmMenuPanel.SetActive(true);
        BTNClick.Play();
    }

    public void showConfirmExitPanel()
    {
        confirmExitPanel.SetActive(true);
        BTNClick.Play();
    }



    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.ShowMouse();
        //GameManager.Instance.SetPause(false);
        BTNClick.Play();
        BasepauseMenuUI.SetActive(false);
        confirmMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        MainMenuUI.SetActive(true);

    }

    public void CancelExit()
    {
        confirmMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        BTNClick.Play();
    }

    public void QuitGame()
    {
        BTNClick.Play();
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
