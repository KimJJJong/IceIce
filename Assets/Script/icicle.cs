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
            gameObject.tag = "Ground";
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
        // bottomPlatform의 Collider 가져오기
        Collider2D platformCol = bottomPlatform.GetComponent<Collider2D>();

        // 이 icicle의 Collider가 platform과 충돌하고 있는지 확인
        if (col.IsTouching(platformCol))
        {
            // platform 위에 정확히 멈추게 설정
            float platformTopY = platformCol.bounds.max.y;
            float icicleHalfHeight = col.bounds.extents.y;
            float newY = platformTopY + icicleHalfHeight;

            // 낙하 정지 및 위치 보정
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            isFalling = false;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // icicle 태그는 아직 Ground가 아닌 상태에서만
        if (collision.gameObject.CompareTag("Untagged"))
        {
            // 플레이어 바닥 위치
            float playerBottomY = col.bounds.min.y;

            // 충돌 대상의 위쪽 위치
            float targetTopY = collision.collider.bounds.max.y;

            // 수직 거리 차이를 비교해서 바닥끼리 맞닿은 경우만 감지
            if (Mathf.Abs(playerBottomY - targetTopY) < 0.05f)
            {
                collision.gameObject.tag = "Ground";
                Debug.Log("Icicle 태그가 Ground로 변경되었습니다.");
            }
        }
    }
}
