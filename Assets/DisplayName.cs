using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayName : MonoBehaviour
{
    public GameObject textbla;
    public float hightAbove;

    private void Update()
    {
        textbla.transform.position = transform.position + (Vector3.up*hightAbove);
        textbla.transform.rotation = Camera.main.transform.rotation;
    }
}
