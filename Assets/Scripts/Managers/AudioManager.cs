using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public static AudioManager Instance { get; private set; }



    // Game Loop Methods---------------------------------------------------------------------------

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    // Member Methods------------------------------------------------------------------------------

    /// <summary>
    /// Play the desired SFX
    /// </summary>
    /// <param name="audioClip">sound asset</param>
    /// <param name="volume">volume degree (0 : 1)</param>
    public void PlaySFX(AudioClip audioClip, float volume = 1.0f)
    {
        _audioSource.PlayOneShot(audioClip, volume);
    }
}
