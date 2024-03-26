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
    private TMP_Text textComponent;

    void Start()
    {
        GameObject worldCanvas = GameObject.FindGameObjectWithTag("WorldCanvas");
        _namePlate = Instantiate(namePlatePrefab);
        _namePlate.transform.SetParent(worldCanvas.transform, false);
        textComponent = _namePlate.GetComponentInChildren<TMP_Text>();
        
        Soldier soldier = GetComponent<Soldier>();
        soldier.OnNameChanged += adjustText;
        adjustText(soldier.SoldierName);
    }

    private void adjustText(string text)
    {
        textComponent.text = text;
    }

    private void Update()
    {
        _namePlate.transform.position = transform.position + (Vector3.up * hightAbove);
        _namePlate.transform.rotation = Camera.main.transform.rotation;
    }

    private void OnDisable()
    {
        if (_namePlate)
            _namePlate.SetActive(false);
    }

    private void OnEnable()
    {
        _namePlate?.SetActive(true);
    }
}