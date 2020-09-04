using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAI : MonoBehaviour
{
    private GameManager gameManager;

    public float speed = 5f;
    public Sprite front;
    public Sprite back;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    private bool moving = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Whenever a rat is within our view radius,
    // target them.
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 startPos = this.transform.position;
            Vector2 targetPos = other.transform.position;

            Vector2 netVelocity = Vector2.zero;
            
            // Move toward rat based on relative position.
            if (targetPos.y > startPos.y + 0.1f)
            {
                netVelocity += Vector2.up;
            }
            else
            {
                netVelocity += Vector2.down;
            }
            if (targetPos.x > startPos.x)
            {
                netVelocity += Vector2.right;
            }
            else
            {
                netVelocity += Vector2.left;
            }

            rigidbody.velocity = netVelocity * speed;

            // Handle sound effects (alert)
            if (!moving)
            {
                gameManager.PlaySound("alert");
                moving = true;
            }
            
            // Update sprite based on vertical velocity.
            if (rigidbody.velocity.y > 0)
            {
                spriteRenderer.sprite = back;
            } else
            {
                spriteRenderer.sprite = front;
            }
        }
    }

    // Stop when rat leaves our view radius.
    public void OnTriggerExit2D(Collider2D collision)
    {
        rigidbody.velocity = Vector2.zero;
        moving = false;
    }
}
