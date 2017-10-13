//// Just add this script to your GetComponent<Camera>(). It doesn't need any configuration.
using UnityEngine.UI;
using UnityEngine;
using System.Threading;

public class TouchCamera : MonoBehaviour
{
    
    public float speedPan = 0.1F;

    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
    public float minZoom = 130.0f;
    public float maxZoom = 160.0f;

    public float limitLeft = -10f;
    public float limitRight = 10f;
    public float limitTop = 50;
    public float limitBottom = -1f;

    public GameObject moveUp;

    bool KT_Limit()
    {
        return (transform.position.x > limitLeft &&
            transform.position.x < limitRight &&
                transform.position.y < limitTop &&
                    transform.position.y > limitBottom);

    }
    void Update()
    {
        //if (transform.position.x < limitLeft)
        //{
        //    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        //    transform.Translate(-touchDeltaPosition.x * speedPan + 0.1f, -touchDeltaPosition.y * speedPan, 0);

        //    return;
        //}
        //if (transform.position.x > limitRight)
        //{
        //    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        //    transform.Translate(-touchDeltaPosition.x * speedPan - 0.1f, -touchDeltaPosition.y * speedPan, 0);

        //    return;
        //}
        if (Input.touchCount == 1 && transform.position.y <= limitBottom)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(0, -touchDeltaPosition.y * speedPan + 4f, 0);

            return;
        }
        if (Input.touchCount == 1 && transform.position.y >= limitTop)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(0, -touchDeltaPosition.y * speedPan - 4f, 0);
            return;
        }
        


        
        if (Input.touchCount ==1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(0, -touchDeltaPosition.y * speedPan, 0);
            
        }
        else if (Input.touchCount == 2 )
        {
            
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // If the camera is orthographic...
                if (GetComponent<Camera>().orthographic)
                {
                    // ... change the orthographic size based on the change in distance between the touches.
                    GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                    // Make sure the orthographic size never drops below zero.
                    GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 0.1f);
                }
                else
                {
                    // Otherwise change the field of view based on the change in distance between the touches.
                    GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                    // Clamp the field of view to make sure it's between 0 and 180.
                    GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, minZoom, maxZoom);
                }
        }
        if (isMoveCam)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 200);
        }
        if (transform.position.y >= -0.88)
        {
            isMoveCam = false;
            moveUp.SetActive(false);
        }
    }
    bool isMoveCam = false;
    public void MoveUpToGame()
    {
        isMoveCam = true;
        moveUp.SetActive(false);
    }
}




//public class TouchCamera : MonoBehaviour
//{
//    public Vector2 startPos;
//    public Vector2 direction;
//    public bool directionChosen;
//    void Update()
//    {

//        // Track a single touch as a direction control.
//        if (Input.touchCount > 0)
//        {

//            Touch touch = Input.GetTouch(0);

//            // Handle finger movements based on touch phase.
//            switch (touch.phase)
//            {
//                // Record initial touch position.
//                case TouchPhase.Began:
//                    startPos = touch.position;
//                    directionChosen = false;
//                    break;

//                // Determine direction by comparing the current touch position with the initial one.
//                case TouchPhase.Moved:
//                    direction = touch.position - startPos;
//                    break;

//                // Report that a direction has been chosen when the finger is lifted.
//                case TouchPhase.Ended:
//                    directionChosen = true;
//                    break;
//            }
//        }
//        if (directionChosen)
//        {
//            transform.
//        }
//    }



//    //public float zoomSize = 5;
//    //private void Update()
//    //{
//    //    //if (Input.GetAxis("Mouse ScrollWheel") > 0)
//    //    //{
//    //        GetComponent<Camera>().orthographicSize = zoomSize - 1;
//    //    //}
//    //}
//}



//public class TouchCamera : MonoBehaviour
//{
//    Vector2?[] oldTouchPositions = {
//        null,
//        null
//    };
//    Vector2 oldTouchVector;
//    float oldTouchDistance;

//    void Update()
//    {
//        if (Input.touchCount == 0)
//        {
//            oldTouchPositions[0] = null;
//            oldTouchPositions[1] = null;
//        }
//        else if (Input.touchCount == 1)
//        {
//            if (oldTouchPositions[0] == null || oldTouchPositions[1] != null)
//            {
//                oldTouchPositions[0] = Input.GetTouch(0).position;
//                oldTouchPositions[1] = null;
//            }
//            else
//            {
//                Vector2 newTouchPosition = Input.GetTouch(0).position;

//                transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] - newTouchPosition) * GetComponent<Camera>().orthographicSize / GetComponent<Camera>().pixelHeight * 2f));

//                oldTouchPositions[0] = newTouchPosition;
//            }

//        }
//        else
//        {
//            if (oldTouchPositions[1] == null)
//            {
//                oldTouchPositions[0] = Input.GetTouch(0).position;
//                oldTouchPositions[1] = Input.GetTouch(1).position;
//                oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
//                oldTouchDistance = oldTouchVector.magnitude;
//            }
//            else
//            {
//                Vector2 screen = new Vector2(GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight);

//                Vector2[] newTouchPositions = {
//                    Input.GetTouch(0).position,
//                    Input.GetTouch(1).position
//                };
//                Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
//                float newTouchDistance = newTouchVector.magnitude;

//                transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] + oldTouchPositions[1] - screen) * GetComponent<Camera>().orthographicSize / screen.y));
//                transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, Mathf.Asin(Mathf.Clamp((oldTouchVector.y * newTouchVector.x - oldTouchVector.x * newTouchVector.y) / oldTouchDistance / newTouchDistance, -1f, 1f)) / 0.0274532924f));
//                GetComponent<Camera>().orthographicSize *= oldTouchDistance / newTouchDistance;
//                transform.position -= transform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * GetComponent<Camera>().orthographicSize / screen.y);

//                oldTouchPositions[0] = newTouchPositions[0];
//                oldTouchPositions[1] = newTouchPositions[1];
//                oldTouchVector = newTouchVector;
//                oldTouchDistance = newTouchDistance;
//            }
//        }
//    }
//}
