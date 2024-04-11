using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu]
public class Loader : MonoBehaviour
{
    public float waitForSeconds;
    public float transitionTime;
    public Image transitionImage;

    public bool clickForStart;
    private void Start()
    {
        if(!clickForStart)
            StartCoroutine(LoadScene());
    }

    public void LoadNewScene()
    {
        StartCoroutine(LoadScene());
    }
    
    private IEnumerator LoadScene()
    {
        float t = 0f;
        while (t < waitForSeconds)
        {
            t += Time.deltaTime;
            yield return null;
        }

        t = 0f;
        while (t < transitionTime)
        {
            t += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(t / transitionTime);
            Color c = transitionImage.color;
            c.a = t;
            transitionImage.color = c;
            yield return null;
        }
        SceneManager.LoadScene(1);
    }

}
