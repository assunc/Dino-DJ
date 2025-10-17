using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Tooltip("Plaats hier de AudioSource component die de muziek zal afspelen.")]
    public AudioSource musicSource;

    [Tooltip("Sleep hier al je liedjes in de gewenste volgorde.")]
    public AudioClip[] musicPlaylist;

    private int currentTrackIndex = -1;

    void Start()
    {
        // Start met het eerste liedje
        NextTrack();
    }

    // Deze publieke functie wordt aangeroepen om naar het volgende liedje te gaan
    public void NextTrack()
    {
        if (musicPlaylist.Length == 0)
        {
            Debug.LogWarning("De music playlist is leeg!");
            return;
        }

        // Ga naar de volgende index en begin opnieuw als we aan het einde zijn
        currentTrackIndex = (currentTrackIndex + 1) % musicPlaylist.Length;

        // Stel de nieuwe audio clip in en speel hem af
        musicSource.clip = musicPlaylist[currentTrackIndex];
        musicSource.Play();

        Debug.Log("Speelt nu: " + musicSource.clip.name);
    }
}