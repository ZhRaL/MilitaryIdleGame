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
    

    public Transform localTransform;
    
    public float smoothSpeed;

    private Vector3 lastMousePos;
    private bool mousMoving;
    public float mouseScaleFactor;
    
    private Vector3 targetPosition;

    private float currentZoom => mainCamera.orthographicSize;
    private float zoomDelta => maxDistanceZoomOut - minDistanceZoomIn;
    private float zoomLerper => (currentZoom - minDistanceZoomIn) /zoomDelta;

    [Header("Low Zoom Level")] 
    public Vector2 Local_Low_X;
    public Vector2 Local_Low_Z;
    public Vector2 World_Low_X, World_Low_Z;

    [Header("High Zoom Level")] 
    public Vector2 Local_High_X;
    public Vector2 Local_High_Z;
    public Vector2 World_High_X, World_High_Z;
 
     private void Start()
    {
        targetPosition = localTransform.localPosition;
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
        var tempTargetPos = localTransform.localPosition + (new Vector3(-touchDeltaPosition.x * zoomFaktorRecalculate, 0,
            -touchDeltaPosition.y * zoomFaktorRecalculate) * speedPan);
        
        targetPosition = tempTargetPos;
        
        targetPosition = checkLocal(tempTargetPos);
        var tempWorld = localTransform.TransformPoint(targetPosition-localTransform.localPosition);

        tempWorld = checkWorld(tempWorld);
        var transformed = localTransform.InverseTransformPoint(tempWorld);
        
        targetPosition += transformed;
    }

    private float Between(float value, float min, float max)
    {
        return Mathf.Min(max, Mathf.Max(value, min));
    }
    
    private Vector3 checkLocal(Vector3 vec) {
        var borderX = GetValue(CameraValues.LOCAL_X);
        var borderZ = GetValue(CameraValues.LOCAL_Z);
        
        return new Vector3(Within(vec.x,borderX),
            vec.y,
            Within(vec.z,borderZ));
    }

    private Vector3 checkWorld(Vector3 vec) {
        var borderX = GetValue(CameraValues.WORLD_X);
        var borderZ = GetValue(CameraValues.WORLD_Z);
    
        return new Vector3(Within(vec.x,borderX),
            vec.y,
            Within(vec.z,borderZ));
    }

    private float Within(float value, Vector2 borders) {
        return Between(value,borders.x,borders.y);
    }

    private Vector2 GetValue(CameraValues type)
    {
        return type switch
        {
            CameraValues.LOCAL_X => Vector2.Lerp(Local_Low_X, Local_High_X, zoomLerper),
            CameraValues.LOCAL_Z => Vector2.Lerp(Local_Low_Z, Local_High_Z, zoomLerper),

            CameraValues.WORLD_X => Vector2.Lerp(World_Low_X, World_High_X, zoomLerper),
            CameraValues.WORLD_Z => Vector2.Lerp(World_Low_Z, World_High_Z, zoomLerper)
        };
    }

    private enum CameraValues {
        LOCAL_X,LOCAL_Z,WORLD_X,WORLD_Z
    }
    
    private void moveCamera()
    {
        Vector3 smoothedPosition = Vector3.Lerp(localTransform.localPosition, targetPosition, smoothSpeed * Time.deltaTime);

        localTransform.localPosition = smoothedPosition;
    }
    
}