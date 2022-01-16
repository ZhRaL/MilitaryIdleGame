using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasOpener : MonoBehaviour
{
    public GameObject overlay;
    public GameObject overlay_NotReady;
    public int referringNumber;
    private float temp;
    public GameObject Controller;

    private void OnMouseDown()
    {
        temp = Time.time;
    }

    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject() || Time.time - temp > 0.2)
        {
            return;
        }

        if (Controller == null)
        {
            overlay.SetActive(true);
            return;
        }

        if (Controller.GetComponent<IController>().isObjectUnlocked(referringNumber))
            overlay.SetActive(true);
        else
        {
            overlay_NotReady.SetActive(true);
        }
    }
}