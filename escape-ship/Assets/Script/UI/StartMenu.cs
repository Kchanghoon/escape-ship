using System;
using UnityEngine;
using UnityEngine.UI;  // Button 클래스 사용을 위해 필요
using DG.Tweening;
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
    [SerializeField] private GameObject confirmLoadSlotPanel;
    [SerializeField] private GameObject confirmSelectSlotPanel;

    // 플레이어 설정
    [Header("Player Settings")]
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Button resumeButton;

    // 오디오 설정
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private TMP_InputField bgmVolumeText;
    [SerializeField] private TMP_InputField effectVolumeText;
    [SerializeField] private TMP_InputField masterVolumeText;

    private bool isPaused = false;
    public GameObject panel;

    private void Start()
    {
        MouseCam.Instance.SetCursorState(false);  // 초기에는 마우스 커서를 보이게 설정
        CheckSavedGame();

        // 슬라이더 값 변경 이벤트 연결
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);

        // 텍스트 필드 값 변경 이벤트 연결
        bgmVolumeText.onEndEdit.AddListener(delegate { OnBGMTextChange(bgmVolumeText.text); });
        effectVolumeText.onEndEdit.AddListener(delegate { OnEffectTextChange(effectVolumeText.text); });
        masterVolumeText.onEndEdit.AddListener(delegate { OnMasterTextChange(masterVolumeText.text); });

        // 슬라이더 초기값 설정
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.75f);
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);

        UpdateTextFields();
    }


    public void ShowMainUI()
    {
        StartMainUI.SetActive(true);
        MouseCam.Instance.SetCursorState(false);  // 커서 보이기
    }

    // StartMainUI 비활성화 시 커서를 숨기고 잠금
    public void HideMainUI()
    {
        StartMainUI.SetActive(false);
        MouseCam.Instance.SetCursorState(true);  // 커서 잠금 및 숨기기
    }


    // 저장된 게임이 있는지 확인하는 메서드
    private void CheckSavedGame()
    {
        // 예시로 PlayerPrefs 사용. 실제 게임에서는 세이브 파일이나 DB 확인 가능
        if (PlayerPrefs.HasKey("SavedGame"))
        {
            resumeButton.interactable = true;  // 저장된 게임이 있으면 Resume 버튼 활성화
        }
        else
        {
            resumeButton.interactable = false;  // 저장된 게임이 없으면 Resume 버튼 비활성화
        }
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
        confirmLoadSlotPanel.SetActive(false);
        confirmSelectSlotPanel.SetActive(false);

        // 플레이어 상태 초기화
        if (playerState != null)
        {
            playerState.IncreaseOxygen(playerState.MaxOxygen - playerState.Oxygen); // 산소를 100으로 설정
            playerState.DecreaseStress(playerState.Stress); // 스트레스를 0으로 설정
        }

        Time.timeScale = 1f;
        isPaused = false;

        // 게임 진입 시 커서 잠금과 화면 회전 활성화 설정
        MouseCam.Instance.SetCursorState(true);  // 마우스 커서 잠금 및 화면 회전 활성화

        // 스테이지 활성화
        StageManager.Instance.ActivateStage(stageIndex);
    }



    public void OnPanelClick()
    {
        panel.transform.SetAsLastSibling(); // 패널을 가장 앞으로 보냄
    }

    public void Resume()
    {
        HideMainUI();  // StartMainUI를 비활성화하고 커서를 숨김

        PauseMenuUI.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmOptionSlotPanel.SetActive(false);
        confirmLoadSlotPanel.SetActive(false);
        confirmSelectSlotPanel.SetActive(false);

        if (playerState != null)
        {
            playerState.IncreaseOxygen(playerState.MaxOxygen - playerState.Oxygen);  // 산소 100 설정
            playerState.DecreaseStress(playerState.Stress);  // 스트레스 0 설정
        }

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void showConfirmExitPanel() { confirmExitPanel.SetActive(true); }
    public void showLoadSlotPanel() { confirmLoadSlotPanel.SetActive(true); }
    public void showOptionPanel() { confirmOptionSlotPanel.SetActive(true); }
    public void showStageSelectPanel() { confirmSelectSlotPanel.SetActive(true); }
    public void CancelExit() { confirmExitPanel.SetActive(false); confirmLoadSlotPanel.SetActive(false); confirmOptionSlotPanel.SetActive(false); confirmSelectSlotPanel.SetActive(false); }

    public void QuitGame()
    {
        // 게임 종료 기능
        Application.Quit();

        // 에디터 모드에서 테스트 중인 경우
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
