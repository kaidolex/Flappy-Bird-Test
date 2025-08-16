using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button restartButton = null;


    private void Awake()
    {
        LevelManager.Instance.SpaceShip.OnDeath += ShowRestartButton;
    }

    public void StartGame()
    {
        LevelManager.Instance.StartLevel();
        startButton.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        LevelManager.Instance.RestartLevel();
        restartButton.gameObject.SetActive(false);
    }

    public void ShowRestartButton()
    {
        restartButton.gameObject.SetActive(true);
    }
}
