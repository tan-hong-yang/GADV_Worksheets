using UnityEngine;

namespace GADVDataTypes
{
    public class VarExample : MonoBehaviour
    {
        void Start()
        {
	     // Inferred as SpriteRenderer
            var sprite = GetComponent<SpriteRenderer>(); 

            // Inferred as Color
            var spriteColor = sprite.color; 

            // Inferred as Vector2            
            var spriteSize = sprite.bounds.size;        

            Debug.Log("Sprite Color: " + spriteColor);
            Debug.Log("Sprite Size: " + spriteSize);
        }
    }
}
