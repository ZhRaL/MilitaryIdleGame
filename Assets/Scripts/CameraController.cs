using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // variables for camera pan
    public float speedPan;

    // variables for camera zoom in and out
    public float perspectiveZoomSpeed;
    public float orthoZoomSpeed;

    public Camera mainCamera;

    //variables for camera orbit
    public Vector3 FirstPoint;
    public Vector3 SecondPoint;

    public float xAngle; //angle for axes x for rotation
    public float yAngle;
    public float xAngleTemp; //temp variable for angle
    public float yAngleTemp;

    public float minDistanceZoomIn, maxDistanceZoomOut;

    private float zoomFaktorRecalculate;
    public float Left, Right, Top, Bottom;
    private float Left_Origin, Right_Origin, Top_Origin, Bottom_Origin;

    public Transform camTarget;
    public Vector3 offset;
    public float smoothSpeed;


    private void Start()
    {
        Left_Origin = Left;
        Right_Origin = Right;
        Top_Origin = Top;
        Bottom_Origin = Bottom;
    }
    void Update()

    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
            }
        }


        // This part is for camera pan only & for 2 fingers stationary gesture
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            zoomFaktorRecalculate = mainCamera.orthographicSize / 15;
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            Debug.Log("Here"+zoomFaktorRecalculate);
            Vector3 temp = Quaternion.Euler(0, 150, 0) * new Vector3(-touchDeltaPosition.x * zoomFaktorRecalculate, 0, -touchDeltaPosition.y * zoomFaktorRecalculate) * speedPan;
            camTarget.position += temp;
        }

        //this part is for zoom in and out 
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
            float TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;


            if (mainCamera.orthographic)
            {
                mainCamera.orthographicSize += deltaMagDiff * orthoZoomSpeed;
                mainCamera.orthographicSize = Mathf.Max(mainCamera.orthographicSize, minDistanceZoomIn);
                mainCamera.orthographicSize = Mathf.Min(mainCamera.orthographicSize, maxDistanceZoomOut);
            }
            else
            {
                mainCamera.fieldOfView += deltaMagDiff * perspectiveZoomSpeed;
                mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, .1f, 179.9f);
            }
        }
        moveCamera();
    }

    private void adjustBorders()
    {
        
       
        float temp = (1 - zoomFaktorRecalculate)*8;
        Left = Left_Origin + temp;
        Right = Right_Origin - temp;
        Top = Top_Origin - temp;
        Bottom = Bottom_Origin + temp;

    }

    private void moveCamera()
    {
        adjustBorders();
        camTarget.position = new Vector3(Mathf.Min(Mathf.Max(camTarget.position.x, Right), Left), camTarget.position.y, Mathf.Min(Mathf.Max(camTarget.position.z, Top), Bottom));

        Vector3 desiredPosition = camTarget.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
