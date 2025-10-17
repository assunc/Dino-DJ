using UnityEngine;
using UnityEngine.UI;

public class HypeMeterController : MonoBehaviour
{
    // --- PUBLIEKE TOEGANG ---
    [Header("Manager Koppelingen")]
    public ScoreManager scoreManager;
    public static HypeMeterController instance;

    // --- SLIDER & INSTELLINGEN ---
    public Slider hypeSlider;
    public float maxHype = 100f;
    public float hypeIncreaseAmount = 10f;
    public float hypeDecreaseRate = 2f;

    private float currentHype;

    public MusicManager musicManager;
    public DanceManager danceManager;
   
    // --- GELUIDSINSTELLINGEN ---
    [Header("Geluidseffecten")]
    public AudioClip cheerSound; // Het geluid voor pijl omhoog
    public AudioClip booSound;   // Het geluid voor pijl omlaag

    private AudioSource sfxSource; // De component die het geluid afspeelt

    // --- PUBLIEKE EIGENSCHAPPEN ---
    public float NormalizedHype
    {
        get
        {
            if (maxHype <= 0) return 0;
            return currentHype / maxHype;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        sfxSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Begin met de hype meter op 50%
        currentHype = maxHype / 2f;

        hypeSlider.maxValue = maxHype;
        //UpdateHypeUI(); // Zorg ervoor dat de UI direct de startwaarde toont
    }

    void Update()
    {
        // Optie A: Reset het spel met de 'R' toets
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }

        // Reageer op input om de hype te verhogen
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentHype += hypeIncreaseAmount;
            // Speel het juich-geluid af
            if (cheerSound != null)
            {
                //sfxSource.PlayOneShot(cheerSound);
            }
        }

        // --- NIEUW: Hype handmatig verlagen ---
        // Reageer op input om de hype te verlagen
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentHype -= hypeIncreaseAmount; // We gebruiken dezelfde waarde als voor het verhogen
            // Speel het boe-geluid af
            if (booSound != null)
            {
                sfxSource.PlayOneShot(booSound);
            }
        }

        // Luister naar de pijl-naar-rechts toets
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            Debug.Log("Rechter pijltje ingedrukt!");
            // Geef een seintje aan de managers om naar het volgende item te gaan
            if (musicManager != null)
            {
                Debug.Log("MusicManager is GEVONDEN! Volgend liedje wordt gestart...");
                musicManager.NextTrack();
            }
            else
            {
                // Deze rode foutmelding zie je als de koppeling mist.
                Debug.LogError("FOUT: De MusicManager is NIET gekoppeld in de Inspector!");
            }

            if (danceManager != null)
            {
                Debug.Log("DanceManager is GEVONDEN! Volgend dansje wordt gestart...");
                danceManager.NextDance();
            }
            else
            {
                // Deze rode foutmelding zie je als de koppeling mist.
                Debug.LogError("FOUT: De DanceManager is NIET gekoppeld in de Inspector!");
            }
        }

        // Laat de hype langzaam automatisch dalen over tijd
        if (currentHype > 0)
        {
            currentHype -= hypeDecreaseRate * Time.deltaTime;
        }



        // Zorg ervoor dat de hype binnen de grenzen blijft (0 tot maxHype)
        currentHype = Mathf.Clamp(currentHype, 0, maxHype);

        UpdateHypeUI();
    }

    private void UpdateHypeUI()
    {
        hypeSlider.value = currentHype;
    }

    public void ResetHype()
    {
        currentHype = maxHype / 2f;
        UpdateHypeUI();
    }

    public int GetCurrentMultiplier()
    {
        if (NormalizedHype >= 0.75f) { return 4; }
        else if (NormalizedHype >= 0.50f) { return 3; }
        else if (NormalizedHype >= 0.25f) { return 2; }
        else { return 1; }
    }

    public void ResetGame()
    {
        Debug.Log("Spel wordt gereset!");

        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }

        ResetHype();
       
    }
}