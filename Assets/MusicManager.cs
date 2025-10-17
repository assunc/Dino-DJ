using UnityEngine;
using UnityEngine.InputSystem; // Belangrijk voor het nieuwe Input System

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

    // Update wordt elke frame aangeroepen
    void Update()
    {
        // Controleer of de 'S'-toets is ingedrukt
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleMute();
        }
    }

    // Functie om de mute-status te wisselen
    public void ToggleMute()
    {
        // Wissel de mute-status van de AudioSource
        musicSource.mute = !musicSource.mute;

        // Log de nieuwe status naar de console
        if (musicSource.mute)
        {
            Debug.Log("Muziek gedempt.");
        }
        else
        {
            Debug.Log("Muziek speelt weer.");
        }
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

        // Zorg ervoor dat de muziek niet gedempt is als je naar het volgende nummer gaat
        musicSource.mute = false;

        Debug.Log("Speelt nu: " + musicSource.clip.name);
    }
}