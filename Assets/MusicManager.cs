using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Tooltip("Deze AudioSource speelt de muziek af. Wordt automatisch gevonden als je hem leeg laat.")]
    public AudioSource musicSource;

    [Tooltip("Sleep hier al je liedjes in de gewenste volgorde.")]
    public AudioClip[] musicPlaylist;

    private int currentTrackIndex = -1;

    void Awake()
    {
        if (musicSource == null)
        {
            musicSource = GetComponent<AudioSource>();
        }
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false;
        }
    }

    void Start()
    {
        NextTrack();
    }

    public void NextTrack()
    {
        if (musicPlaylist.Length == 0)
        {
            Debug.LogWarning("De music playlist is leeg! Voeg liedjes toe in de Inspector.");
            return;
        }

        currentTrackIndex = (currentTrackIndex + 1) % musicPlaylist.Length;

        musicSource.clip = musicPlaylist[currentTrackIndex];
        musicSource.Play();

        Debug.Log("Speelt nu: " + musicSource.clip.name);
    }
}