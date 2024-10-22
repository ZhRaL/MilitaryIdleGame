using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Util;

public class SliderToggle : MonoBehaviour
{
    public Slider slider;

    public UnityEvent<bool> action;

    
    public SoundSetting soundSetting;
    
    public void toggle()
    {
        action?.Invoke(slider.value == 1);
    }

    public void ToggleValue()
    {
        bool b = slider.value == 0;
        slider.value = b ? 1 : 0;
    }

    public void Start()
    {
        slider.value = soundSetting switch
        {
            SoundSetting.SOUND => AudioManager.Instance.SoundEnabled.ToInt(),
            SoundSetting.MUSIC => AudioManager.Instance.MusicEnabled.ToInt(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public enum SoundSetting
    {
        SOUND,MUSIC
    }
}