using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMusicPlayer : MonoBehaviour
{
    public static MenuMusicPlayer Instance;

    [Header("Music")]
    public AudioClip menuMusic;

    [Header("UI Click Sound")]
    public AudioClip clickSound; // staviti u Resources ili ručno dodati u Inspector

    private AudioSource musicSource;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // AudioSource za glazbu i click
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.clip = menuMusic;
            musicSource.loop = true;
            musicSource.playOnAwake = false;
            musicSource.volume = 0.6f;
            musicSource.Play();

            // Ako clickSound nije dodan, pokuša učitati iz Resources
            if (clickSound == null)
            {
                clickSound = Resources.Load<AudioClip>("Audio/click");
                if (clickSound == null)
                    Debug.LogWarning("Click sound nije pronađen u Resources/Audio/click!");
            }

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
        if (scene.name == "GameScene")
        {
            if (musicSource.isPlaying)
                musicSource.Pause();
        }
        else
        {
            if (!musicSource.isPlaying)
                musicSource.UnPause();
        }

        // Automatski dodaj click zvuk svim Button-ima u sceni
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button btn in buttons)
        {
            btn.onClick.RemoveListener(PlayClickSound); // da ne dodajemo više puta
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    void PlayClickSound()
    {
        if (clickSound != null)
        {
            musicSource.PlayOneShot(clickSound, 1.0f); 
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


