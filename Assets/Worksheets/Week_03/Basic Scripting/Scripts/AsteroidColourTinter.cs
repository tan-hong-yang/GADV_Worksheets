using UnityEngine;

public class AsteroidColourTinter  : MonoBehaviour
{
    void Update ()
    {
        if (Input .GetKeyDown(KeyCode.Space))
        {
            // Access the SpriteRenderer component
            SpriteRenderer spriteRenderer = GetComponent <SpriteRenderer>();

            // Change the color to blue
            spriteRenderer.color = Color.blue;
        }
    }
}
