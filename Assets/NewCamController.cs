using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamController : MonoBehaviour
{
    public Camera orthoCamera;

    // Zielkoordinaten für den sichtbaren Bereich (Weltpositionen)
    public float leftVisible = -10f;
    public float rightVisible = 10f;
    public float topVisible = 5f;
    public float bottomVisible = -5f;

    void Start()
    {
        if (orthoCamera == null)
        {
            orthoCamera = Camera.main;
        }
    }

    void Update()
    {
        AdjustCameraPosition();
    }

    void AdjustCameraPosition()
    {
        // Viewport-Ecken in Weltkoordinaten berechnen
        Vector3 topLeft = orthoCamera.ViewportToWorldPoint(new Vector3(0, 1, orthoCamera.nearClipPlane));
        Vector3 topRight = orthoCamera.ViewportToWorldPoint(new Vector3(1, 1, orthoCamera.nearClipPlane));
        Vector3 bottomLeft = orthoCamera.ViewportToWorldPoint(new Vector3(0, 0, orthoCamera.nearClipPlane));
        Vector3 bottomRight = orthoCamera.ViewportToWorldPoint(new Vector3(1, 0, orthoCamera.nearClipPlane));

        // Aktuelle Kameraposition
        Vector3 pos = orthoCamera.transform.position;

        // Horizontale und vertikale Grenzen berechnen
        float cameraLeft = Mathf.Min(topLeft.x, bottomLeft.x);
        float cameraRight = Mathf.Max(topRight.x, bottomRight.x);
        float cameraBottom = Mathf.Min(bottomLeft.y, bottomRight.y);
        float cameraTop = Mathf.Max(topLeft.y, topRight.y);

        // Horizontale Position korrigieren
        if (cameraLeft < leftVisible)
        {
            pos.x += leftVisible - cameraLeft;
        }
        else if (cameraRight > rightVisible)
        {
            pos.x -= cameraRight - rightVisible;
        }

        // Vertikale Position korrigieren
        if (cameraBottom < bottomVisible)
        {
            pos.y += bottomVisible - cameraBottom;
        }
        else if (cameraTop > topVisible)
        {
            pos.y -= cameraTop - topVisible;
        }

        // Korrigierte Position auf die Kamera anwenden
        orthoCamera.transform.position = pos;
    }
}
