using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class icicle : MonoBehaviour
{
    [SerializeField] private List<GameObject> targetObjects = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"트리거 감지: {other.gameObject.name}");

        foreach (GameObject obj in targetObjects)
        {
            if (obj == null) continue;

            if (obj.TryGetComponent(out Rigidbody2D rb))
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.freezeRotation = true;
            }
            else
            {
                Debug.LogWarning($"대상 {obj.name}에 Rigidbody2D가 없습니다.");
            }
        }
    }


}
