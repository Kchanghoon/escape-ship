using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : Singleton<SettingsManager>
{
    [Header("오디오 설정")]
    [SerializeField] private AudioMixer audioMixer; // 오디오 믹서
    [SerializeField] private Slider bgmSlider; // 배경 음악 슬라이더
    [SerializeField] private Slider effectSlider; // 효과음 슬라이더
    [SerializeField] private Slider masterSlider; // 마스터 볼륨 슬라이더
    [SerializeField] private TMP_InputField bgmVolumeText; // 배경 음악 볼륨 텍스트 입력 필드
    [SerializeField] private TMP_InputField effectVolumeText; // 효과음 볼륨 텍스트 입력 필드
    [SerializeField] private TMP_InputField masterVolumeText; // 마스터 볼륨 텍스트 입력 필드
    [SerializeField] private AudioSource BTNClick;

    [Header("카메라 및 마우스 설정")]
    [SerializeField] private Slider sensitivitySlider;  // 마우스 감도 슬라이더
    [SerializeField] private Slider fovSlider;          // 시야각(FOV) 슬라이더
    [SerializeField] private Camera playerCamera;       // 플레이어 카메라
    [SerializeField] private TMP_InputField sensitivityText;  // 감도 텍스트 입력 필드
    [SerializeField] private TMP_InputField fovText;          // FOV 텍스트 입력 필드

    [Header("옵션 패널")]
    [SerializeField] private GameObject OptionPanel; // 옵션 패널

    private void Start()
    {
        // 오디오 슬라이더 및 텍스트 필드 초기화
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);

        bgmVolumeText.onEndEdit.AddListener(delegate { OnVolumeTextChange(bgmVolumeText, bgmSlider, SetBGMVolume); });
        effectVolumeText.onEndEdit.AddListener(delegate { OnVolumeTextChange(effectVolumeText, effectSlider, SetEffectVolume); });
        masterVolumeText.onEndEdit.AddListener(delegate { OnVolumeTextChange(masterVolumeText, masterSlider, SetMasterVolume); });

        // 감도 및 FOV 슬라이더 초기화
        sensitivitySlider.minValue = 0.1f; // 최소값 설정
        sensitivitySlider.maxValue = 10f; // 최대값 설정
        sensitivitySlider.value = Mathf.Clamp(MouseCam.Instance.mouseSpeed, 0.1f, 10f); // 마우스 감도 초기값 설정

        fovSlider.value = playerCamera.fieldOfView; // FOV 초기값 설정

        // FOV 슬라이더 한계 설정
        fovSlider.minValue = 30f;
        fovSlider.maxValue = 90f;

        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        fovSlider.onValueChanged.AddListener(OnFOVChanged);

        sensitivityText.text = Mathf.Round(sensitivitySlider.value).ToString();
        fovText.text = Mathf.Round(fovSlider.value).ToString();

        sensitivityText.onEndEdit.AddListener(delegate { OnValueTextChange(sensitivityText, sensitivitySlider, OnSensitivityChanged); });
        fovText.onEndEdit.AddListener(delegate { OnValueTextChange(fovText, fovSlider, OnFOVChanged); });

        LoadSettings(); // 설정 불러오기
        UpdateTextFields(); // 텍스트 필드 업데이트
    }


    private void LoadSettings()
    {
        // 오디오 설정 불러오기
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.75f);
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);

        // 마우스 감도 및 FOV 설정 불러오기
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 4f); // 기본값을 5로 설정
        fovSlider.value = PlayerPrefs.GetFloat("FOV", 60f); // 기본값을 60으로 설정
    }

    private void UpdateTextFields()
    {
        // 오디오 텍스트 필드 업데이트
        bgmVolumeText.text = Mathf.Round(bgmSlider.value * 100).ToString();
        effectVolumeText.text = Mathf.Round(effectSlider.value * 100).ToString();
        masterVolumeText.text = Mathf.Round(masterSlider.value * 100).ToString();

        // 감도 및 FOV 텍스트 필드 업데이트
        sensitivityText.text = sensitivitySlider.value.ToString("F1"); // 소수점 첫째 자리까지 표시
        fovText.text = Mathf.Round(fovSlider.value).ToString();
    }


    // 오디오 볼륨 설정 메서드
    public void SetBGMVolume(float value)
    {
        SetVolume("BGMVolume", value);
        PlayerPrefs.SetFloat("BGMVolume", value);
        UpdateTextFields();
    }

    public void SetEffectVolume(float value)
    {
        SetVolume("EffectVolume", value);
        PlayerPrefs.SetFloat("EffectVolume", value);
        UpdateTextFields();
    }

    public void SetMasterVolume(float value)
    {
        SetVolume("MasterVolume", value);
        PlayerPrefs.SetFloat("MasterVolume", value);
        UpdateTextFields();
    }

    private void SetVolume(string parameter, float value)
    {
        float volume = (value > 0.0001f) ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat(parameter, volume);
    }

    private void OnVolumeTextChange(TMP_InputField textField, Slider slider, System.Action<float> setVolumeAction)
    {
        if (float.TryParse(textField.text, out float result))
        {
            result = Mathf.Clamp(result / 100f, 0.0001f, 1f);
            slider.value = result;
            setVolumeAction(result);
        }
    }

    // 감도 및 FOV 조정 메서드
    private void OnSensitivityChanged(float value)
    {
        MouseCam.Instance.mouseSpeed = value;
        sensitivityText.text = value.ToString("F1"); // 소수점 첫째 자리까지 표시
        PlayerPrefs.SetFloat("MouseSensitivity", value); // 마우스 감도 저장
    }


    private void OnFOVChanged(float value)
    {
        value = Mathf.Clamp(value, 10f, 90f);  // FOV 값을 10~90 사이로 제한
        playerCamera.fieldOfView = value;
        fovText.text = Mathf.Round(value).ToString(); // 텍스트 필드 업데이트
        PlayerPrefs.SetFloat("FOV", value); // FOV 저장
    }

    private void OnValueTextChange(TMP_InputField textField, Slider slider, System.Action<float> onValueChanged)
    {
        if (float.TryParse(textField.text, out float result))
        {
            result = Mathf.Clamp(result, slider.minValue, slider.maxValue);
            slider.value = result;
            onValueChanged(result);
        }
    }

    public void ExitPanel()
    {
        OptionPanel.SetActive(false); // 옵션 패널 비활성화
        BTNClick.Play();
    }
}
