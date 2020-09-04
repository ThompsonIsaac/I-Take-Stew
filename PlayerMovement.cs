using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that controls player movement.
// Class is the same for each player, one WASD, one arrow keys.
public class PlayerMovement : MonoBehaviour
{
    public KeyCode forwardKey;
    public KeyCode leftKey;
    public KeyCode backwardsKey;
    public KeyCode rightKey;

    private GameManager gameManager;

    // Images and frame timing
    private SpriteRenderer spriteRenderer;
    private Sprite selectedFrame;
    public Sprite frame1;
    public Sprite frame2;
    private float timer = 0f;

    public int playerID;
    public float speed = 5f;
    public float stewCollectionSpeed = 0.2f;
    public float frameTime = 0.5f;

    // Amount of stew collected so far. 1f is a full bowl.
    private float stewCollected = 0f;
    private bool hasEnoughStew = false;

    private Rigidbody2D rigidbody;

    // Initialize level
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectedFrame = frame1;
        UpdateFrame();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 netVelocity = Vector2.zero;

        if (Input.GetKey(forwardKey))
        {
            netVelocity += Vector2.up;
        }
        if (Input.GetKey(leftKey))
        {
            netVelocity += Vector2.left;

            // Flip rat left
            Vector3 scale = transform.localScale;
            scale.x = -scale.y; // Flipped to left and scaled
            transform.localScale = scale;
        }
        if (Input.GetKey(backwardsKey))
        {
            netVelocity += Vector2.down;
        }
        if (Input.GetKey(rightKey))
        {
            netVelocity += Vector2.right;

            // Flip rat right
            Vector3 scale = transform.localScale;
            scale.x = scale.y; // Flipped to right and scaled
            transform.localScale = scale;
        }

        rigidbody.velocity = netVelocity * speed;

        if (rigidbody.velocity != Vector2.zero)
            timer += Time.deltaTime;
    
        if (timer >= frameTime)
        {
            timer = 0f;
            if (selectedFrame == frame1)
            {
                selectedFrame = frame2;
            }
            else
            {
                selectedFrame = frame1;
            }
            UpdateFrame();
        }
    }

    // StewCollider calls this method on the rat
    // when rat is in stew collection radius.
    public void IncrementStew()
    {
        stewCollected += Time.deltaTime * stewCollectionSpeed;
        if (!hasEnoughStew && stewCollected >= 1f)
        {
            hasEnoughStew = true;
            Debug.Log("Bowl full for " + gameObject.name);
        }
    }

    // Respond to collision with enemy. Death.
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    // Handle death.
    private void Die()
    {
        gameManager.PlaySound("hit");
        gameManager.RemovePlayer(this);
        Destroy(this.gameObject);
    }

    // Get stew collected. Float from 0f to 1f.
    public float GetStewCollected()
    {
        return stewCollected;
    }

    // Return true if we have enough stew to win.
    public bool HasEnoughStew()
    {
        return hasEnoughStew;
    }

    private void UpdateFrame()
    {
        spriteRenderer.sprite = selectedFrame;
    }
}
