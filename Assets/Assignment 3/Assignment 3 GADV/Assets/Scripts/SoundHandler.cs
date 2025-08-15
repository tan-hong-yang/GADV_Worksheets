using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [Header("Sound Clips")]
    public AudioClip pickupSound;
    public AudioClip laserFireSound;
    public AudioClip[] sparkSounds; // Array of spark sounds to choose from
    
    [Header("Settings")]
    [Range(0, 1)] public float laserVolume = 0.7f;
    [Range(0, 1)] public float sparkVolume = 0.5f;
    
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

    public void PlayPickupSound()
    {
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }

    public void PlayLaserFireSound()
    {
        if (laserFireSound != null)
        {
            audioSource.volume = laserVolume;
            audioSource.PlayOneShot(laserFireSound);
        }
    }

    public void PlayRandomSparkSound()
    {
        if (sparkSounds != null && sparkSounds.Length > 0)
        {
            AudioClip randomSound = sparkSounds[Random.Range(0, sparkSounds.Length)];
            audioSource.volume = sparkVolume;
            audioSource.PlayOneShot(randomSound);
        }
    }

    public void PlayLaserFireSoundWithPitch(float minPitch = 0.95f, float maxPitch = 1.05f)
    {
        if (laserFireSound != null)
        {
            audioSource.volume = laserVolume;
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(laserFireSound);
            audioSource.pitch = 1f; // Reset to default
        }
    }
}