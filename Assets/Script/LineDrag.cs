using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class LineDrag : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1f;
    [SerializeField] private float lineThickness = 0.1f;
    [SerializeField] private Material lineMaterial;
    [SerializeField] private float minLength = 1f;
    [SerializeField] private GameObject collisionObjPrefab;  // 충돌용 프리팹


    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private Vector2 startPos;
    private Vector2 endPos;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        if (!TryGetComponent(out Rigidbody2D rb))
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (lineMaterial != null)
            lineRenderer.material = lineMaterial;

        lineRenderer.startWidth = lineThickness;
        lineRenderer.endWidth = lineThickness;
        lineRenderer.positionCount = 2;
    }

    public void StartDraw()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, new Vector3(startPos.x, startPos.y, -1f));
        lineRenderer.SetPosition(1, new Vector3(startPos.x, startPos.y, -1f));
    }

    public void UpdateDraw()
    {
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(1, endPos);
    }

    public void FinalizeDraw()
    {
        Vector2[] colliderPoints = { startPos, endPos };
        float distance = Vector2.Distance(startPos, endPos);

        if (distance < minLength)
        {
            Debug.Log("It to Short");
            Destroy(gameObject);
            return;
        }

        // 충돌 전용 오브젝트 소환
        GameObject colObj = Instantiate(collisionObjPrefab);

        // 위치 중앙으로 이동
        Vector2 midPoint = (startPos + endPos) * 0.5f;
        colObj.transform.position = midPoint;

        // 회전 계산
        Vector2 dir = endPos - startPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        colObj.transform.rotation = Quaternion.Euler(0, 0, angle);

        // 크기 조정
        colObj.transform.localScale = new Vector3(distance, colObj.transform.localScale.y, 1f);

        Destroy(colObj, destroyDelay);
        Destroy(gameObject, destroyDelay);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"충돌 감지: {collision.gameObject.name}");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"트리거 감지: {other.gameObject.name}");
    }

}
