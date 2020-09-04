using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Source 0 = piano
    // Source 1 = drums
    private AudioSource[] audioSource;
    private AudioHighPassFilter highFilter;
    private bool initialized = false;
    // 0 = low intensity
    // 1 = medium intensity
    // 2 = high intensity
    public int intensity;

    public AudioClip[] pianoLowIntensity;
    public AudioClip[] pianoMedIntensity;
    public AudioClip[] pianoHiIntensity;
    public AudioClip[] drumsLowIntensity;
    public AudioClip[] drumsMedIntensity;
    public AudioClip[] drumsHiIntensity;

    private bool isTitleScreen = true;

    // DontDestroyOnLoad
    // Sourced from https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html
    private void Awake()
    {
        GameObject[] managers = GameObject.FindGameObjectsWithTag("Music");
        if (managers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        audioSource = GetComponents<AudioSource>();
        highFilter = GetComponent<AudioHighPassFilter>();
        initialized = true;
    }

    private void PlayRandomPiano()
    {
        int randPiano = Random.Range(0, 5);

        if (intensity == 0)
        {
            audioSource[0].clip = pianoLowIntensity[randPiano];
            audioSource[0].Play();
        }
        else if (intensity == 1)
        {
            audioSource[0].clip = pianoMedIntensity[randPiano];
            audioSource[0].Play();
        }
        else
        {
            audioSource[0].clip = pianoHiIntensity[randPiano];
            audioSource[0].Play();
        }
    }

    private void PlayRandomDrums()
    {
        int randDrums = Random.Range(0, 5);

        if (intensity == 0)
        {
            audioSource[1].clip = drumsLowIntensity[randDrums];
            audioSource[1].Play();
        }
        else if (intensity == 1)
        {
            audioSource[1].clip = drumsMedIntensity[randDrums];
            audioSource[1].Play();
        }
        else
        {
            audioSource[1].clip = drumsHiIntensity[randDrums];
            audioSource[1].Play();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 ||
            SceneManager.GetActiveScene().buildIndex == 7)
        {
            isTitleScreen = true;
            intensity = 0;
        } else
        {
            isTitleScreen = false;
        }

        // When no audio is playing, start a new track.
        if (!(audioSource[0].isPlaying || audioSource[1].isPlaying) && initialized)
        {
            PlayRandomPiano();
            if (!isTitleScreen)
            {
                PlayRandomDrums();
            }
        }

        // Disable filters anywhere that's not title screen.
        if (isTitleScreen)
        {
            highFilter.enabled = true;
        }
        else
        {
            highFilter.enabled = false;
        }
    }
}
