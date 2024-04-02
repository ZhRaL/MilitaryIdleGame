using UnityEngine;

public class MovementController : MonoBehaviour
{
   
    // variables for camera pan
    public float speedPan;

    // variables for camera zoom in and out
    public float perspectiveZoomSpeed;
    public float orthoZoomSpeed;

    public Camera mainCamera;

    public float minDistanceZoomIn, maxDistanceZoomOut;

    private float zoomFaktorRecalculate;
    public float Left, Right, Top, Bottom;
    private float Left_Origin, Right_Origin, Top_Origin, Bottom_Origin;
    
    public Vector3 offset;
    public float smoothSpeed;

    private Vector3 lastMousePos;
    private bool mousMoving;
    public float mouseScaleFactor;

    private Vector3 targetPosition;

    private void Start()
    {
        Left_Origin = Left;
        Right_Origin = Right;
        Top_Origin = Top;
        Bottom_Origin = Bottom;
        targetPosition = transform.position;
    }

    void Update()

    {
        if (CanvasOpener.MouseOverElement())
        {
            return;
        }


        // This part is for camera pan only & for 2 fingers stationary gesture
        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);

            if (firstTouch.phase == TouchPhase.Moved)
            {
                zoomFaktorRecalculate = mainCamera.orthographicSize / 15;
                Vector2 inputPosition = Input.touchCount == 1
                    ? Input.GetTouch(0).deltaPosition
                    : new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseScaleFactor;

                moveCamTarget(inputPosition);
            }

            if (Input.touchCount > 1)
            {
                Touch secondTouch = Input.GetTouch(1);
                
                Vector2 touchZeroPreviousPosition = firstTouch.position - firstTouch.deltaPosition;
                Vector2 touchOnePreviousPosition = secondTouch.position - secondTouch.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
                float TouchDeltaMag = (firstTouch.position - secondTouch.position).magnitude;

                float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;
// Go

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

        }

        moveCamera();
    }

    private void moveCamTarget(Vector2 touchDeltaPosition)
    {
        Vector3 temp = Quaternion.Euler(0, 150, 0) * new Vector3(-touchDeltaPosition.x * zoomFaktorRecalculate, 0,
            -touchDeltaPosition.y * zoomFaktorRecalculate) * speedPan;
        targetPosition = transform.position;
        targetPosition += temp;
    }

    private void adjustBorders()
    {
        float temp = (1 - zoomFaktorRecalculate) * 8;
        Left = Left_Origin + temp;
        Right = Right_Origin - temp;
        Top = Top_Origin - temp;
        Bottom = Bottom_Origin + temp;
    }

    private void moveCamera()
    {
        adjustBorders();
        targetPosition = new Vector3(Mathf.Min(Mathf.Max(targetPosition.x, Right), Left), targetPosition.y,
            Mathf.Min(Mathf.Max(targetPosition.z, Top), Bottom));

        Vector3 desiredPosition = targetPosition + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
