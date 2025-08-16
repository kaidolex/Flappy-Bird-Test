using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : PraganoidSystems.Utils.Singleton<LevelManager>
{
    [SerializeField] private Transform shipStartPosition = null;
    [SerializeField] private SpaceShip spaceShip = null;
    [SerializeField] private AsteroidSpawner asteroidSpawner = null;
    [SerializeField] private Parallax[] parallax = null;
    [SerializeField] private PointsCounter pointsCounter = null;

    public SpaceShip SpaceShip => spaceShip;
    public AsteroidSpawner AsteroidSpawner => asteroidSpawner;
    public PointsCounter PointsCounter => pointsCounter;

    private bool startGame = false;

    public bool StartGame => startGame;

    protected override void Start()
    {
        base.Start();

        if (spaceShip == null) spaceShip = FindFirstObjectByType<SpaceShip>();
        if (asteroidSpawner == null) asteroidSpawner = FindFirstObjectByType<AsteroidSpawner>();
        if (parallax == null) parallax = FindObjectsByType<Parallax>(FindObjectsSortMode.None);
        if (pointsCounter == null) pointsCounter = FindFirstObjectByType<PointsCounter>();
    }

    public void StartLevel()
    {
        startGame = true;
        asteroidSpawner.StartSpawning();
        spaceShip.Flap();
    }

    // TODO: Add a way to restart the level
    public void RestartLevel()
    {
        ResetParallax();
        asteroidSpawner.ResetSpawner();
        pointsCounter.ResetPoints();
        spaceShip.ResetShip(shipStartPosition.position);
    }

    void ResetParallax()
    {
        foreach (var parallax in parallax)
        {
            parallax.ResetParallax();
        }
    }
}
