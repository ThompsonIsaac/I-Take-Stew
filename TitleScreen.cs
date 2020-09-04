using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public Button start;
    public Button credits;
    public Button back;

    public Canvas menuCanvas;
    public Canvas creditsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            start.onClick.AddListener(StartGame);
            credits.onClick.AddListener(OpenCredits);
            back.onClick.AddListener(ReturnToMenu);
        }
        catch
        {
            Debug.LogWarning("Missing UI elements");
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OpenCredits()
    {
        menuCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
    }

    void ReturnToMenu()
    {
        menuCanvas.gameObject.SetActive(true);
        creditsCanvas.gameObject.SetActive(false);
    }
}
