using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class icicle : MonoBehaviour
{
    private bool isFalling = false;
    private bool wasMouseHeld = false;

    private Rigidbody2D rb;
    private Collider2D col;

    private float screenWidth;
    private float screenHeight;
    private float spriteWidth;
    private float spriteHeight;

    public GameObject bottomPlatform; // 인스펙터에서 연결 필요

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // 초기엔 안 떨어짐

        col = GetComponent<Collider2D>();

        // 화면 크기 계산
        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;

        // 스프라이트 크기 계산
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;

        if (bottomPlatform == null)
        {
            Debug.LogError("bottomPlatform이 연결되지 않았습니다!");
        }
    }

    void Update()
    {
        DetectMouseDrag();

        if (isFalling)
        {
            CheckCollisionWithPlatform();
        }

        // 화면 아래로 넘어가면 강제 멈춤
        if (transform.position.y <= -screenHeight + spriteHeight / 2)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.position = new Vector2(transform.position.x, -screenHeight + spriteHeight / 2);
            isFalling = false;
        }
    }

    void DetectMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            if (col.OverlapPoint(mousePos2D) && !isFalling)
            {
                isFalling = true;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    void CheckCollisionWithPlatform()
    {
        // icicle 하단 y좌표
        float icicleBottomY = transform.position.y - spriteHeight / 2f;

        // platform 상단 y좌표
        SpriteRenderer platformRenderer = bottomPlatform.GetComponent<SpriteRenderer>();
        float platformHeight = platformRenderer.bounds.size.y;
        float platformTopY = bottomPlatform.transform.position.y + platformHeight / 2f;

        // platform x 좌표 범위
        float platformLeftX = bottomPlatform.transform.position.x - platformRenderer.bounds.size.x / 2f;
        float platformRightX = bottomPlatform.transform.position.x + platformRenderer.bounds.size.x / 2f;

        // icicle 중심 x
        float icicleX = transform.position.x;

        // platform에 닿았는지 (y축 기준)
        if (icicleBottomY <= platformTopY)
        {
            // ✅ platform 위에 있는 경우
            if (icicleX >= platformLeftX && icicleX <= platformRightX)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;

                float newY = platformTopY + spriteHeight / 2f;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);

                isFalling = false;
            }
            // platform 범위를 벗어난 경우 → 계속 떨어져서 바닥에 멈추도록 둠
        }
    }
}
