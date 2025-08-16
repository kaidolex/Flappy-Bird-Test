using System;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private int points = 0;
    [SerializeField] private int bestPoints = 0;

    private bool canCount = true;

    public Action<int, int> OnPointsChanged;

    private void Awake()
    {
        LevelManager.Instance.SpaceShip.OnDeath += StopCounting;
    }

    public void ResetPoints()
    {
        canCount = true;
        points = 0;
        OnPointsChanged?.Invoke(points, bestPoints);
    }

    public void AddPoints(int points)
    {
        this.points += points;

        SetBestPoints();

        OnPointsChanged?.Invoke(this.points, this.bestPoints);
    }

    public void SetBestPoints()
    {
        if (points < bestPoints) return;

        bestPoints = points;
    }

    public void StopCounting()
    {
        canCount = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canCount) return;
        if (!collision.gameObject.CompareTag("Asteroid")) return;

        AddPoints(1);
    }
}
