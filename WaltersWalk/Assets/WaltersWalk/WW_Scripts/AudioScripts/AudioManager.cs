using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;

public enum SoundType
{
    PILLS,
    SCRATCHING,
    SMOKING
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    private static AudioManager instance;
    public AudioSource effectsAudioSource;
    public AudioSource musicAudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (effectsVolumeSlider != null)
        {
            effectsVolumeSlider.onValueChanged.AddListener(SetEffectsVolume);
            effectsVolumeSlider.value = effectsAudioSource.volume;
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            musicVolumeSlider.value = musicAudioSource.volume;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.PlaySound(SoundType.SCRATCHING);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioManager.PlaySound(SoundType.PILLS);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            AudioManager.PlaySound(SoundType.SMOKING);
        }
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        if (instance != null && instance.effectsAudioSource != null && instance.soundList != null)
        {
            AudioClip clip = instance.soundList[(int)sound];
            if (clip != null)
            {
                instance.effectsAudioSource.PlayOneShot(clip, volume * instance.effectsAudioSource.volume);
            }
        }
    }

    public static void PlayMusic(AudioClip musicClip, bool loop = true)
    {
        if (instance != null && instance.musicAudioSource != null && musicClip != null)
        {
            instance.musicAudioSource.clip = musicClip;
            instance.musicAudioSource.loop = loop;
            instance.musicAudioSource.Play();
        }
    }

    public void SetEffectsVolume(float volume)
    {
        effectsAudioSource.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }
}