using UnityEngine;

public class BacteriaHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHitPoints = 8;
    private int currentHitPoints;

    void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    // Make this public so bullets can call it
    public void TakeDamage()
    {
        currentHitPoints--;
        Debug.Log($"Bacteria took damage! HP left: {currentHitPoints}");

        if (currentHitPoints <= 0)
        {
            Die();
        }
    }

    // Also make this public for bullets to call
    public void PlayPainSound()
    {
        PainSoundHandler soundHandler = FindObjectOfType<PainSoundHandler>();
        soundHandler?.PlayPainSound();
    }

    void Die()
    {
        Debug.Log("Bacteria destroyed!");
        Destroy(gameObject);
    }
}