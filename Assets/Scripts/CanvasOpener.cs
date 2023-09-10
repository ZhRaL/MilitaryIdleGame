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

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData,results);
        Debug.Log("list is: "+results);
        
        if (MouseOverElement() || Time.time - temp > 0.2)
        {
            return;
        }

        Debug.Log("Great sogar");
        overlay.SetActive(true);
    }

    private bool MouseOverElement()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData,results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.tag == "Clickthrough")
            {
                results.RemoveAt(i);
                i--;
            }
        }

        return results.Count > 0;
    }
}