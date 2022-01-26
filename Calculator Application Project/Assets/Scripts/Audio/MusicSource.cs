using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Plays a sequential list of audio clips through the music audio source.
/// </summary>
public class MusicSource : MonoBehaviour
{
    /// <summary>
    /// Should the song order be randomized?
    /// </summary>
    [Tooltip("Should the song order be randomized?")]
    [SerializeField] private bool randomizeSongOrder;

    /// <summary>
    /// Audio Source through which the music will be played.
    /// </summary>
    [Tooltip("Audio Source through which the music will be played.")]
    [SerializeField] private AudioSource musicSource;

    /// <summary>
    /// Reference to the text of the mute/unmute button to update.
    /// </summary>
    [Tooltip("Reference to the text of the mute/unmute button to update.")]
    [SerializeField] private TextMeshProUGUI muteButtonText;

    [Header("Silence")]
    /// <summary>
    /// Should a silent clip be played between songs?
    /// </summary>
    [Tooltip("Should a silent clip be played between songs?")]
    [SerializeField] private bool alternateSilence;

    /// <summary>
    /// If silence should be played, how long should it be played for?
    /// </summary>
    [Tooltip("If silence should be played, how long should it be played for " +
        "(in seconds)?")]
    [SerializeField] int silenceLength;

    /// <summary>
    /// List of clips for the music source to cycle through. This should be a 
    /// list of songs to make a soundtrack for the game.
    /// </summary>
    [Header("Audio Clips")]
    [Tooltip("List of clips for the music source to cycle through. This " +
        "should be a  list of songs to make a soundtrack for the game.")]
    [SerializeField] private List<AudioClip> clipsToCycle;

    /// <summary>
    /// The silent clip to be played between songs. The length of the silent
    /// clip can be multiplied by the silenceLength to get the full length of
    /// silence between songs.
    /// </summary>
    [Tooltip("The silent clip to be played between songs. The length of " +
        "the silent clip can be multiplied by the silenceLength to get the " +
        "full length of silence between songs.")]
    [SerializeField] private AudioClip silenceClip;

    /// <summary>
    /// Index of the song currently playing.
    /// </summary>
    private int currentTrackIndex;

    /// <summary>
    /// Number of times the silent clip has been repeated.
    /// </summary>
    private int silenceReps;

    /// <summary>
    /// Volume of the music when playing.
    /// </summary>
    private float volume;

    #region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        if (musicSource != null && !clipsToCycle.IsEmpty())
        {
            if (randomizeSongOrder)
            {
                currentTrackIndex = Random.Range(0, clipsToCycle.Count);
            }
            else
            {
                currentTrackIndex = 0;
            }
            volume = musicSource.volume;
            musicSource.clip = clipsToCycle[currentTrackIndex];
            musicSource.Play();
        }

        silenceReps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            if (alternateSilence && silenceReps < silenceLength)
            {
                musicSource.clip = silenceClip;
                silenceReps++;
            }
            else
            {
                if (randomizeSongOrder)
                {
                    currentTrackIndex = Random.Range(0, clipsToCycle.Count);
                }
                else
                {
                    if (currentTrackIndex + 1 < clipsToCycle.Count)
                    {
                        currentTrackIndex++;
                    }
                    else
                    {
                        currentTrackIndex = 0;
                    }
                }

                musicSource.clip = clipsToCycle[currentTrackIndex];
                silenceReps = 0;
            }

            musicSource.Play();
        }
    }
    #endregion

    /// <summary>
    /// Toggles mute state of the calculator music.
    /// </summary>
    public void ToggleMute()
    {
        if (musicSource.volume == 0)
        {
            musicSource.volume = volume;
            muteButtonText.text = "Mute";
        }
        else
        {
            musicSource.volume = 0;
            muteButtonText.text = "Unmute";
        }
    }
}
