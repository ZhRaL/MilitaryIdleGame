using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAdjuster : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> texts;
    
    void Start()
    {
        float minFontSize = texts[0].fontSize;

        foreach (TextMeshProUGUI textMeshPro in texts)
        {
            if (textMeshPro.fontSize < minFontSize)
            {
                minFontSize = textMeshPro.fontSize;
            }
        }

        // Setze alle Schriftgrößen auf die kleinste gefundene Schriftgröße
        foreach (TextMeshProUGUI textMeshPro in texts)
        {
            textMeshPro.enableAutoSizing = false;
            textMeshPro.fontSize = minFontSize;
        }
    }
    
}
