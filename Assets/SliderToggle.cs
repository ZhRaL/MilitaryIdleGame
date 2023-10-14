using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderToggle : MonoBehaviour
{
public Slider slider;

public void toggle() {
    slider.value = (slider.value == 1)?0:1;
}

private void OnClick() {
    toggle();
}
}
