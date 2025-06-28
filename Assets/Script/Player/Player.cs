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

        // Collider 기준으로 platform 위에 위치시키기
        Collider2D platformCol = bottomPlatform.GetComponent<Collider2D>();

        float platformTopY = platformCol.bounds.max.y; // 플랫폼 상단 Y
        float playerHalfHeight = col.bounds.extents.y; // 플레이어 반 높이

        transform.position = new Vector3(-4, platformTopY + playerHalfHeight + 0.01f, 0);
        
    }


    void Update()
    {
        Collider2D platformCol = bottomPlatform.GetComponent<Collider2D>();

        float platformTopY = platformCol.bounds.max.y; // 플랫폼 상단 Y
        float playerHalfHeight = col.bounds.extents.y; // 플레이어 반 높이

        transform.position = new Vector3(-4, platformTopY + playerHalfHeight + 0.01f, 0);

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            rb.gravityScale = 0;

            // 정확한 착지 보정
            float groundTopY = collision.collider.bounds.max.y;
            float playerHalfHeight = col.bounds.extents.y;

            transform.position = new Vector3(
                transform.position.x,
                groundTopY + playerHalfHeight,
                transform.position.z
            );
        }
        else
        {
            // 다른 오브젝트와 충돌 시 중력 적용
            rb.gravityScale = 1;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            rb.gravityScale = 1;
        }
    }



}
