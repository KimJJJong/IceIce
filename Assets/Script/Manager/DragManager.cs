using UnityEngine;

public class DragManager : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;

    private LineDrag currentLine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDraw();
        }
        else if (Input.GetMouseButton(0) && currentLine != null)
        {
            currentLine.UpdateDraw();
        }
        else if (Input.GetMouseButtonUp(0) && currentLine != null)
        {
            currentLine.FinalizeDraw();
            currentLine = null;
        }
    }

    private void StartDraw()
    {
        GameObject lineObj = Instantiate(linePrefab);
        currentLine = lineObj.GetComponent<LineDrag>();
        currentLine.StartDraw();
    }
}
