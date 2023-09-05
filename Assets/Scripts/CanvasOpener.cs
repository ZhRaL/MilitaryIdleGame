using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasOpener : MonoBehaviour
{
    public GameObject overlay;
    private float temp;

    private void OnMouseDown()
    {
        temp = Time.time;
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("WOrks!!");
        if (EventSystem.current.IsPointerOverGameObject() || Time.time - temp > 0.2)
        {
            return;
        }
        overlay.SetActive(true);
    }
}