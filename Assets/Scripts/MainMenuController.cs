using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameMode
{
    public static bool IsSingleplayer;
}

public class MainMenuController : MonoBehaviour
{
    private AudioSource musicSource;

    // Poveži AudioClip iz foldera Audio u Inspectoru
    public AudioClip menuMusic;

    void Start()
    {
        // Dodaj AudioSource komponentu na ovaj GameObject
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = menuMusic;
        musicSource.loop = true; // glazba se vrti cijelo vrijeme
        musicSource.playOnAwake = false;
        musicSource.volume = 0.6f;
        musicSource.Play();
    }

    public void OnSingleplayerClicked()
    {
        GameMode.IsSingleplayer = true;
        SceneManager.LoadScene("GameScene");
    }

    public void OnMultiplayerClicked()
    {
        GameMode.IsSingleplayer = false;
        SceneManager.LoadScene("ModelSelect");
    }
}

