using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip flame;
    public AudioClip freeze;
    public AudioClip heal;
    public AudioClip punch;

    public void PlayFlame() { PlaySound(flame); }
    public void PlayFreeze() { PlaySound(freeze); }
    public void PlayHeal() { PlaySound(heal); }
    public void PlayPunch() { PlaySound(punch); }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}
