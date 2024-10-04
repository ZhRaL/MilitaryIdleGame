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

// the other way round, because the Action in AudioSource is mute, true=silence, false=partyHard
public void toggle() {
    bool b = slider.value == 0;
    action?.Invoke(b);
}
}
