using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class FlyingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    //[SerializeField] private float destroyDelay = 10f;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D rb;
    private bool isFlying = false;
    private bool isStuck = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;  // 초기 정지
        if (speed > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);  
            transform.localScale = scale;
        }
    }

    void OnMouseUp()
    {
        if (!isFlying)
        {
            Launch();
            AudioManager.Instance.PlaySFX("SFX_NorwhalClick");
            AudioManager.Instance.PlaySFX("SFX_NorwhalFly");
        }
    }

    private void Launch()
    {
        isFlying = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = Vector2.right * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFlying || isStuck) return;

        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            StickToWall();
        }
    }

    private void StickToWall()
    {
        isStuck = true;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        Debug.Log("플랫폼 벽에 고정");
        AudioManager.Instance.PlaySFX("SFX_NorwhalStuck");

        //Destroy(gameObject, destroyDelay);
    }
}
