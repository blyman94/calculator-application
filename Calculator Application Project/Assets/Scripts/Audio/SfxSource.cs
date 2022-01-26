using UnityEngine;

/// <summary>
/// Allows sound effects to be played through a central SFX source.
/// </summary>
public class SfxSource : MonoBehaviour
{
    /// <summary>
    /// Audio Source through which the sound effects will be played.
    /// </summary>
    [Tooltip("Audio Source through which the sound effects will be played.")]
    [SerializeField] private AudioSource sfxSource;

    /// <summary>
    /// Plays the passed audio clip once.
    /// </summary>
    /// <param name="sfxClip">SFX clip to play.</param>
    public void PlaySoundEffect(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }
}
