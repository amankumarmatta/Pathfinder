using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if an AudioSource component is attached
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject.");
        }
        else
        {
            // Check if the current scene is the Start scene
            if (SceneManager.GetActiveScene().name == "Start")
            {
                // Play the audio
                audioSource.Play();
            }
        }
    }

    public void TurnOffAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    // Call this method when the "Turn On Audio" button is clicked
    public void TurnOnAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
