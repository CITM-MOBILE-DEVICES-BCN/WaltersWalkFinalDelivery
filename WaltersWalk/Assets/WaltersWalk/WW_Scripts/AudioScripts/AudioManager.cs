using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System.Diagnostics;

public enum SoundType
{
    CLAXON1,
    CLAXON2,
    CLAXON3,
    PILLS,
    SCRATCHING,
    SMOKING,
    CITY
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioClip[] soundList;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    public static AudioManager instance;
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
        AudioManager.PlayMusic(soundList[(int)SoundType.CITY], true);
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
                UnityEngine.Debug.Log("Reproduciendo sonido: " + sound.ToString() + " con clip: " + clip.name);
            }
            else
            {
                UnityEngine.Debug.LogWarning("Clip de audio no encontrado para: " + sound.ToString());
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("AudioManager o AudioSource no está configurado correctamente.");
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