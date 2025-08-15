using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public string groundLayerName = "Ground N Platform";
    public float minX = -8f; // Left screen boundary (adjust as needed)

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private int groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the layer index from the name
        groundLayer = LayerMask.NameToLayer(groundLayerName);
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Calculate new velocity
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Prevent going past left boundary
        if (transform.position.x <= minX && rb.velocity.x < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }

        // Flip sprite
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;

        // Bunny hop: jump instantly whenever grounded and space is held
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false; // prevents double-trigger within the same frame
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    return;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = false;
        }
    }
}
