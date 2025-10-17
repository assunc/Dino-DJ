using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Tooltip("Het UI-element dat de score moet weergeven.")]
    public TextMeshProUGUI scoreText;

    [Tooltip("Het UI-element dat de huidige multiplier moet weergeven.")]
    public TextMeshProUGUI multiplierText; // <-- NIEUWE REGEL
    public HypeMeterController hypeController;

    //private int currentMultiplier = 0; 

   

    private int score = 0;

    void Start()
    {
        UpdateScoreText();
        UpdateMultiplierText(); // <-- NIEUWE REGEL: Initialiseer de multiplier tekst
    }

    void Update()
    {
        // Als de speler op pijl-omhoog drukt...
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            int currentMultiplier = hypeController.GetCurrentMultiplier(); 

            score += currentMultiplier;
            UpdateScoreText();

            //Debug.Log("Combo x" + comboMultiplier + "! Score verhoogd met " + comboMultiplier);
        }

        UpdateMultiplierText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    // --- NIEUWE FUNCTIE ---
    // Deze functie werkt de tekst van de multiplier bij.
    private void UpdateMultiplierText()
    {
        if (multiplierText != null)
        {
            int currentMultiplier = hypeController.GetCurrentMultiplier(); 
            multiplierText.text = "x" + currentMultiplier.ToString();
        }
    }
}