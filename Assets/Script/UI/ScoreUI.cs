using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText= null;

    private void Awake()
    {
        LevelManager.Instance.PointsCounter.OnPointsChanged += OnPointsChanged;
    }

    private void OnPointsChanged(int points, int bestPoints)
    {
        scoreText.text = $"Score: {points}\nBest: {bestPoints}";
    }
}
