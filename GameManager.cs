using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public int levelIntensity;

    public GameObject rat1;
    public GameObject rat2;

    private AudioManager audioManager;
    private AudioSource[] audioSources;

    public AudioClip[] audioClips;

    private bool lost = false;
    private bool stewFlowing = false;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        try
        {
            audioManager = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioManager>();
        }
        catch
        {
            Debug.LogWarning("Audio manager not found");
        }
    }

    private void Update()
    {
        if (rat1 == null && rat2 == null && !lost)
        {
            lost = true;
            PlaySound("lose");
            Invoke("Lose", 2);
        }

        // Update intensity
        if (audioManager != null)
        {
            int intensity = 0;
            if (rat1 == null)
                intensity++;
            else if (rat1.GetComponent<PlayerMovement>().HasEnoughStew())
                intensity++;
            if (rat2 == null)
                intensity++;
            else if (rat2.GetComponent<PlayerMovement>().HasEnoughStew())
                intensity++;
            if (rat1 && rat2)
            {
                if (rat1.GetComponent<PlayerMovement>().HasEnoughStew() &&
                    rat2.GetComponent<PlayerMovement>().HasEnoughStew())
                {
                    intensity--;
                }
            }
            audioManager.intensity = intensity;
        }
    }

    // Called by player when player dies.
    public void RemovePlayer(PlayerMovement rat)
    {
        if (rat.gameObject == rat1)
        {
            rat1 = null;
        }
        if (rat.gameObject == rat2)
        {
            rat2 = null;
        }
    }

    // Play various sounds.
    public void PlaySound(string sound)
    {
        if (sound == "alert")
        {
            audioSources[0].clip = audioClips[Random.Range(0, 2)];
            audioSources[0].Play();
        }
        else if (sound == "hit")
        {
            audioSources[0].clip = audioClips[2];
            audioSources[0].Play();
        }
        else if (sound == "lose")
        {
            audioSources[0].clip = audioClips[3];
            audioSources[0].Play();
        }
        else if (sound == "stew" && !stewFlowing)
        {
            stewFlowing = true;
            audioSources[1].Play();
        }
        else if (sound == "stop_stew")
        {
            stewFlowing = false;
            audioSources[1].Stop();
        }
    }
    
    private void Lose()
    {
        Debug.Log("You lose");
        RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Called by player when stew is returned to base.
    public void Win()
    {
        Debug.Log("You win");
        NextLevel();
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool IsTitleScreen()
    {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }
}
