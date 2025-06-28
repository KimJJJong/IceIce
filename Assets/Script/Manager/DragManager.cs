using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class DragManager : MonoBehaviour
{
    [Header("설정")]
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

        // 머티리얼 적용
        if (lineMaterial != null)
            lineRenderer.material = lineMaterial;

        // 두께 설정 (시작/끝 동일하게)
        lineRenderer.startWidth = lineThickness;
        lineRenderer.endWidth = lineThickness;

        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDraw();
        }
        else if (Input.GetMouseButton(0))
        {
            UpdateDraw();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            FinalizeDraw();
        }
    }

    private void StartDraw()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, startPos);
    }

    private void UpdateDraw()
    {
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(1, endPos);
    }

    private void FinalizeDraw()
    {
        Vector2[] colliderPoints = new Vector2[2];
        colliderPoints[0] = startPos;
        colliderPoints[1] = endPos;

        edgeCollider.points = colliderPoints;
    }
}
