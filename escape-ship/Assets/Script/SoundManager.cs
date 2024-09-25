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
    public static SoundManager Instance;  // �̱��� �ν��Ͻ�

    [SerializeField] private AudioMixer audioMixer;   // ����� �ͼ� (BGM�� EFFECT ������ �����ϱ� ����)
    [SerializeField] private AudioSource bgmSource;   // �������(AudioSource)
    [SerializeField] private AudioSource effectSource; // ȿ����(AudioSource)
    [SerializeField] private AudioSource masterSource;

    // ���� ������ ���� �Ķ���� �̸� (AudioMixer���� ������ �̸��� �����ؾ� ��)
    private const string BGM_VOLUME = "BGMVolume";
    private const string EFFECT_VOLUME = "EffectVolume";
    private const string MASTER_VOLUME = "MasterVolume";

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // ���� �ٲ� ����
        }
        else
        {
            Destroy(gameObject);  // �ߺ��� SoundManager�� ���� ��� ����
        }
    }

    // ������� ���
    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmSource.clip == bgmClip) return;  // �̹� ��� ���� BGM�̸� ����
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    // ȿ���� ���
    public void PlayEffect(AudioClip effectClip)
    {
        effectSource.PlayOneShot(effectClip);  // ���� ȿ������ ���ÿ� ����� �� �ְ� PlayOneShot ���
    }

    // BGM ���� ����
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat(BGM_VOLUME, Mathf.Log10(volume) * 20);  // 0~1 ������ ������ dB�� ��ȯ
    }

    // ȿ���� ���� ����
    public void SetEffectVolume(float volume)
    {
        audioMixer.SetFloat(EFFECT_VOLUME, Mathf.Log10(volume) * 20);
    }

    public void SetMasterVolume(float volume) 
    { 
        audioMixer.SetFloat(MASTER_VOLUME, Mathf.Log10(volume) * 20);
    }

    // ������ �ʱ�ȭ (AudioMixer�� ������ �ҷ��ͼ� ����)
    public void InitVolumes(float bgmVolume, float effectVolume , float masterVolume)
    {
        SetBGMVolume(bgmVolume);
        SetEffectVolume(effectVolume);
        SetMasterVolume(masterVolume);
    }

    // ��� �Ҹ� �Ͻ� ����
    public void PauseAllSounds()
    {
        bgmSource.Pause();
        effectSource.Pause();
        masterSource.Pause();
    }

    // ��� �Ҹ� �ٽ� ���
    public void ResumeAllSounds()
    {
        bgmSource.UnPause();
        effectSource.UnPause();
        masterSource.UnPause();
    }

    // ��� �Ҹ� ����
    public void StopAllSounds()
    {
        bgmSource.Stop();
        effectSource.Stop();
        masterSource.Stop();
    }
}
