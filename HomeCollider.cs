using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeCollider : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // If a player enters and has the required stew,
    // we win the game.
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement.HasEnoughStew())
            {
                gameManager.Win();
            }
        }
    }
}
