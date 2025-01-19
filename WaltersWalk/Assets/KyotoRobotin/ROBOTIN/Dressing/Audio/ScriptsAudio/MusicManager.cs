using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] sceneMusic; // Asigna las m�sicas en el inspector.
    private AudioSource audioSource;

    private void Awake()
    {
        // Configura el GameObject para que no se destruya entre escenas.
        DontDestroyOnLoad(gameObject);

        // Obt�n o a�ade un AudioSource si no existe.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = true; // Para que la m�sica se repita.
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;

        // Cambia la m�sica si hay una asociada a esta escena.
        if (sceneIndex < sceneMusic.Length && sceneMusic[sceneIndex] != null)
        {
            PlayMusic(sceneMusic[sceneIndex]);
        }
        else
        {
            StopMusic();
        }
    }

    public void PlayMusic(AudioClip newMusic)
    {
        if (audioSource.clip != newMusic)
        {
            audioSource.clip = newMusic;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}

