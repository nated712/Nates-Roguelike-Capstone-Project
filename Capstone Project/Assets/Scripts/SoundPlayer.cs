using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleAudioPlayer : MonoBehaviour
{
    public AudioClip audioClip; // Drag audio to this
    public bool playOnStart = true; // can set to play on awake or not

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false; // Prevent it from playing immediately if false
        audioSource.clip = audioClip;
    }

    void Start()
    {
        if (playOnStart && audioClip != null)
        {
            PlayAudio();
        }
    }

    public void PlayAudio()
    {
        if (audioClip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to " + gameObject.name);
        }
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
