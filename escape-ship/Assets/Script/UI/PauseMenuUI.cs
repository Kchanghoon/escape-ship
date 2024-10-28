using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // TextMeshPro를 사용하기 위해 추가

public class PauseMenuUI : MonoBehaviour
{
// 기본 UI 패널들
    [Header("UI Panels")]
    [SerializeField] private GameObject BasepauseMenuUI;      // PauseMenu의 Canvas
    [SerializeField] private GameObject confirmMenuPanel;     // 메뉴 확인창 UI
    [SerializeField] private GameObject confirmExitPanel;     // 게임 종료 확인창 UI
    [SerializeField] private GameObject confirmSaveSlotPanel; // 저장 슬롯 확인창 UI
    [SerializeField] private GameObject confirmLoadSlotPanel; // 로드 슬롯 확인창 UI
    [SerializeField] private GameObject MainMenuUI;           // 메인 메뉴 UI

    // 오디오 설정 슬라이더와 입력 필드
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private TMP_InputField bgmVolumeText;
    [SerializeField] private TMP_InputField effectVolumeText;
    [SerializeField] private TMP_InputField masterVolumeText;

    // 기타 설정
    [Header("Other Settings")]
    [SerializeField] private GameObject[] uiElements; // 활성화 상태 확인용 UI 요소 배열
    private bool isPaused = false;                    // 게임 일시정지 상태 확인

    private void Start()
    {
        KeyManager.Instance.keyDic[KeyAction.Setting] += OnSetting;

        // 슬라이더의 값이 변경될 때마다 호출될 메서드 추가
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);

        // 텍스트 필드 값이 변경될 때마다 호출될 메서드 추가
        bgmVolumeText.onEndEdit.AddListener(delegate { OnBGMTextChange(bgmVolumeText.text); });
        effectVolumeText.onEndEdit.AddListener(delegate { OnEffectTextChange(effectVolumeText.text); });
        masterVolumeText.onEndEdit.AddListener(delegate { OnMasterTextChange(masterVolumeText.text); });

        // 시작 시 슬라이더 값 초기화 (저장된 설정값 불러오기 또는 기본값 설정)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f); // 기본값 0.75
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.75f); // 기본값 0.75
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);

        // 텍스트 필드도 초기화
        UpdateTextFields();
    }

    // 슬라이더 변경 시 텍스트 필드 업데이트
    private void UpdateTextFields()
    {
        bgmVolumeText.text = Mathf.Round(bgmSlider.value * 100).ToString(); // 슬라이더 값 퍼센트로 변환
        effectVolumeText.text = Mathf.Round(effectSlider.value * 100).ToString();
        masterVolumeText.text = Mathf.Round(masterSlider.value * 100).ToString();
    }

    // BGM 볼륨을 조절하는 함수
    public void SetBGMVolume(float value)
    {
        float volume = (value > 0.0001f) ? Mathf.Log10(value) * 20 : -80f; // 0이면 -80dB로 설정
        audioMixer.SetFloat("BGMVolume", volume);
        PlayerPrefs.SetFloat("BGMVolume", value);
        UpdateTextFields(); // 슬라이더 변경 시 텍스트 필드도 업데이트
    }

    // 효과음 볼륨을 조절하는 함수
    public void SetEffectVolume(float value)
    {
        float volume = (value > 0.0001f) ? Mathf.Log10(value) * 20 : -80f; // 0이면 -80dB로 설정
        audioMixer.SetFloat("EffectVolume", volume);
        PlayerPrefs.SetFloat("EffectVolume", value);
        UpdateTextFields(); // 슬라이더 변경 시 텍스트 필드도 업데이트
    }

    // Master 볼륨을 조절하는 함수
    public void SetMasterVolume(float value)
    {
        float volume = (value > 0.0001f) ? Mathf.Log10(value) * 20 : -80f; // 0이면 -80dB로 설정
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", value);
        UpdateTextFields(); // 슬라이더 변경 시 텍스트 필드도 업데이트
    }


    // 텍스트 입력으로 BGM 볼륨 변경
    private void OnBGMTextChange(string newValue)
    {
        if (float.TryParse(newValue, out float result))
        {
            result = Mathf.Clamp(result / 100f, 0.0001f, 1f); // 0 ~ 100 범위를 0 ~ 1로 변환
            bgmSlider.value = result;  // 슬라이더 변경
            SetBGMVolume(result);  // 오디오 볼륨 변경
        }
    }

    // 텍스트 입력으로 효과음 볼륨 변경
    private void OnEffectTextChange(string newValue)
    {
        if (float.TryParse(newValue, out float result))
        {
            result = Mathf.Clamp(result / 100f, 0.0001f, 1f); // 0 ~ 100 범위를 0 ~ 1로 변환
            effectSlider.value = result;
            SetEffectVolume(result);
        }
    }

    // 텍스트 입력으로 Master 볼륨 변경
    private void OnMasterTextChange(string newValue)
    {
        if (float.TryParse(newValue, out float result))
        {
            result = Mathf.Clamp(result / 100f, 0.0001f, 1f); // 0 ~ 100 범위를 0 ~ 1로 변환
            masterSlider.value = result;
            SetMasterVolume(result);
        }
    }

    


    private void OnSetting()
    {
        isPaused = !isPaused;
        if (AreAllUIElementsInactive()) Pause(isPaused);
    }

    private bool AreAllUIElementsInactive()
    {
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement.activeSelf) // 하나라도 활성화되어 있으면 false 반환
            {
                Debug.Log(uiElement.name + " is active.");
                return false;
            }
        }
        return true; // 모든 UI가 비활성화되어 있으면 true 반환
    }

    void Pause(bool isPause)
    {
        BasepauseMenuUI.SetActive(isPause);  // UI 표시
        GameManager.Instance.isSetting = isPause;
        Time.timeScale = isPause ? 0f : 1;  // 게임 시간 정지
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // 게임 일시정지 해제
        Pause(false);  // 일시정지를 해제하여 게임 상태 초기화

        // UI 상태 업데이트
        BasepauseMenuUI.SetActive(false);
        confirmMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmSaveSlotPanel.SetActive(false);
        confirmLoadSlotPanel.SetActive(false);
        MainMenuUI.SetActive(true);

        // 마우스 커서 표시 및 잠금 해제
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
