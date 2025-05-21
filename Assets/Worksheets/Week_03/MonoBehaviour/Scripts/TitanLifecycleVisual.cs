using UnityEngine;


public class TitanLifecycleVisual : MonoBehaviour
{

    private float moveSpeed = 2f;  // Speed of horizontal movement
    private float moveLimit = 3f;  // Maximum distance from start pos
    private Vector3 startPos;
    private int direction = 1;  // 1 = right, -1 = left

    public Color activationColor;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = activationColor;
        activationColor.a = 1f;

        Debug.Log("Awake: Titan-01 is configured.");
    }

    void Start()
    {

        
        // Scale up the sprite’s width and height to 1.25 times.
        transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);

        Debug.Log("Start: Titan-01 scaled for action.");
    }

    void Update()
    {
        // Move left and right to simulate patrolling
        transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);

        // Check if the sprite needs to change direction
        if (Mathf.Abs(transform.position.x - startPos.x) >= moveLimit)
        {
            direction *= -1; // Reverse direction
        }

        Debug.Log("Update: Titan-01 patrolling...");
    }



}
