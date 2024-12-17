using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Button _button;             // Referenz auf den Button
    public float initialDelay = 0.5f; // Startverzögerung
    public float minDelay = 0.1f;     // Minimalverzögerung
    public float acceleration = 0.9f; // Beschleunigungsfaktor

    private bool _isHolding = false;   // Wird gedrückt gehalten?
    private bool _suppressClick = false; // Blockiere normalen Klick
    private Coroutine _holdRoutine;

    void Start()
    {
        if (_button == null)
            _button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button != null && _button.interactable)
        {
            _isHolding = true;
            _suppressClick = false; // Zurücksetzen bei neuem Drücken
            _holdRoutine = StartCoroutine(HoldAction());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopHold();

        // Nur normalen Klick ausführen, wenn kein Hold stattgefunden hat
        if (!_suppressClick)
        {
            // _button.onClick.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopHold();
    }

    private IEnumerator HoldAction()
    {
        float currentDelay = initialDelay;

        while (_isHolding)
        {
            _button.onClick.Invoke(); // Event des Buttons ausführen
            _suppressClick = true;   // Blockiere normalen Klick nach Hold
            yield return new WaitForSeconds(currentDelay);

            currentDelay = Mathf.Max(minDelay, currentDelay * acceleration); // Verzögerung verkürzen
        }
    }

    private void StopHold()
    {
        _isHolding = false;

        if (_holdRoutine != null)
            StopCoroutine(_holdRoutine);
    }
}