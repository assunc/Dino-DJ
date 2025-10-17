// Bestand: HypeMeterVisuals.cs (NIEUWE VERSIE)

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HypeMeterVisuals : MonoBehaviour
{
    [Header("Koppelingen")]
    [Tooltip("De hoofdslider die de 'hype' waarde bijhoudt.")]
    public Slider hypeSlider;

    [Tooltip("Plaats hier de Image componenten van je segmenten, VAN ONDER NAAR BOVEN.")]
    public List<Image> hypeSegments;

    void Start()
    {
        // Zorg ervoor dat alle segmenten op 'Filled' staan.
        foreach (var segment in hypeSegments)
        {
            if (segment.type != Image.Type.Filled)
            {
                Debug.LogWarning("Segment '" + segment.name + "' staat niet op Image Type 'Filled'. Zet dit in de Inspector voor het juiste effect.", segment.gameObject);
            }
            segment.fillMethod = Image.FillMethod.Vertical;
            segment.fillOrigin = (int)Image.OriginVertical.Bottom;
        }
    }

    void Update()
    {
        if (hypeSlider == null) return; 

        float currentValue = hypeSlider.normalizedValue; // Waarde tussen 0 en 1
        float segmentSize = 1f / hypeSegments.Count;

        for (int i = 0; i < hypeSegments.Count; i++)
        {
            Image currentSegment = hypeSegments[i];
            float segmentStartValue = i * segmentSize;

            // Bereken de voortgang (0 tot 1) binnen DIT specifieke segment.
            // Mathf.InverseLerp berekent waar 'currentValue' zich bevindt tussen de start en het einde van het segment.
            float progressInSegment = Mathf.InverseLerp(segmentStartValue, segmentStartValue + segmentSize, currentValue);

            // Mathf.Clamp zorgt ervoor dat de waarde nooit onder 0 of boven 1 komt.
            currentSegment.fillAmount = Mathf.Clamp01(progressInSegment);
        }
    }


}