using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite Player_standing;
    public Sprite Player_walking1;
    public Sprite Player_walking2;
    public Sprite Player_jump;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private BoxCollider2D col;

    public float moveSpeed = 3f;
    public float jumpForce = 7f;
    public float animationInterval = 0.2f;

    private float animationTimer = 0f;
    private int walkFrameIndex = 0;

    private bool isJumping = false;
    private bool isGrounded = false;

    public GameObject bottomPlatform;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        sr.sprite = Player_standing;
        rb.gravityScale = 0;

        // platform 위로 올려놓기
        SpriteRenderer platformRenderer = bottomPlatform.GetComponent<SpriteRenderer>();
        float platformTopY = bottomPlatform.transform.position.y + platformRenderer.bounds.size.y / 2f;
        float spriteHeight = sr.bounds.size.y;

        transform.position = new Vector3(-4, platformTopY + spriteHeight / 2f - 0.05f, 0);
    }

    void Update()
    {
        CheckPlatformBoundary();

        HandleMove();

        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        UpdateSprite();
    }

    void HandleMove()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h != 0)
        {
            sr.flipX = (h < 0);
            rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Jump()
    {
        rb.gravityScale = 1;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
        isGrounded = false;
    }

    void UpdateSprite()
    {
        if (!isGrounded)
        {
            sr.sprite = Player_jump;
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");

        if (h != 0)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= animationInterval)
            {
                walkFrameIndex = (walkFrameIndex + 1) % 2;
                sr.sprite = (walkFrameIndex == 0) ? Player_walking1 : Player_walking2;
                animationTimer = 0f;
            }
        }
        else
        {
            sr.sprite = Player_standing;
            animationTimer = 0f;
        }
    }

    void CheckPlatformBoundary()
    {
        SpriteRenderer platformRenderer = bottomPlatform.GetComponent<SpriteRenderer>();

        float platformLeft = bottomPlatform.transform.position.x - platformRenderer.bounds.size.x / 2f;
        float platformRight = bottomPlatform.transform.position.x + platformRenderer.bounds.size.x / 2f;

        float platformTopY = bottomPlatform.transform.position.y + platformRenderer.bounds.size.y / 2f;
        float playerBottomY = transform.position.y - sr.bounds.size.y / 2f;

        float playerX = transform.position.x;

        // x범위 밖이고 y도 아래 있지 않다면 중력 적용
        if (playerX < platformLeft || playerX > platformRight || playerBottomY > platformTopY + 0.05f)
        {
            rb.gravityScale = 1;
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == bottomPlatform)
        {
            isGrounded = true;
            isJumping = false;
            rb.gravityScale = 0;

            // 정확한 착지 보정
            SpriteRenderer platformRenderer = bottomPlatform.GetComponent<SpriteRenderer>();
            float platformTopY = bottomPlatform.transform.position.y + platformRenderer.bounds.size.y / 2f;
            float spriteHeight = sr.bounds.size.y;

            transform.position = new Vector3(transform.position.x, platformTopY + spriteHeight / 2f - 0.01f, transform.position.z);
        }
    }
}
