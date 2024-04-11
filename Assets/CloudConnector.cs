using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudConnector : MonoBehaviour
{
    public float rotationSpeed = 45f; // Rotationsgeschwindigkeit in Grad pro Sekunde

    private RectTransform rectTransform;
    private float currentRotation = 0f;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        // Berechne die neue Rotation
        currentRotation -= rotationSpeed * Time.deltaTime;
        currentRotation = Mathf.Repeat(currentRotation, 360f);

        // Setze die neue Rotation des RectTransform
        rectTransform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
    }
}