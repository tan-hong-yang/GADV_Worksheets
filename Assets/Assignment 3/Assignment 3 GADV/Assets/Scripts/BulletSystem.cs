using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletSystem : MonoBehaviour
{
    [Header("Spark Settings")]
    public GameObject sparkPrefab; // Assign in Inspector
    public int sparkCount = 6;
    public float sparkForce = 3f;
    public float sparkUpwardForce = 2f;
    public float sparkRandomness = 0.5f;
    public float sparkLifetime = 5f;
    public LayerMask groundLayer;
    public LayerMask bacteriaLayer;

    private float directionMultiplier = 1f;

    public void Initialize(float directionX, LayerMask groundLayerMask, LayerMask bacteriaLayerMask)
    {
        directionMultiplier = -Mathf.Sign(directionX);
        groundLayer = groundLayerMask;
        bacteriaLayer = bacteriaLayerMask;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int collidedLayer = collision.gameObject.layer;
        
        // Check for bacteria collision first
        if (((1 << collidedLayer) & bacteriaLayer) != 0)
        {
            BacteriaHealth bacteriaHealth = collision.gameObject.GetComponent<BacteriaHealth>();
            if (bacteriaHealth != null)
            {
                bacteriaHealth.TakeDamage();
                bacteriaHealth.PlayPainSound(); // Directly call the sound
            }
        }
        // Then check for ground collision
        else if (((1 << collidedLayer) & groundLayer) != 0)
        {
            CreateSparks();
            PlaySparkSound();
        }

        Destroy(gameObject);
    }

    void CreateSparks()
    {
        if (sparkPrefab == null) return;

        for (int i = 0; i < sparkCount; i++)
        {
            GameObject spark = Instantiate(sparkPrefab, transform.position, Quaternion.identity);
            
            Rigidbody2D rb = spark.GetComponent<Rigidbody2D>();
            if (rb == null) rb = spark.AddComponent<Rigidbody2D>();
            
            Collider2D col = spark.GetComponent<Collider2D>();
            if (col == null) spark.AddComponent<CircleCollider2D>();

            float randomAngle = Random.Range(-45f, 45f) * directionMultiplier;
            Vector2 forceDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.right * directionMultiplier;
            
            float randomForceVariation = 1f + Random.Range(-sparkRandomness, sparkRandomness);
            Vector2 force = forceDirection * sparkForce * randomForceVariation;
            force.y += sparkUpwardForce * randomForceVariation;
            
            rb.AddForce(force, ForceMode2D.Impulse);
            Destroy(spark, sparkLifetime);
        }
    }

    void PlaySparkSound()
    {
        GameObject soundHandlerObj = GameObject.Find("SoundHandler");
        if (soundHandlerObj)
        {
            var soundHandler = soundHandlerObj.GetComponent<SoundHandler>();
            if (soundHandler) soundHandler.PlayRandomSparkSound();
        }
    }
}