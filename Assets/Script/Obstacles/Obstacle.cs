using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.TryGetComponent<IDamageable>(out var damageable)) return;

        damageable.TakeDamage(1);
    }
}