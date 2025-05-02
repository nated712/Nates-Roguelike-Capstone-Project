using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MenuFunctions : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;
    private float delay = 0.2f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }

    public void GoToGame()
    {
        PlayClickSound();
        Invoke(nameof(LoadGame), delay);
    }

    public void GoToCredits()
    {
        PlayClickSound();
        Invoke(nameof(LoadCredits), delay);
    }

    public void GoToMainMenu()
    {
        PlayClickSound();
        Invoke(nameof(LoadMainMenu), delay);
    }

    public void QuitGame()
    {
        PlayClickSound();
        Invoke(nameof(Quit), delay);
    }

    private void LoadGame() => SceneManager.LoadScene(2);
    private void LoadCredits() => SceneManager.LoadScene(1);
    private void LoadMainMenu() => SceneManager.LoadScene(0);
    private void Quit()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
