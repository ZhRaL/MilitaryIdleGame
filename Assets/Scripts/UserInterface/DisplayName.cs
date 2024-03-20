using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayName : MonoBehaviour
{
    public GameObject namePlatePrefab;
    private GameObject _namePlate;
    
    public float hightAbove;

    public string displayText;

    void Start()
    {
        // Schritt 1: Suche nach einem GameObject mit dem Tag "WorldCanvas"
        GameObject worldCanvas = GameObject.FindGameObjectWithTag("WorldCanvas");

        if (worldCanvas != null)
        {
            // Schritt 2: Erstelle ein GameObject anhand des Prefabs
            _namePlate = Instantiate(namePlatePrefab);

            // Schritt 3: Setze das erstellte GameObject als Kind von WorldCanvas
            _namePlate.transform.SetParent(worldCanvas.transform, false);

            // Schritt 4: Übergebe den Text an die TMP_Text-Komponente im Prefab
            TMP_Text textComponent = _namePlate.GetComponentInChildren<TMP_Text>();

            if (textComponent != null)
            {
                textComponent.text = displayText;
            }
            else
            {
                Debug.LogError("Das Prefab enthält keine TMP_Text-Komponente.");
            }
        }
        else
        {
            Debug.LogError("Kein GameObject mit dem Tag 'WorldCanvas' gefunden.");
        }
    }
    private void Update()
    {
        _namePlate.transform.position = transform.position + (Vector3.up*hightAbove);
        _namePlate.transform.rotation = Camera.main.transform.rotation;
    }

    private void OnDisable()
    {
        if(_namePlate)
        _namePlate.SetActive(false);
    }

    private void OnEnable()
    {
        _namePlate?.SetActive(true);
    }
}
