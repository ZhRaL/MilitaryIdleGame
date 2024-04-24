using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EndlessRadial : MonoBehaviour
{
    [SerializeField] private float speed;
    private RectTransform recci;

    private void Start()
    {
        recci = GetComponent<RectTransform>();
    }

    private void Update()
    {
        recci.RotateAround(Vector3.forward, speed);
    }
}
