using System;
using UnityEngine;

public class SpaceShip : MonoBehaviour, IDamageable
{
    [Header("Movement Settings")]
    [SerializeField] private float flapForce = 5f;
    [SerializeField] private float baseGravityScale = 1f;
    [SerializeField] private float maxGravityScale = 4f;
    [SerializeField] private float gravityIncreaseRate = 0.5f;


    [SerializeField] private float animPitch = 0;
    
    // Public property to access animDelta for external systems (e.g., animations)
    public float AnimPitch => animPitch;
    
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float maxUpRotation = 30f;
    [SerializeField] private float maxDownRotation = -30f;
    
    
    private Rigidbody2D rb;
    private Animator animator;
    private bool isDead = false;
    private float fallTime = 0f;
    private float currentGravityScale;
    

    public Action OnDeath;

    void Start()
    {
        Initialize();
    }


    void Initialize()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Initialize gravity scale
        currentGravityScale = baseGravityScale;
        rb.gravityScale = currentGravityScale;
    }


    void Update()
    {
        if (!LevelManager.Instance.StartGame) 
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            return;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }

        if (isDead) return;

        HandleFlap();
        HandleDynamicGravity();
        HandleRotation();

        animator.SetFloat("Pitch", animPitch);
    }

    private void HandleFlap()
    {
        // TODO: use the new input system
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) Flap();
    }
    
    void HandleDynamicGravity()
    {
        // Only increase gravity when falling (negative velocity)
        if (rb.linearVelocity.y < 0)
        {
            fallTime += Time.deltaTime;
            
            // Calculate new gravity scale based on fall time
            currentGravityScale = Mathf.Lerp(baseGravityScale, maxGravityScale, fallTime * gravityIncreaseRate);
            
            // Apply the new gravity scale
            rb.gravityScale = currentGravityScale;
        }
    }
    
    void HandleRotation()
    {
        // Calculate the target rotation based on the bird's velocity
        float targetRotation;
        
        if (rb.linearVelocity.y > 0)
        {
            // Bird is moving upward - rotate to maxUpRotation
            targetRotation = maxUpRotation;
        }
        else
        {
            // Bird is falling - calculate rotation based on downward velocity
            // The faster it falls, the more it rotates down (up to maxDownRotation)
            float fallSpeed = Mathf.Abs(rb.linearVelocity.y);
            float rotationRatio = Mathf.Clamp01(fallSpeed / 10f); // Adjust divisor to control sensitivity
            targetRotation = Mathf.Lerp(0f, maxDownRotation, rotationRatio);
        }
        
        // Smoothly rotate towards the target rotation
        float currentRotation = transform.eulerAngles.z;
        
        // Handle angle wrapping (Unity uses 0-360 degrees)
        if (currentRotation > 180f)
            currentRotation -= 360f;
            
        float newRotation = Mathf.LerpAngle(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        // Apply the rotation
        transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
        
        // Update animDelta based on current rotation
        UpdateAnimDelta(newRotation);
    }
    
    void UpdateAnimDelta(float currentRotation)
    {
        // Map rotation to animDelta:
        // maxUpRotation (30°) -> animDelta = 1
        // 0° -> animDelta = 0  
        // maxDownRotation (-30°) -> animDelta = -1
        
        if (currentRotation >= 0)
        {
            // Positive rotation: interpolate between 0 and 1
            animPitch = Mathf.Lerp(0f, 1f, currentRotation / maxUpRotation);
        }
        else
        {
            // Negative rotation: interpolate between 0 and -1
            animPitch = Mathf.Lerp(0f, -1f, Mathf.Abs(currentRotation) / Mathf.Abs(maxDownRotation));
        }
        
        // Clamp to ensure we stay within bounds
        animPitch = Mathf.Clamp(animPitch, -1f, 1f);
    }

    
    public void Flap()
    {
        // Reset fall time and gravity when flapping
        fallTime = 0f;
        currentGravityScale = baseGravityScale;
        rb.gravityScale = currentGravityScale;
        
        // Reset vertical velocity to zero before applying flap force
        // This ensures consistent flap behavior regardless of current velocity
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        
        // Apply upward force to make the bird flap
        rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
    }
    
    // Method to handle bird death (can be called from collision detection)
    public void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        Die();
    }

    public void ResetShip(Vector2 position)
    {
        isDead = false;
        rb.gravityScale = baseGravityScale;
        rb.linearVelocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
        transform.position = position;
        Flap();
    }
}
