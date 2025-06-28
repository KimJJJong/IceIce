using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class LineDrag : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1f;
    [SerializeField] private float lineThickness = 0.1f;
    [SerializeField] private Material lineMaterial;
    [SerializeField] private float minLength = 1f;

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private Vector2 startPos;
    private Vector2 endPos;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

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

        Debug.Log($"{distance}  minLenght : {minLength}");


        if (distance < minLength)
        {
            Debug.Log("It to Short");
            Destroy(gameObject);  // 너무 짧으면 바로 제거
            return;
        }


        edgeCollider.points = colliderPoints;

        Destroy(gameObject, destroyDelay);  // 선 개별 제거
    }
}
