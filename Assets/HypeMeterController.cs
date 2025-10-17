using UnityEngine;
using UnityEngine.UI;

public class HypeMeterController : MonoBehaviour
{
    // --- PUBLIEKE TOEGANG ---
    public static HypeMeterController instance;

    // --- SLIDER & INSTELLINGEN ---
    public Slider hypeSlider;
    public float maxHype = 100f;
    public float hypeIncreaseAmount = 10f;
    public float hypeDecreaseRate = 2f;

    private float currentHype;

    public MusicManager musicManager;
    public DanceManager danceManager;

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
    }

    void Start()
    {
        // Begin met de hype meter op 50%
        currentHype = maxHype / 2f;

        hypeSlider.maxValue = maxHype;
        UpdateHypeUI(); // Zorg ervoor dat de UI direct de startwaarde toont
    }

    void Update()
    {
        // Reageer op input om de hype te verhogen
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentHype += hypeIncreaseAmount;
        }

        // --- NIEUW: Hype handmatig verlagen ---
        // Reageer op input om de hype te verlagen
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentHype -= hypeIncreaseAmount; // We gebruiken dezelfde waarde als voor het verhogen
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

    public int GetCurrentMultiplier()
    {
        if (NormalizedHype >= 0.75f) { return 4; }
        else if (NormalizedHype >= 0.50f) { return 3; }
        else if (NormalizedHype >= 0.25f) { return 2; }
        else { return 1; }
    }
}