using UnityEngine;

public class TitanLifecycle : MonoBehaviour
{
    void Awake()
    {
        Debug.Log ("Titan-01: Initial boot sequence initiated.");
    }

    void Start()
    {
        Debug.Log("Titan-01: Power core stable. Ready for action.");
    }

    void Update()
    {
        Debug.Log("Titan-01: Scanning for threats...");
    }
}
