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
    CITY,
    CITY2,
    MOMCALL,
    CARCRASH
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioClip[] soundList;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Slider envVolumeSlider;
    public static AudioManager instance;

    public AudioSource effectsAudioSource;
    public AudioSource envAudioSource1;
    public AudioSource envAudioSource2;

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

        if (envVolumeSlider != null)
        {
            envVolumeSlider.onValueChanged.AddListener(SetEnvVolume);
            envVolumeSlider.value = envAudioSource1.volume;
        }

        PlayEnv(soundList[(int)SoundType.CITY], soundList[(int)SoundType.CITY2], true);
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

        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.PlaySound(SoundType.CARCRASH);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.PlaySound(SoundType.MOMCALL);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();
        }
    }

    public AudioClip GetAudioClip(SoundType sound)
    {
        if (soundList != null && (int)sound < soundList.Length)
        {
            return soundList[(int)sound];
        }
        return null;
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
                UnityEngine.Debug.Log("Clip de audio no encontrado para: " + sound.ToString());
            }
        }
        else
        {
            UnityEngine.Debug.Log("AudioManager o AudioSource no está configurado correctamente.");
        }
    }

    public static void PlayEnv(AudioClip envClip1, AudioClip envClip2, bool loop = true)
    {
        if (instance != null && instance.envAudioSource1 != null && instance.envAudioSource2 != null && envClip1 != null && envClip2 != null)
        {
            instance.envAudioSource1.clip = envClip1;
            instance.envAudioSource1.loop = loop;
            instance.envAudioSource1.Play();
            instance.envAudioSource1.mute = false;

            instance.envAudioSource2.clip = envClip2;
            instance.envAudioSource2.loop = loop;
            instance.envAudioSource2.Play();
            instance.envAudioSource2.mute = true;
        }
    }

    public void SetEffectsVolume(float volume)
    {
        effectsAudioSource.volume = volume;
    }

    public void SetEnvVolume(float volume)
    {
        envAudioSource1.volume = volume;
        envAudioSource2.volume = volume;
    }

    private void ToggleMute()
    {
        if (envAudioSource1 != null && envAudioSource2 != null)
        {   
            envAudioSource1.mute = !envAudioSource1.mute;
            envAudioSource2.mute = !envAudioSource2.mute;
        }
    }
}
