using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScreenManager : MonoBehaviour
{
    public float visibilityDuration;
    public LevelLoader levelLoader;
    private float _timer;

    private void Start()
    {
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= visibilityDuration)
        {
            levelLoader.LoadNextLevel();
            _timer = -999f; 
        }
    }
}
