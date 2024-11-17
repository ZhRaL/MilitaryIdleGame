using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Util;

public class DisplayName : MonoBehaviour
{
    public GameObject namePlatePrefab;
    private GameObject _namePlate;

    public float hightAbove;
    private TMP_Text textComponent;
    private Color color_Army;
    private Color color_Airf;
    private Color color_Mar;

    void Start()
    {
        ColorUtility.TryParseHtmlString("#27973C", out color_Army);
        ColorUtility.TryParseHtmlString("#B92E35", out color_Airf);
        ColorUtility.TryParseHtmlString("#443EAD", out color_Mar);
        
        GameObject worldCanvas = GameObject.FindGameObjectWithTag("WorldCanvas");
        _namePlate = Instantiate(namePlatePrefab);
        _namePlate.transform.SetParent(worldCanvas.transform, false);
        textComponent = _namePlate.GetComponentInChildren<TMP_Text>();

        Soldier soldier = GetComponent<Soldier>();
        soldier.OnNameChanged += adjustText;
        adjustText(soldier.SoldierName);
        switch (soldier.SoldierType)
        {
            case DefenseType.ARMY:
                textComponent.color = color_Army;
                break;
            case DefenseType.AIRFORCE:
                textComponent.color = color_Airf;
                break;
            case DefenseType.MARINE:
                textComponent.color = color_Mar;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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

    public Transform GetPosition()
    {
        return _namePlate.transform;
    }
}