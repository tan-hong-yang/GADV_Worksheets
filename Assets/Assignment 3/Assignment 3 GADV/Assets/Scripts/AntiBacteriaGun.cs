using UnityEngine;

public class AntiBacteriaGun : MonoBehaviour
{
    [Header("Pickup Float")]
    public float hoverSpeed = 2f;
    public float hoverHeight = 0.5f;

    [Header("Shooting")]
    public GameObject bulletPrefab;          // Assign "Bullet" prefab in Inspector
    public float bulletSpeed = 10f;         // Speed of the bullet
    public Transform muzzle;                // Optional: set a child transform as muzzle
    public float cooldown = 0.5f;           // Cooldown between shots in seconds
    public LayerMask groundLayer;

    public LayerMask bacteriaLayer;

    [Header("Bullet Spawn Offset")]
    public Vector2 bulletOffset = new Vector2(0.15f, 0f); // X (forward), Y (vertical)

    [Header("Hold Position Relative to Player")]
    public Vector3 holdOffset = new Vector3(1f, 0f, 0f);

    public bool PickedUp = false;

    private Vector3 startPos;
    private Transform playerTransform;
    private SpriteRenderer playerSprite;
    private SpriteRenderer gunSprite;
    private float lastShotTime;              // Time when last shot was fired

    void Start()
    {
        startPos = transform.position;
        gunSprite = GetComponent<SpriteRenderer>();
        lastShotTime = -cooldown; // Allow immediate first shot
    }

    void Update()
    {
        if (PickedUp && playerTransform != null)
        {
            // Determine facing: right = +1, left = -1
            float dirX = (playerSprite != null && playerSprite.flipX) ? -1f : 1f;

            // Stick to player hand
            transform.position = playerTransform.position + 
                               new Vector3(dirX * holdOffset.x, holdOffset.y, holdOffset.z);

            // Flip gun with player
            if (gunSprite != null) gunSprite.flipX = (dirX < 0f);

            // Shoot only on mouse button DOWN and if cooldown has passed
            if (Input.GetMouseButtonDown(0) && Time.time >= lastShotTime + cooldown)
            {
                Fire(dirX);
                lastShotTime = Time.time;
            }
        }
        else
        {
            // Idle hover before pickup
            float offset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
            transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);
        }
    }

    void Fire(float dirX)
    {
        if (bulletPrefab == null) return;

        // Play laser fire sound through SoundHandler
        PlayLaserSound();

        Vector3 basePos = muzzle != null ? muzzle.position : transform.position;
        Vector3 spawnPos = basePos + new Vector3(dirX * bulletOffset.x, bulletOffset.y, 0f);

        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null) rb = bullet.AddComponent<Rigidbody2D>();
        rb.velocity = new Vector2(dirX * bulletSpeed, 0);
        
        // Get or add the BulletWithSparks component
        BulletSystem bulletWithSparks = bullet.GetComponent<BulletSystem>();
        if (bulletWithSparks == null) bulletWithSparks = bullet.AddComponent<BulletSystem>();
        bulletWithSparks.Initialize(dirX, groundLayer, bacteriaLayer);
        
        Destroy(bullet, 3f);
    }

    void PlayLaserSound()
    {
        GameObject soundHandlerObj = GameObject.Find("SoundHandler");
        if (soundHandlerObj)
        {
            var soundHandler = soundHandlerObj.GetComponent<SoundHandler>();
            if (soundHandler) soundHandler.PlayLaserFireSound();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!PickedUp && other.CompareTag("Player"))
        {
            PickedUp = true;
            playerTransform = other.transform;
            playerSprite = playerTransform.GetComponent<SpriteRenderer>();

            // Play pickup sound through SoundHandler
            GameObject soundHandlerObj = GameObject.Find("SoundHandler");
            if (soundHandlerObj)
            {
                var soundHandler = soundHandlerObj.GetComponent<SoundHandler>();
                if (soundHandler) soundHandler.PlayPickupSound();
            }
        }
    }
}