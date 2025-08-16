using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"ObstacleDestroyer initialized at position: {transform.position}");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log($"ObstacleDestroyer triggered by: {collider.gameObject.name}");
        
        if (!collider.gameObject.TryGetComponent<Obstacle>(out var obstacle)) 
        {
            Debug.Log($"Object {collider.gameObject.name} does not have Obstacle component");
            return;
        }
    
        Debug.Log($"Destroying obstacle: {collider.gameObject.name}");
        Destroy(collider.gameObject);
    }
}
