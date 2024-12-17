using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

public class CameraController : MonoBehaviour
{
    // variables for camera pan
    public float speedPan;
    public float orthoZoomSpeed;
    public Camera mainCamera;
    public float minDistanceZoomIn, maxDistanceZoomOut;
    public Transform localTransform;
    private float currentZoom => mainCamera.orthographicSize;
    private float zoomDelta => maxDistanceZoomOut - minDistanceZoomIn;
    private float zoomLerper => (currentZoom - minDistanceZoomIn) / zoomDelta;
    public float mouseScaleFactor;

    public float touchDelay; // Zeit in Sekunden, bevor eine Berührung als Bewegung interpretiert wird

    private bool isZooming = false;
    private bool isMoving = false;

    public float smoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 _targetPosition;


    // Neue Variablen für die Trägheit (Inertia)
    private Vector2 lastTouchDeltaPosition;
    private Vector2 currentTouchDeltaPosition;
    private Vector2 inertiaVelocity;
    public float inertiaDamping = 0.9f;
    public float maxInertiaDistance = 5f;
    private Vector3 inertiaTargetPosition;
    public float ScrollWheelSpeed;


    private void Update()
    {
       if (CanvasOpener.IsMouseOverUIElement())
        {
            return;
        }

        if (Input.touchCount == 2)
        {
            StopAllCoroutines();
            isZooming = true;
            isMoving = false;
            HandleZoom();
        }
        else if (Input.touchCount == 1)
        {
            if (!isMoving && !isZooming)
            {
                StartCoroutine(StartMovingAfterDelay());
            }

            if (isMoving)
            {
                HandleMovement();
                // Berechne die aktuelle DeltaPosition
                currentTouchDeltaPosition = Input.GetTouch(0).deltaPosition;
            }
        }
        else
        {
            if (isMoving)
            {
                // Wenn der Finger losgelassen wurde, speichere die letzte DeltaPosition als Inertia-Geschwindigkeit
                inertiaVelocity = currentTouchDeltaPosition * speedPan * Time.deltaTime;
                inertiaVelocity *= mainCamera.orthographicSize / maxDistanceZoomOut;
                inertiaVelocity = Vector2.ClampMagnitude(inertiaVelocity, maxInertiaDistance);
                inertiaVelocity *= -1;
                isMoving = false;
                inertiaTargetPosition = transform.localPosition;
            }

            // Wende Trägheit an
            if (inertiaVelocity.magnitude > 0.1f)
            {
                HandleInertia();
                inertiaVelocity *= inertiaDamping; // Dämpfe die Geschwindigkeit ab
            }
            else
            {
                StopAllCoroutines();
                isZooming = false;
                isMoving = false;
                _targetPosition = transform.localPosition;
            }
        }
    }


    private IEnumerator StartMovingAfterDelay()
    {
        yield return new WaitForSeconds(touchDelay);
        if (Input.touchCount == 1 && !isZooming)
        {
            isMoving = true;
        }
    }

    private void HandleZoom()
    {
        Touch firstTouch = Input.GetTouch(0);
        Touch secondTouch = Input.GetTouch(1);

        Vector2 touchZeroPreviousPosition = firstTouch.position - firstTouch.deltaPosition;
        Vector2 touchOnePreviousPosition = secondTouch.position - secondTouch.deltaPosition;

        float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
        float TouchDeltaMag = (firstTouch.position - secondTouch.position).magnitude;

        float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;

        Zoom(deltaMagDiff * orthoZoomSpeed);
    }

    public void Zoom(float delta)
    {
        if (Math.Abs(delta) <= 0.001)
        {
            return;    
        }

        
        mainCamera.orthographicSize += delta;
        mainCamera.orthographicSize = Mathf.Max(mainCamera.orthographicSize, minDistanceZoomIn);
        mainCamera.orthographicSize = Mathf.Min(mainCamera.orthographicSize, maxDistanceZoomOut);
    }

    private void HandleInertia()
    {
        var targetX = transform.localPosition.x + inertiaVelocity.x;
        var targetZ = transform.localPosition.z + inertiaVelocity.y;

        float zoomFactor = (mainCamera.orthographicSize - minDistanceZoomIn) / zoomDelta;

        var erlaubtXMin = GetXMinFunction(targetZ, zoomFactor);
        var erlaubtXMax = GetXMaxFunction(targetZ, zoomFactor);
        var erlaubtZMin = GetZMinFunction(targetX, zoomFactor);
        var erlaubtZMax = GetZMaxFunction(targetX, zoomFactor);

        inertiaTargetPosition = new Vector3(
            Mathf.Clamp(targetX, erlaubtXMin, erlaubtXMax),
            transform.localPosition.y,
            Mathf.Clamp(targetZ, erlaubtZMin, erlaubtZMax)
        );


        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, inertiaTargetPosition, ref velocity,
            smoothTime * Time.deltaTime);
    }




    private void HandleMovement()
    {
        float zoomFactor = (mainCamera.orthographicSize - minDistanceZoomIn) / zoomDelta;

        Vector2 inputPosition = Input.touchCount == 1
            ? Input.GetTouch(0).deltaPosition
            : new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseScaleFactor;

        inputPosition *= mainCamera.orthographicSize / maxDistanceZoomOut;

        var targetX = -inputPosition.x * speedPan * Time.deltaTime;
        targetX += transform.localPosition.x;
        var targetZ = -inputPosition.y * speedPan * Time.deltaTime;
        targetZ += transform.localPosition.z;

        var erlaubtXMin = GetXMinFunction(targetZ, zoomFactor);
        var erlaubtXMax = GetXMaxFunction(targetZ, zoomFactor);
        var erlaubtZMin = GetZMinFunction(targetX, zoomFactor);
        var erlaubtZMax = GetZMaxFunction(targetX, zoomFactor);

        var targetPosition = new Vector3(
            Mathf.Clamp(targetX, erlaubtXMin, erlaubtXMax),
            transform.localPosition.y,
            Mathf.Clamp(targetZ, erlaubtZMin, erlaubtZMax)
        );


        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref velocity,
            smoothTime * Time.deltaTime);
    }


    private float GetZMaxFunction(float valueX, float zoomFactor)
    {
        return -0.3f * valueX + 10.7f - (zoomFactor * 11.4f);
    }

    private float GetZMinFunction(float valueX, float zoomFactor)
    {
        return -0.4388f * valueX - 33.6748f + (zoomFactor * 11.4f);
    }

    private float GetXMaxFunction(float valueZ, float zoomFactor)
    {
        return 0.413f * valueZ + 36.47f - (zoomFactor * 3.2f);
    }

    private float GetXMinFunction(float valueZ, float zoomFactor)
    {
        return 0.446f * valueZ - 21.08f + (zoomFactor * 3.2f);
    }

    private bool Similar(Vector3 first, Vector3 second)
    {
        float epsilon = 0.1f;
        var diff = second - first;
        return diff.x <= epsilon && diff.y <= epsilon && diff.z <= epsilon;
    }
}