using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    BGM,
    EFFECT,
    MASTER
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;  // 싱글톤 인스턴스

    [SerializeField] private AudioMixer audioMixer;   // 오디오 믹서 (BGM과 EFFECT 볼륨을 조절하기 위해)
    [SerializeField] private AudioSource bgmSource;   // 배경음악(AudioSource)
    [SerializeField] private AudioSource effectSource; // 효과음(AudioSource)
    [SerializeField] private AudioSource masterSource;

    // 볼륨 조절을 위한 파라미터 이름 (AudioMixer에서 설정한 이름과 동일해야 함)
    private const string BGM_VOLUME = "BGMVolume";
    private const string EFFECT_VOLUME = "EffectVolume";
    private const string MASTER_VOLUME = "MasterVolume";

    private void Awake()
    {
        // 싱글톤 인스턴스 생성
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬이 바뀌어도 유지
        }
        else
        {
            Destroy(gameObject);  // 중복된 SoundManager가 있을 경우 제거
        }
    }

    // 배경음악 재생
    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmSource.clip == bgmClip) return;  // 이미 재생 중인 BGM이면 무시
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    // 효과음 재생
    public void PlayEffect(AudioClip effectClip)
    {
        effectSource.PlayOneShot(effectClip);  // 여러 효과음을 동시에 재생할 수 있게 PlayOneShot 사용
    }

    // BGM 볼륨 설정
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat(BGM_VOLUME, Mathf.Log10(volume) * 20);  // 0~1 사이의 볼륨을 dB로 변환
    }

    // 효과음 볼륨 설정
    public void SetEffectVolume(float volume)
    {
        audioMixer.SetFloat(EFFECT_VOLUME, Mathf.Log10(volume) * 20);
    }

    public void SetMasterVolume(float volume) 
    { 
        audioMixer.SetFloat(MASTER_VOLUME, Mathf.Log10(volume) * 20);
    }

    // 볼륨을 초기화 (AudioMixer의 볼륨을 불러와서 설정)
    public void InitVolumes(float bgmVolume, float effectVolume , float masterVolume)
    {
        SetBGMVolume(bgmVolume);
        SetEffectVolume(effectVolume);
        SetMasterVolume(masterVolume);
    }

    // 모든 소리 일시 정지
    public void PauseAllSounds()
    {
        bgmSource.Pause();
        effectSource.Pause();
        masterSource.Pause();
    }

    // 모든 소리 다시 재생
    public void ResumeAllSounds()
    {
        bgmSource.UnPause();
        effectSource.UnPause();
        masterSource.UnPause();
    }

    // 모든 소리 정지
    public void StopAllSounds()
    {
        bgmSource.Stop();
        effectSource.Stop();
        masterSource.Stop();
    }
}
