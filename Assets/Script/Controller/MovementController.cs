using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 5f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    [Header("스프라이트 설정")]
    public Sprite idleSprite;
    public Sprite walkSprite1;
    public Sprite walkSprite2;
    public Sprite jumpSprite;

    [SerializeField] private Transform groundCheckPoint;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private float moveInput = 0f;
    private bool isGrounded = false;
    private float walkAnimTimer = 0f;
    private float walkAnimInterval = 0.2f;
    private bool useWalk1 = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(UIManager.Instance.isStartGame is true)
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
            {
                rb2d.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                AudioManager.Instance.PlaySFX("SFX_Movement_Jump");
            }

            UpdateSprite();
        }
    
    }

    void FixedUpdate()
    {
        if (UIManager.Instance.isStartGame is true)
        {
            rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
        GroundCheck();

        }
    }

    private void GroundCheck()
    {
        Vector2 origin = groundCheckPoint.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);

        isGrounded = hit.collider != null;
        Debug.DrawRay(origin, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }

    private void UpdateSprite()
    {
        if (!isGrounded)
        {
            sr.sprite = jumpSprite;
            return;
        }

        if (Mathf.Abs(moveInput) > 0.1f)
        {
            // 방향에 따른 반전
            if (moveInput < 0)
                sr.flipX = true;
            else if (moveInput > 0)
                sr.flipX = false;

            walkAnimTimer += Time.deltaTime;

            if (walkAnimTimer >= walkAnimInterval)
            {
                walkAnimTimer = 0f;
                useWalk1 = !useWalk1;
                AudioManager.Instance.PlaySFX("SFX_Movement_Walk");
            }

            sr.sprite = useWalk1 ? walkSprite1 : walkSprite2;
        }
        else
        {
            sr.sprite = idleSprite;
        }
    }

}
