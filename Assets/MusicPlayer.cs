using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicPlayer : MonoBehaviour
{
    public static MenuMusicPlayer Instance;

    public AudioClip menuMusic;
    private AudioSource musicSource;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Dodaj AudioSource
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.clip = menuMusic;
            musicSource.loop = true;
            musicSource.playOnAwake = false;
            musicSource.volume = 0.6f;
            musicSource.Play();

            // Pretplati se na promjenu scene
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ako smo u GameScene, pauziraj glazbu
        if (scene.name == "GameScene")
        {
            if (musicSource.isPlaying)
                musicSource.Pause();
        }
        else
        {
            // Ako nije GameScene, nastavi glazbu
            if (!musicSource.isPlaying)
                musicSource.UnPause();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

