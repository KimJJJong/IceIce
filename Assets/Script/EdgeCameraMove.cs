using UnityEngine;

public class EdgeCameraMove : MonoBehaviour
{
    public static EdgeCameraMove Instance = new EdgeCameraMove();

    [Header("카메라 이동 설정")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float edgeThreshold = 30f;
    [SerializeField] private float smoothTime = 0.15f;

    [Header("맵 경계 설정")]
/*    [SerializeField] private Vector2 minPos;
    [SerializeField] private Vector2 maxPos;*/
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;



    [Header("플레이어 따라가기")]
    [SerializeField] private Transform player;
    [SerializeField] private bool followPlayer = false;

    private Vector3 targetPos;
    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    void Start()
    {
        cam = Camera.main;
        targetPos = transform.position;
    }

    void FixedUpdate()
    {
        if (followPlayer)
        {
            FollowPlayer();
        }
        else
        {
            EdgeMoveControl();
        }

        SmoothMove();
    }

    private void EdgeMoveControl()
    {
        Vector3 moveDir = Vector3.zero;
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x < edgeThreshold)
            moveDir.x = -1;
        else if (mousePos.x > Screen.width - edgeThreshold)
            moveDir.x = 1;

        if (mousePos.y < edgeThreshold)
            moveDir.y = -1;
        else if (mousePos.y > Screen.height - edgeThreshold)
            moveDir.y = 1;

        if (moveDir != Vector3.zero)
        {
            targetPos += moveDir.normalized * moveSpeed * Time.deltaTime;

            targetPos.x = Mathf.Clamp(targetPos.x, minPos.transform.position.x, maxPos.transform.position.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.transform.position.y, maxPos.transform.position.y);
        }
    }

    private void FollowPlayer()
    {
        targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minPos.transform.position.x, maxPos.transform.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minPos.transform.position.y, maxPos.transform.position.y);
    }

    private void SmoothMove()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

    // 외부에서 호출하는 함수 (필요하면)
    public void SetFollowPlayer(bool follow)
    {
        followPlayer = follow;
    }
}
