using UnityEngine;

public class Asteroid : Obstacle, IDamageable
{
    [SerializeField] private float minSpeed = 0.1f;
    [SerializeField] private float maxSpeed = 5f;

    private float speed;

    void Start()
    {
        SetSpeed(Random.Range(minSpeed, maxSpeed));
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }

    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
