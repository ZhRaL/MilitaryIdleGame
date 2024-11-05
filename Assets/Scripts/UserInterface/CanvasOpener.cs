using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

public class CanvasOpener : MonoBehaviour
{
    public GameObject overlay;
    private bool wasActive;
    private Collider _collider;


    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (overlay.activeSelf)
        {
            wasActive = true;
            return;
        }

        // Last Frame it was active
        if (wasActive)
        {
            wasActive = false;
            return;
        }
        
        if (Input.GetMouseButtonUp(0)) // Prüfen, ob die linke Maustaste losgelassen wurde
        {
            // Erzeuge einen Ray von der Main Camera basierend auf der Mausposition
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (var hit in hits)
            {
                if (hit.collider == _collider)
                {
                    if (!IsMouseOverUIElement())
                    {
                        overlay.SetActive(true);
                    }
                }
            }
        }
    }

    public static bool IsMouseOverUIElement()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, results);

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