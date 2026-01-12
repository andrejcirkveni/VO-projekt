using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Sound Clips")]
    public AudioClip music;
    public AudioClip flame;
    public AudioClip heal;
    public AudioClip freeze;
    public AudioClip punch;
    public AudioClip click;

    [Header("Settings")]
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.6f;
    public bool randomPitch = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --------------------
    // CORE
    // --------------------

    void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        if (randomPitch)
            sfxSource.pitch = Random.Range(0.9f, 1.1f);
        else
            sfxSource.pitch = 1f;

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    // --------------------
    // PUBLIC METHODS
    // --------------------

    public void PlayPunch()
    {
        PlaySFX(punch);
    }

    public void PlayFlame()
    {
        PlaySFX(flame);
    }

    public void PlayFreeze()
    {
        PlaySFX(freeze);
    }

    public void PlayHeal()
    {
        PlaySFX(heal);
    }

    public void PlayClick()
    {
        PlaySFX(click);
    }

    // --------------------
    // MUSIC
    // --------------------

    public void PlayMusic()
    {
        if (music == null) return;

        musicSource.clip = music;
        musicSource.volume = musicVolume;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
