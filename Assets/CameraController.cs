using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Update()
    {
        if (CanvasOpener.MouseOverElement())
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
            }
        }
        else if (isMoving && !Similar(transform.localPosition, _targetPosition))
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _targetPosition, ref velocity,
                smoothTime * Time.deltaTime);
        }
        else
        {
            StopAllCoroutines();
            isZooming = false;
            isMoving = false;
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

        mainCamera.orthographicSize += deltaMagDiff * orthoZoomSpeed;
        mainCamera.orthographicSize = Mathf.Max(mainCamera.orthographicSize, minDistanceZoomIn);
        mainCamera.orthographicSize = Mathf.Min(mainCamera.orthographicSize, maxDistanceZoomOut);
    }

    private void HandleMovement()
    {
        float zoomFactor = (mainCamera.orthographicSize - minDistanceZoomIn) / zoomDelta;

        Vector2 inputPosition = Input.touchCount == 1
            ? Input.GetTouch(0).deltaPosition
            : new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseScaleFactor;
        inputPosition *= (float)((float)mainCamera.orthographicSize / maxDistanceZoomOut);

        var targetX = -inputPosition.x * speedPan * Time.deltaTime;
        targetX += transform.localPosition.x;
        var targetZ = -inputPosition.y * speedPan * Time.deltaTime;
        targetZ += transform.localPosition.z;

        // Form für xMin
        var erlaubtXMin = GetXMinFunction(zoomFactor);
        var erlaubtXMax = GetXMaxFunction(zoomFactor);
        var erlaubtZMin = GetZMinFunction(zoomFactor);
        var erlaubtZMax = GetZMaxFunction(zoomFactor);


        var current = transform.localPosition;
        current.x = Mathf.Clamp(targetX, erlaubtXMin, erlaubtXMax);
        current.z = Mathf.Clamp(targetZ, erlaubtZMin, erlaubtZMax);

        var targetPosition = new Vector3(
            Mathf.Clamp(targetX, erlaubtXMin, erlaubtXMax),
            transform.localPosition.y,
            Mathf.Clamp(targetZ, erlaubtZMin, erlaubtZMax)
        );

        _targetPosition = targetPosition;
        
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _targetPosition, ref velocity,
            smoothTime * Time.deltaTime);
    }

    private float GetZMaxFunction(float zoomFactor)
    {
        return -0.3f * transform.localPosition.x + 10.7f - (zoomFactor * 11.4f);
    }

    private float GetZMinFunction(float zoomFactor)
    {
        return -0.4388f * transform.localPosition.x - 33.6748f + (zoomFactor * 11.4f);
    }

    private float GetXMaxFunction(float zoomFactor)
    {
        return 0.413f * transform.localPosition.z + 36.47f - (zoomFactor * 3.2f);
    }

    private float GetXMinFunction(float zoomFactor)
    {
        return 0.446f * transform.localPosition.z - 21.08f + (zoomFactor * 3.2f);
    }

    private bool Similar(Vector3 first, Vector3 second)
    {
        float epsilon = 0.1f;
        var diff = second - first;
        return diff.x <= epsilon && diff.y <= epsilon && diff.z <= epsilon;
    }
}