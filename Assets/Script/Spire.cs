using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spire : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ÇÃ·¹ÀÌ¾î°¡ Äã¸¶ ´êÀ½!");
            Kill();
        }
    }

    private void Kill()
    {
        // UIManager.Instance.ShowGameOverClear_Panel();
        TimeManager.Instance.ForceEndGame();
    }
}
