using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Parallax Settings")]
    [SerializeField] private float speed = 0.5f;

    [SerializeField] private bool stopScrolling = false;

    private Material material;
    private float distance;

    void Awake()
    {
        LevelManager.Instance.SpaceShip.OnDeath += StopScrolling;
    }

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Scroll();
    }

    public void StopScrolling()
    {
        stopScrolling = true;
    }

    public void ResetParallax()
    {
        stopScrolling = false;
        material.mainTextureOffset = Vector2.zero;
        distance = 0;
    }

    void Scroll()
    {
        if (stopScrolling) return;

        distance += Time.deltaTime * speed;

        material.mainTextureOffset = new Vector2(distance, 0);
    }
}