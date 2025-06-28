using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class DragLine : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1f;
    [SerializeField] private float lineThickness = 0.1f;
    [SerializeField] private Material lineMaterial;

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
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, startPos);
    }

    public void UpdateDraw()
    {
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(1, endPos);
    }

    public void FinalizeDraw()
    {
        Vector2[] colliderPoints = { startPos, endPos };
        edgeCollider.points = colliderPoints;

        Destroy(gameObject, destroyDelay);  // 선 개별 제거
    }
}
