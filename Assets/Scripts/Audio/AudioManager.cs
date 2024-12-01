using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip errorClip;
    [SerializeField] private AudioClip clickClip;

    private void Awake()
    {
        // Implement the Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioManager: Attempted to play a null AudioClip.");
            return;
        }

        // Play the clip using PlayOneShot to allow overlapping sounds
        audioSource.PlayOneShot(clip, Mathf.Clamp01(volume));
    }
    public void PlayErrorSound()
    {
        audioSource.PlayOneShot(errorClip);
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickClip);
    }
}
