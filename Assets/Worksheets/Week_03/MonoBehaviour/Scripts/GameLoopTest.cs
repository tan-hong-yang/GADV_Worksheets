using UnityEngine;

public class GameLoopTest : MonoBehaviour
{
    private float lastFixedUpdateTime;
    private float lastUpdateTime;
    private float lastLateUpdateTime;

    void FixedUpdate()
    {
        float deltaTime  = Time.time  - lastFixedUpdateTime;
        lastFixedUpdateTime  = Time.time;
        Debug.Log($"FixedUpdate: {deltaTime:F4}s since last call.");
    }

    void Update()
    {
        float deltaTime = Time.time - lastUpdateTime;
        lastUpdateTime = Time.time;
        Debug.Log($"Update: {deltaTime:F4}s since last call.");
    }

    void LateUpdate()
    {
        float deltaTime = Time.time - lastLateUpdateTime;
        lastLateUpdateTime = Time.time;
        Debug.Log($"LateUpdate: {deltaTime:F4}s since last call.");
    }
}
