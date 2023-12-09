using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject.");
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Start")
            {
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
    public void TurnOnAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
