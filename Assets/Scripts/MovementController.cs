using System.Drawing;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

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

    public Vector2 Local_X, Local_Z;

    public Transform localTransform;
    
    public float smoothSpeed;

    private Vector3 lastMousePos;
    private bool mousMoving;
    public float mouseScaleFactor;

    private Vector3 targetPosition;
 
     private void Start()
    {

        targetPosition = localTransform.position;
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
        var tempTargetPos = localTransform.position + new Vector3(-touchDeltaPosition.x * zoomFaktorRecalculate, 0,
            -touchDeltaPosition.y * zoomFaktorRecalculate) * speedPan;
        
        var tempRight = Mathf.Max(tempTargetPos.x, Local_X.y);
        var tempX = Mathf.Min(tempRight, Local_X.x);
        
        var tempTop = Mathf.Max(tempTargetPos.z, Local_Z.y);
        var tempZ = Mathf.Min(tempTop, Local_Z.x);

        targetPosition = new Vector3(tempX, targetPosition.y, tempZ);
        
        // Vector3 temp = Quaternion.Euler(0, 150, 0) * new Vector3(-touchDeltaPosition.x * zoomFaktorRecalculate, 0,
        //     -touchDeltaPosition.y * zoomFaktorRecalculate) * speedPan;
        // targetPosition = transform.position;
        // targetPosition += temp;
    }

    private float Between(float value, float min, float max)
    {
        return Mathf.Min(max, Mathf.Max(value, min));
    }


    private void moveCamera()
    {
        Vector3 smoothedPosition = Vector3.Lerp(localTransform.position, targetPosition, smoothSpeed * Time.deltaTime);

        localTransform.position = smoothedPosition;
    }
    
    
    
    
}
