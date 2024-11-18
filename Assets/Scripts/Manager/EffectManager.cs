using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    private void Awake()
    {
        Instance ??= this;
    }

    public GameObject rewardEffectPrefab;

    private GameObject worldCanvas;
    // Start is called before the first frame update
    void Start()
    {
        worldCanvas = GameObject.FindGameObjectWithTag("WorldCanvas");
    }

    public void ShowReward(Transform position, int amount)
    {
        GameObject effect = Instantiate(rewardEffectPrefab);
        effect.transform.SetParent(worldCanvas.transform, false);
        var nameplate = position.gameObject.GetComponent<DisplayName>();
        effect.transform.position = nameplate.GetPosition().position;
        
        var textComponent = effect.GetComponentInChildren<TMP_Text>();
        textComponent.text = string.Format(textComponent.text, "" + amount);
        var animator = effect.GetComponent<Animator>();
        Destroy(effect,6f);
    }
    
}
