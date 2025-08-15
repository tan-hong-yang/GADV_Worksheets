using UnityEngine;

public class PainSoundHandler : MonoBehaviour
{
    [Header("Sound Settings")]
    public AudioClip painSound;
    [Range(0, 1)] public float volume = 0.3f;
    [Range(1f, 2f)] public float minPitch = 1f;
    [Range(1f, 2f)] public float maxPitch = 2f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    public void PlayPainSound()
    {
        if (painSound != null)
        {
            // Set random pitch and volume
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.volume = volume;
            
            // Play the sound
            audioSource.PlayOneShot(painSound);
            
        }
    }

    // Optional: Reset pitch after playing
    System.Collections.IEnumerator ResetPitch()
    {
        yield return new WaitForSeconds(0.1f); // Wait for sound to start
        audioSource.pitch = 1f;
    }
}