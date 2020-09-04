using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StewCollider : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // When a player is within the trigger zone,
    // their stew counter is incremented.
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.ToLower() == "player")
        {
            other.gameObject.GetComponent<PlayerMovement>().IncrementStew();
            gameManager.PlaySound("stew");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.ToLower() == "player")
        {
            gameManager.PlaySound("stop_stew");
        }
    }
}
