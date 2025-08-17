using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab = null;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float spawnAngle = 10f;
    [SerializeField] private float spawnMaxScale = 1f;
    [SerializeField] private float spawnMinScale = 0.5f;

    private BoxCollider2D boxCollider = null;
    private bool isSpawning = false;

    private void Awake()
    {
        LevelManager.Instance.SpaceShip.OnDeath += StopSpawning;
    }

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        
        StartSpawning();
        StopSpawning();
    }

    public void StartSpawning()
    {
        if (!isSpawning && boxCollider != null && asteroidPrefab != null)
        {
            isSpawning = true;
            StartCoroutine(SpawnAsteroidsCoroutine());
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    public void ResetSpawner()
    {
        StopSpawning();
        StartSpawning();

        DeleteAllAsteroids();
    }

    public void DeleteAllAsteroids()
    {
        foreach (var asteroid in FindObjectsByType<Asteroid>(FindObjectsSortMode.None))
        {
            Destroy(asteroid.gameObject);
        }
    }

    private IEnumerator SpawnAsteroidsCoroutine()
    {
        // Wait for the initial spawn delay
        yield return new WaitForSeconds(spawnDelay);

        while (isSpawning)
        {
            SpawnAsteroid();
            
            // Wait for the next spawn based on spawn rate
            yield return new WaitForSeconds(1f / spawnRate);
        }
    }

    private void SpawnAsteroid()
    {
        if (asteroidPrefab == null || boxCollider == null) return;

        // Get the bounds of the box collider in world space
        Bounds bounds = boxCollider.bounds;

        // Calculate random position within the bounds
        Vector3 spawnPosition = GetRandomPositionInBounds(bounds);
        
        // Apply spawn angle variation
        Vector3 finalPosition = ApplyAngleVariation(spawnPosition);

        // Create the asteroid
        GameObject asteroid = Instantiate(asteroidPrefab, finalPosition, Quaternion.identity, this.transform);

        // Apply random scale variation
        ApplyRandomScale(asteroid);

        // Optional: Add some random rotation
        asteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        Destroy(asteroid, 12.5f);
    }

    private Vector3 GetRandomPositionInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        
        return new Vector3(randomX, randomY, 0f);
    }

    private Vector3 ApplyAngleVariation(Vector3 basePosition)
    {
        // Apply spawn angle variation (small directional offset)
        float angle = Random.Range(-spawnAngle, spawnAngle);
        float angleInRadians = angle * Mathf.Deg2Rad;
        
        // Small offset based on angle only (fixed small distance)
        float offsetDistance = 0.5f; // Small fixed offset
        Vector3 offset = new Vector3(
            Mathf.Cos(angleInRadians) * offsetDistance,
            Mathf.Sin(angleInRadians) * offsetDistance,
            0f
        );
        
        return basePosition + offset;
    }

    private void ApplyRandomScale(GameObject asteroid)
    {
        float randomScale = Random.Range(spawnMinScale, spawnMaxScale);
        asteroid.transform.localScale = Vector3.one * randomScale;
    }

    void OnDrawGizmosSelected()
    {
        // Draw the spawning bounds in the scene view
        if (boxCollider != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
        }
    }
}
