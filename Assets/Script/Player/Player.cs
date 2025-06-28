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

    public float moveSpeed = 3f;
    public float animationInterval = 0.2f;

    private float animationTimer = 0f;
    private int walkFrameIndex = 0;

    private bool isJumping = false;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = Player_standing;
    }

    void Update()
    {
        HandleMove();
        HandleJump();
    }

    void HandleMove()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h != 0)
        {
            sr.flipX = (h < 0);
            transform.Translate(h * moveSpeed * Time.deltaTime, 0, 0);

            if (!isJumping)
            {
                animationTimer += Time.deltaTime;
                if (animationTimer >= animationInterval)
                {
                    walkFrameIndex = (walkFrameIndex + 1) % 4;

                    switch (walkFrameIndex)
                    {
                        case 0:
                        case 2:
                            sr.sprite = Player_standing;
                            break;
                        case 1:
                            sr.sprite = Player_walking1;
                            break;
                        case 3:
                            sr.sprite = Player_walking2;
                            break;
                    }

                    animationTimer = 0f;
                }
            }
            else
            {
                // 점프 중에는 항상 점프 스프라이트
                sr.sprite = Player_jump;
            }
        }
        else
        {
            if (!isJumping)
            {
                sr.sprite = Player_standing;
            }

            walkFrameIndex = 0;
            animationTimer = 0f;
        }
    }

    void HandleJump()
    {
        if (!isJumping && Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            StartCoroutine(JumpCoroutine());
        }
    }

    IEnumerator JumpCoroutine()
    {
        float jumpHeight = 2f;
        float jumpSpeed = 5f;
        float progress = 0f;

        sr.sprite = Player_jump;

        // 위로 점프
        while (progress < jumpHeight)
        {
            float step = jumpSpeed * Time.deltaTime;
            transform.Translate(0, step, 0);
            progress += step;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f); // 공중 정지

        // 착지
        while (progress > 0f)
        {
            float step = jumpSpeed * Time.deltaTime;
            transform.Translate(0, -step, 0);
            progress -= step;
            yield return null;
        }

        isJumping = false;
        sr.sprite = Player_standing;
    }
}
