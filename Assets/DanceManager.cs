using UnityEngine;

public class DanceManager : MonoBehaviour
{
    [Header("Dino Settings")]
    [Tooltip("Sleep hier de Animator componenten van al je dino's in.")]
    public Animator[] dinoAnimators; // Een lijst voor meerdere dino's

    [Tooltip("Typ hier de exacte namen van je Animator Triggers in volgorde.")]
    public string[] danceTriggers;

    [Header("Dance Speed Settings")]
    [Tooltip("De snelheid van de animatie als de hype 0 is.")]
    public float minSpeed = 1f;
    [Tooltip("De maximale snelheid van de animatie als de hype 100% is.")]
    public float maxSpeed = 3f;

    private int currentDanceIndex = -1;

    void Start()
    {
        // Start direct met het eerste dansje
        NextDance();
    }

    void Update()
    {
        // Blijf de snelheid van ALLE dino's bijwerken op basis van de Hype Meter
        if (HypeMeterController.instance != null)
        {
            // Vraag de genormaliseerde hype (0.0 tot 1.0) op
            float hypeValue = HypeMeterController.instance.NormalizedHype;
            // Converteer de hype-waarde naar een snelheid tussen minSpeed en maxSpeed
            float targetSpeed = Mathf.Lerp(minSpeed, maxSpeed, hypeValue);

            // Loop door alle dino's en pas de snelheid aan
            foreach (Animator dinoAnimator in dinoAnimators)
            {
                if (dinoAnimator != null)
                {
                    dinoAnimator.SetFloat("DanceSpeed", targetSpeed);
                }
            }
        }
    }

    // Deze functie start het volgende dansje voor alle dino's
    public void NextDance()
    {
        if (danceTriggers.Length == 0 || dinoAnimators.Length == 0) return;

        // Bepaal het volgende dansje in de reeks
        currentDanceIndex = (currentDanceIndex + 1) % danceTriggers.Length;
        string triggerName = danceTriggers[currentDanceIndex];

        // Loop door alle dino's en activeer de trigger
        foreach (Animator dinoAnimator in dinoAnimators)
        {
            if (dinoAnimator != null)
            {
                dinoAnimator.SetTrigger(triggerName);
            }
        }

        Debug.Log("Alle dino's starten dans: " + triggerName);
    }
}