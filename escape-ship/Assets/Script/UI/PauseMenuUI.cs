using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] GameObject BasepauseMenuUI;  // PauseMenu의 Canvas를 드래그하여 연결할 변수
    [SerializeField] GameObject confirmMenuPanel; //메뉴 확인창 UI 
    [SerializeField] GameObject confirmExitPanel;//게임 종료 확인창 UI
    [SerializeField] GameObject confirmSaveSlotPanel;
    [SerializeField] GameObject confirmLoadSlotPanel;
    //[SerializeField] AudioSource[] allAudioSources;
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] private AudioMixer audioMixer; // AudioMixer 연결
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider masterSlider;  // Master Volume 슬라이더
    private bool isPaused = false;  // 게임이 일시정지 상태인지 확인하는 변수

    [SerializeField] private GameObject[] uiElements; // 확인할 UI 요소들을 배열로 지정

    private void Start()
    {
        KeyManager.Instance.keyDic[KeyAction.Setting] += OnSetting;

        // 슬라이더의 값이 변경될 때마다 호출될 메서드 추가
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);

        // 시작 시 슬라이더 값을 초기화 (저장된 설정값 불러오기 또는 기본값 설정)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f); // 기본값 0.75
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.75f); // 기본값 0.75
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);

    }

    // BGM 볼륨을 조절하는 함수
    public void SetBGMVolume(float value)
    {
        // AudioMixer는 볼륨을 데시벨(dB)로 처리하므로, 0~1 값을 -80dB에서 0dB로 변환
        float volume = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("BGMVolume", volume);

        // 설정값 저장
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    // 효과음 볼륨을 조절하는 함수
    public void SetEffectVolume(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("EffectVolume", volume);

        // 설정값 저장
        PlayerPrefs.SetFloat("EffectVolume", value);
    }

    // 효과음 볼륨을 조절하는 함수
    public void SetMasterVolume(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("MasterVolume", volume);

        // 설정값 저장
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    private void OnSetting()
    {
        if (AreAllUIElementsInactive()) Pause(!isPaused); 
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


    void Pause(bool isPuase)
    {
        BasepauseMenuUI.SetActive(isPuase);  // UI 표시
        Time.timeScale = isPuase? 0f : 1;  // 게임 시간 정지
        isPaused = isPuase;  // 일시정지 상태로 설정
        Cursor.visible = isPuase ? Cursor.visible = true : Cursor.visible = false;
        Cursor.lockState = isPuase? CursorLockMode.None : CursorLockMode.Locked;
          
        /*
        pauseMenuUI.SetActive(isPuase);  // UI 표시
            Time.timeScale = isPuase ? 0f : 1f;  // 게임 시간 정지 또는 재개
            isPaused = isPuase;  // 일시정지 상태로 설정

            // 커서 제어
            if (isPuase)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;  // 커서가 자유롭게 움직이게 설정
            }
            else
            {
            EventSystem.current.SetSelectedGameObject(null);

            Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;  // 커서를 화면 중앙에 고정
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
        BasepauseMenuUI.SetActive(false);  // UI 숨기기
        confirmMenuPanel.SetActive(false); //메뉴 확인창 숨기기
        confirmExitPanel.SetActive(false); //나가기 확인창 숨기기
        confirmSaveSlotPanel.SetActive(false); //세이브창 숨기기
        confirmLoadSlotPanel.SetActive(false); // 로드창 숨기기
        MainMenuUI.SetActive(true);

    }

    public void CancelExit()
    {
        confirmMenuPanel.SetActive(false);  // 확인 창 숨기기
        confirmExitPanel.SetActive(false); // 나가기 창 숨기기
        confirmLoadSlotPanel.SetActive(false);
        confirmSaveSlotPanel.SetActive(false);
    }
    public void QuitGame()
    {
        // 게임 종료 기능
        Application.Quit();
        // 에디터 모드에서 테스트 중인 경우
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