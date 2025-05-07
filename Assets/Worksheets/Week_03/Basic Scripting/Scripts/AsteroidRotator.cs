using UnityEngine;

public class AsteroidRotator : MonoBehaviour
{
    public float rotationSpeedd = 100f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0,0,-rotationSpeedd* Time.deltaTime);
    }
}
