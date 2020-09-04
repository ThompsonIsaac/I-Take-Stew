using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Update health bars automatically
// based on player information.
public class HealthBars : MonoBehaviour
{
    private GameManager gameManager;

    // PlayerMovement scripts
    private PlayerMovement player1;
    private PlayerMovement player2;

    // UI Green Bars
    public Slider P1StewBar;
    public Slider P2StewBar;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player1 = gameManager.rat1.GetComponent<PlayerMovement>();
        player2 = gameManager.rat2.GetComponent<PlayerMovement>();
    }

    public void Update()
    {
        P1StewBar.value = player1.GetStewCollected();
        P2StewBar.value = player2.GetStewCollected();
    }
}
