using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class StartMenu : MonoBehaviour
{
    // Main UI 패널들
    [Header("Main UI Panels")]
    [SerializeField] private GameObject StartMainUI;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject confirmExitPanel;
    [SerializeField] private GameObject confirmOptionSlotPanel;
    [SerializeField] private GameObject confirmSelectSlotPanel;

    // 플레이어 설정
    [Header("Player Settings")]
    [SerializeField] private PlayerState playerState;

    // 오디오 설정
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private TMP_InputField bgmVolumeText;
    [SerializeField] private TMP_InputField effectVolumeText;
    [SerializeField] private TMP_InputField masterVolumeText;
    [SerializeField] AudioSource BTNClick;  // 문이 열릴 때 재생할 소리

    public GameObject panel;

    private void Start()
    {

        // 슬라이더 값 변경 이벤트
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);

        // 텍스트 필드 변경 이벤트
        bgmVolumeText.onEndEdit.AddListener(delegate { OnBGMTextChange(bgmVolumeText.text); });
        effectVolumeText.onEndEdit.AddListener(delegate { OnEffectTextChange(effectVolumeText.text); });
        masterVolumeText.onEndEdit.AddListener(delegate { OnMasterTextChange(masterVolumeText.text); });

        // 슬라이더 초기 값 설정
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.75f);
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);

        UpdateTextFields();
    }

    public void ShowMainUI()
    {
        StartMainUI.SetActive(true);
        GameManager.Instance.ShowMouse();
        BTNClick.Play();
    }

    public void HideMainUI()
    {
        StartMainUI.SetActive(false);
        GameManager.Instance.HideMouse();
        BTNClick.Play();
    }

    // 슬라이더 변경 시 텍스트 필드 업데이트
    private void UpdateTextFields()
    {
        bgmVolumeText.text = Mathf.Round(bgmSlider.value * 100).ToString();
        effectVolumeText.text = Mathf.Round(effectSlider.value * 100).ToString();
        masterVolumeText.text = Mathf.Round(masterSlider.value * 100).ToString();
    }

    // 볼륨 조절 메서드들
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

    // 텍스트 입력으로 볼륨 변경
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

    // 스테이지 선택 후 StageManager에 연결하여 활성화
    public void SelectStage(int stageIndex)
    {
        // StartMenu 및 기타 UI 비활성화
        StartMainUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmOptionSlotPanel.SetActive(false);
        confirmSelectSlotPanel.SetActive(false);
        BTNClick.Play();
        // 플레이어 상태 초기화
        if (playerState != null)
        {
            playerState.IncreaseOxygen(playerState.MaxOxygen - playerState.Oxygen);
            playerState.DecreaseStress(playerState.Stress);
        }

        GameManager.Instance.HideMouse();

        // 스테이지 활성화
        StageManager.Instance.ActivateStage(stageIndex);
    }

    public void OnPanelClick()
    {
        panel.transform.SetAsLastSibling(); // 패널을 가장 앞으로 보냄
    }

    public void Resume()
    {
        HideMainUI();
        PauseMenuUI.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmOptionSlotPanel.SetActive(false);
        confirmSelectSlotPanel.SetActive(false);
        BTNClick.Play();
        if (playerState != null)
        {
            playerState.IncreaseOxygen(playerState.MaxOxygen - playerState.Oxygen);
            playerState.DecreaseStress(playerState.Stress);
        }

        //GameManager.Instance.SetPause(false); // 일시 정지 해제 (커서 잠금 포함)
        GameManager.Instance.HideMouse();
    }

    public void showConfirmExitPanel() { confirmExitPanel.SetActive(true); BTNClick.Play(); }
    public void showOptionPanel() { confirmOptionSlotPanel.SetActive(true); BTNClick.Play(); }
    public void showStageSelectPanel() { confirmSelectSlotPanel.SetActive(true); BTNClick.Play(); }
    public void CancelExit() 
         {   
            confirmExitPanel.SetActive(false);  
            confirmOptionSlotPanel.SetActive(false); 
            confirmSelectSlotPanel.SetActive(false);
            BTNClick.Play();
          }

    public void QuitGame()
    {
        // 게임 종료 기능
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
