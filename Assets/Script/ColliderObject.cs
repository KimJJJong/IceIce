using UnityEngine;

public class CollisionObject : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log($"충돌 감지: {other.gameObject.name}");
    }
}
