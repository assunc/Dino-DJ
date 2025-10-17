// Bestand: RainbowAmplitudeControl.cs (AANGEPASTE VERSIE)
using UnityEngine;
using AudioVisualizer;

public class RainbowAmplitudeControl : MonoBehaviour
{
    [Header("Line/Curve Waveform Settings")]
    public LineWaveform[] lineWaveforms;
    public float lineMinAmplitude = 0.0f;
    public float lineMaxAmplitude = 10.0f;

    [Header("Pad Waveform Settings")]
    public PadWaveform[] padWaveforms;
    public float padMinHeight = 0.0f;
    public float padMaxHeight = 3.0f;

    void Update()
    {
        if (HypeMeterController.instance != null)
        {
            // Vraag de genormaliseerde hype (0.0 tot 1.0) op
            float hypeValue = HypeMeterController.instance.NormalizedHype;

            // --- PAS LINE WAVEFORMS AAN ---
            if (lineWaveforms != null)
            {
                // Converteer de hype-waarde naar een amplitude
                float targetAmplitude = Mathf.Lerp(lineMinAmplitude, lineMaxAmplitude, hypeValue);
                foreach (LineWaveform waveform in lineWaveforms)
                {
                    if (waveform != null)
                    {
                        waveform.waveformAmplitude = targetAmplitude;
                    }
                }
            }

            // --- PAS PAD WAVEFORMS AAN ---
            if (padWaveforms != null)
            {
                // Converteer de hype-waarde naar een hoogte
                float targetHeight = Mathf.Lerp(padMinHeight, padMaxHeight, hypeValue);
                foreach (PadWaveform pad in padWaveforms)
                {
                    if (pad != null)
                    {
                        pad.maxHeight = targetHeight;
                    }
                }
            }
        }
    }
}